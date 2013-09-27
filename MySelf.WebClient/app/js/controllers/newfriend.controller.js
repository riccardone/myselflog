friendlogApp.controller('NewFriendController',
    ['$scope', 'friendDatacontext', 'logger', '$filter', 'moment',
    function ($scope, friendDatacontext, logger, $filter, moment) {
        $scope.loading = false;
        $scope.profile = { 'isLoaded': false, 'logs': [] };
        $scope.graph = Morris.Line({
            element: 'diaryGraph',
            xkey: 'logdate',
            ykeys: ['value'],
            labels: ['Diary']
        });
        $scope.setReport = setReport;
        $scope.date = moment();
        $scope.link = "";
        $scope.reports = [
            { name: "Day" },
            { name: "Week" },
            { name: "Month" },
            { name: "Year" }
        ];
        $scope.selectedreport = $scope.reports[0];

        $scope.$watch('selectedreport', function () {
            refreshGraph();
        });

        function setReport(report) {
            $scope.selectedreport = report;
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

        function getDataSucceeded(data) {
            $scope.profile = data.logprofileasfriend;
            $scope.loading = false;
        }

        loadData();

        function loadData() {
            readLinkElement();
            getData();
        }

        function readLinkElement() {
            $scope.link = $("#link").val();
        }
        
        function getMonday(d) {
            d = new Date(d);
            var day = d.getDay(),
                diff = d.getDate() - day + (day == 0 ? -6 : 1); // adjust when day is sunday
            return new Date(d.setDate(diff));
        }

        function refreshGraph() {
            if ($scope.profile) {
                var logs = [];
                var d2 = new Date($scope.date);
                if ($scope.selectedreport.name == "Year") {
                    angular.forEach($scope.profile.logs, function (log) {
                        var d = new Date(log.logdate);
                        if (d.getYear() == d2.getYear()) {
                            logs.push(log);
                        }
                    });
                }
                if ($scope.selectedreport.name == "Month") {
                    angular.forEach($scope.profile.logs, function (log) {
                        var d = new Date(log.logdate);
                        if ((d.getYear() == d2.getYear()) && (d.getMonth() == d2.getMonth())) {
                            logs.push(log);
                        }
                    });
                }
                if ($scope.selectedreport.name == "Week") {
                    var monday = moment($scope.date).day("Monday"); //getMonday($scope.date);
                    var tuesday = moment($scope.date).day("Tuesday");
                    var wednesday = moment($scope.date).day("Wednesday‎");
                    var thursday = moment($scope.date).day("Thursday");
                    var friday = moment($scope.date).day("Friday");
                    var saturday = moment($scope.date).day("Saturday");
                    var sunday = moment($scope.date).day("Sunday");
                    var weekdates = [monday, tuesday, wednesday, thursday, friday, saturday, sunday];
                    angular.forEach($scope.profile.logs, function (log) {
                        angular.forEach(weekdates, function (weekDay) {
                            var a1 = moment(log.logdate).startOf('day').toDate();
                            var a2 = weekDay.startOf('day').toDate();
                            if ((a1.getYear() == a2.getYear()) && (a1.getMonth() == a2.getMonth()) && (a1.getDay() == a2.getDay())) {
                                logs.push(log);
                            }
                        });
                    });
                }
                if ($scope.selectedreport.name == "Day") {
                    angular.forEach($scope.profile.logs, function (log) {
                        var d = new Date(log.logdate);
                        if ((d.getYear() == d2.getYear()) && (d.getMonth() == d2.getMonth()) && (d.getDay() == d2.getDay())) {
                            logs.push(log);
                        }
                    });
                }
                $scope.graph.setData(logs);
            }
        }
    }]);