﻿friendlogApp.controller('FriendController',
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
            ykeys: ['value'],
            labels: ['Diary']
        });
        $scope.setReport = setReport;
        $scope.report = "year";
        $scope.date = getNow();
        $scope.getCurrentDate = getCurrentDate;
        $scope.previous = previous;
        $scope.next = next;
        
        function previous() {
            // setDate(dateObj.getDate()-1)
        }
        
        function next() {
            $scope.date = toDateString(nextDay(new Date($scope.date)));
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
            refreshGraph();
        }

        function getDataSucceeded(data) {
            $scope.logprofilesasfriend.push(data.logprofileasfriend);
            refreshGraph();
            //if ($scope.logprofilesasfriend.length > 0) {
            //    var logs = $scope.logprofilesasfriend[0].logs;
            //    $scope.graph.setData(logs);
            //    logger.info("logs loaded");
            //}
            //$scope.loading = false;
        }
        
        function refreshGraph() {
            if ($scope.logprofilesasfriend.length > 0) {
            //if ($scope.selectedprofile) {
                var logs = [];
                var d2 = new Date($scope.date);
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
                        if ((d.getYear() == d2.getYear()) && (d.getMonth() == d2.getMonth()) && (d.getDay() == d2.getDay())) {
                            logs.push(log);
                        }
                    });
                }
                $scope.graph.setData(logs);
                logger.info("logs loaded");
            }
            $scope.loading = false;
        }
    }]);