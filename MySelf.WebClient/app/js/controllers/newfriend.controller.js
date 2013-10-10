myselflogApp.controller('NewFriendController',
    ['$scope', 'friendDatacontext', 'logger', '$filter', 'moment', '$routeParams',
function ($scope, friendDatacontext, logger, $filter, moment, $routeParams) {
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
            { name: "Day", description: "" },
            { name: "Week", description: "" },
            { name: "Month", description: "" },
            { name: "Year", description: "" }
        ];
        $scope.selectedreport = $scope.reports[0];
        $scope.previous = previous;
        $scope.next = next;
        $scope.getAverage = getAverage;

        function getAverage() {
            var sum = 0;
            for (var i = 0; i < $scope.profile.logs.length; i++) {
                sum += parseInt($scope.profile.logs[i].value);
            }

            var avg = sum / $scope.profile.logs.length;
            return Math.round(avg * 100) / 100;
        }

        function previous() {
            if ($scope.selectedreport.name == "Day") {
                $scope.date = moment($scope.date).subtract("day", 1);
            }
            if ($scope.selectedreport.name == "Week") {
                $scope.date = moment($scope.date).startOf('week').subtract("day", 1).startOf('week');
            }
            if ($scope.selectedreport.name == "Month") {
                $scope.date = moment($scope.date).subtract("month", 1);
            }
            if ($scope.selectedreport.name == "Year") {
                $scope.date = moment($scope.date).subtract("year", 1);
            }
        }

        function next() {
            if ($scope.selectedreport.name == "Day") {
                $scope.date = moment($scope.date).add("day", 1);
            }
            if ($scope.selectedreport.name == "Week") {
                $scope.date = moment($scope.date).endOf('week').add("day", 1).startOf('week');
            }
            if ($scope.selectedreport.name == "Month") {
                $scope.date = moment($scope.date).add("month", 1);
            }
            if ($scope.selectedreport.name == "Year") {
                $scope.date = moment($scope.date).add("year", 1);
            }
        }

        $scope.$watch('date', function () {
            refreshGraph();
        });

        $scope.$watch('selectedreport', function () {
            if (!$scope.profile.isLoaded) {
                loadData();
            } else {
                refreshGraph();
            }
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
            refreshGraph();
            $scope.loading = false;
        }

        function loadData() {
            $scope.link = $routeParams.link;
            getData();
        }

        function getWeekDays(date) {
            var sunday = moment(date).day("Sunday");
            var monday = moment(date).day("Monday");
            var tuesday = moment(date).day("Tuesday");
            var wednesday = moment(date).day("Wednesday‎");
            var thursday = moment(date).day("Thursday");
            var friday = moment(date).day("Friday");
            var saturday = moment(date).day("Saturday");
            var weekdates = [sunday, monday, tuesday, wednesday, thursday, friday, saturday];
            return weekdates;
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
                    $scope.selectedreport.description = moment($scope.date).format('YYYY');
                }
                if ($scope.selectedreport.name == "Month") {
                    angular.forEach($scope.profile.logs, function (log) {
                        var d = new Date(log.logdate);
                        if ((d.getYear() == d2.getYear()) && (d.getMonth() == d2.getMonth())) {
                            logs.push(log);
                        }
                    });
                    $scope.selectedreport.description = moment($scope.date).format('MMMM YYYY');
                }
                if ($scope.selectedreport.name == "Week") {
                    var weekdates = getWeekDays($scope.date);
                    angular.forEach($scope.profile.logs, function (log) {
                        angular.forEach(weekdates, function (weekDay) {
                            var a1 = moment(log.logdate).startOf('day').toDate();
                            var a2 = weekDay.startOf('day').toDate();
                            if (+a1 === +a2) {
                                logs.push(log);
                            }
                        });
                    });
                    $scope.selectedreport.description = "from " + moment($scope.date).day("Sunday").format('DD MM YYYY') + " to " + moment($scope.date).day("Saturday").format('DD MM YYYY');
                }
                if ($scope.selectedreport.name == "Day") {
                    angular.forEach($scope.profile.logs, function (log) {
                        var a1 = moment(log.logdate).startOf('day').toDate();
                        var a2 = moment($scope.date).startOf('day').toDate();
                        if (+a1 === +a2) {
                            logs.push(log);
                        }
                    });
                    $scope.selectedreport.description = moment($scope.date).format('DD MM YYYY');
                }
                $scope.graph.setData(logs);
            }
        }
    }]);