friendlogApp.controller('FriendController',
    ['$scope', 'friendDatacontext', 'logger', '$filter',
    function ($scope, friendDatacontext, logger, $filter) {
        $scope.loading = false;
        $scope.error = "";
        $scope.logprofilesasfriend = [];
        $scope.getData = getData;
        $scope.link = "";
        $scope.graph = Morris.Line({
            element: 'diaryGraph',
            xkey: 'logdate',
            ykeys: ['medicalvalue'],
            labels: ['Diary']
        });
        $scope.setReport = setReport;
        $scope.report = "day";
        $scope.date = getNow();
        $scope.getCurrentDate = getCurrentDate;
        $scope.previous = previous;
        $scope.next = next;

        //Array.prototype.getUnique = function() {
        //    var u = {}, a = [];
        //    for (var i = 0, l = this.length; i < l; ++i) {
        //        if (u.hasOwnProperty(this[i])) {
        //            continue;
        //        }
        //        a.push(this[i]);
        //        u[this[i]] = 1;
        //    }
        //    return a;
        //};
        
        $scope.$watch('date', function () {
            refreshGraph();
        });
        
        //$scope.$watch('logprofilesasfriend', function () {
        //    $scope.days = getDistinctDays();
        //});
        
        //function getDistinctDays() {
        //    var days = [];
        //    angular.foreach($scope.logprofilesasfriend[0].logs, function (log) {
        //        var dateObj = new Date(log.logDate);
        //        days.push(new Date(dateObj.getYear(), dateObj.getMonth(), dateObj.getDay()));
        //    });
        //    return days.getUnique();
        //}
        
        function previous() {
            $scope.date = toDateString(previousDay(new Date($scope.date)));
        }
        
        function next() {
            $scope.date = toDateString(nextDay(new Date($scope.date)));
        }
        
        function previousDay(date) {
            date.setDate(date.getDate() - 1);
            return date;
        }
        
        function nextDay(date) {
            var e = new Date(date.getTime() + 24 * 60 * 60 * 1000);
            if (e.getHours() != date.getHours()){
                e = new Date(e.getTime() + (e.getHours() - date.getHours()) * 60 * 60 * 1000);
            }
            return e;
        }
        
        function toDateString(date) {
            return $filter('date')(date, 'yyyy-MM-dd HH:mm:ss');
        }
        
        function getNow() {
            return toDateString(new Date()); //$filter('date')(new Date(), 'yyyy-MM-dd HH:mm:ss');
        }
        
        function getCurrentDate() {
            return $filter('date')(new Date($scope.date), 'yyyy-MM-dd');
        }
        
        loadData();
        
        function loadData() {
            readLinkElement();
            getData();
        }
        
        function readLinkElement() {
            $scope.link = $("#link").val();
        }

        function getData() {
            $scope.loading = true;
            $scope.error = "";
            if ($scope.link && $scope.link.length > 0) {
                var link = $scope.link;
                friendDatacontext.getAllLogsAsFriend(link, getDataSucceeded);
            } else {
                $scope.error = "link not found";
            }
        }
        
        function setReport(date, report) {
            $scope.report = report;
            $scope.date = date;
        }

        function getDataSucceeded(data) {
            $scope.logprofilesasfriend.push(data.logprofileasfriend);
            $scope.loading = false;
        }
        
        function getDataForCurrentReport() {
            var logs = [];
            if ($scope.logprofilesasfriend.length > 0) {
                var d2 = new Date($scope.date);
                d2.setHours(0, 0, 0, 0);
                if ($scope.report == "year") {
                    angular.forEach($scope.logprofilesasfriend[0].logs, function (log) {
                        var d = new Date(log.logdate);
                        if (d.getYear() == d2.getYear()) {
                            logs.push(log);
                        }
                    });
                }
                if ($scope.report == "month") {
                    angular.forEach($scope.logprofilesasfriend[0].logs, function (log) {
                        var d = new Date(log.logdate);
                        if ((d.getYear() == d2.getYear()) && (d.getMonth() == d2.getMonth())) {
                            logs.push(log);
                        }
                    });
                }
                if ($scope.report == "day") {
                    angular.forEach($scope.logprofilesasfriend[0].logs, function (log) {
                        var d = new Date(log.logdate);
                        d.setHours(0, 0, 0, 0);
                        if (d == d2) {
                            logs.push(log);
                        }
                    });
                }
            }

            return logs;
        }
        
        function refreshGraph() {
            $scope.graph.setData(getDataForCurrentReport());
        }
    }]);