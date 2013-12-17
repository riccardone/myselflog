myselflogApp.controller('NewFriendController',
    ['$scope', 'friendWithLinkDatacontext', 'logger', '$filter', 'moment', '$routeParams', '$route',
function ($scope, friendDatacontext, logger, $filter, moment, $routeParams, $route) {
    $scope.loading = false;
    $scope.profile = { 'isLoaded': false, 'logs': [] };
    $scope.setReport = setReport;
    $scope.date = moment();
    $scope.link = "";
    $scope.graph = {};
    $scope.showTerapies = false;
    $scope.graph = Morris.Line({
        element: 'diaryGraph',
        xkey: 'logdate',
        ykeys: ['value'],
        labels: ['Diary']
    });
    $scope.graph2 = Morris.Line({
        element: 'diaryGraph2',
        xkey: 'logdate',
        ykeys: ['value', 'slow', 'fast'],
        labels: ['Diary', 'Slow terapy', 'Fast terapy']
    });
    $scope.reports = [
        { name: "Day", description: "" },
        { name: "Week", description: "" },
        { name: "Month", description: "" },
        { name: "Year", description: "" }
    ];
    $scope.selectedreport = $scope.reports[0];
    $scope.previous = previous;
    $scope.next = next;
    $scope.logs = [];
    $scope.getAverage = getAverage;
    $scope.reload = function () {
        $route.reload();
    };
    $scope.isTerapiesGraphCollapsed = true;

    function getAverage() {
        var sum = 0;
        for (var i = 0; i < $scope.logs.length; i++) {
            sum += parseInt($scope.logs[i].value);
        }

        var avg = sum / $scope.logs.length;
        return parseInt(Math.round(avg * 100) / 100);
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
    
    $scope.$watch('showTerapies', function () {
        refreshGraph();
    });

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
    
    function refreshGraph() {
        if ($scope.profile) {
            var logs = getLogs($scope.selectedreport.name);
            var terapies = getTerapies($scope.selectedreport.name);
            var graphData = [];
            
            if ($scope.showTerapies) {
                // TODO set data with terapies
                angular.forEach(logs, function (item) {
                    var sameDateTerapy = $filter('getByLogDate')(terapies, item.logdate);
                    if (sameDateTerapy) {
                        sameDateTerapy.value = item.value;
                        graphData.push(sameDateTerapy);
                        sameDateTerapy.Processed = true;
                    } else {
                        graphData.push({ 'logdate': item.logdate, 'value': item.value });
                    }
                });
                angular.forEach(terapies, function (item) {
                    if (!item.Processed) {
                        graphData.push(item);
                    }
                });
                $scope.graph2.setData(graphData);
            } else {
                // TODO set data without terapies
                $scope.graph.setData(logs);
            }
            
            $scope.selectedreport.description = getReportDescription($scope.selectedreport.name);
            
            $scope.logs = logs;
            $scope.terapies = terapies;
            $scope.selectedreport.average = getAverage();
        }
    }
    
    function getLogs(reportname) {
        if (reportname == "Year") {
            return getLogsByYear();
        }
        if (reportname == "Month") {
            return getLogsByMonth();
        }
        if (reportname == "Week") {
            return getLogsByWeeks();
        }
        if (reportname == "Day") {
            return getLogsByDay();
        }
        return null;
    }

    function getTerapies(reportname) {
        var terapies = [];
        var terapyObject = {};
        if (reportname == "Year") {
            terapyObject = getTerapiesByYear();
        }
        if (reportname == "Month") {
            terapyObject = getTerapiesByMonth();
        }
        if (reportname == "Week") {
            terapyObject = getTerapiesByWeeks();
        }
        if (reportname == "Day") {
            terapyObject = getTerapiesByDay();
        }
        // Fill terapies graph data starting with fast values
        angular.forEach(terapyObject.fastvalues, function (item) {
            terapies.push({ 'logdate': item.logdate, 'fast': item.terapyvalue, 'slow': 0 });
        });
        // Fill slow values
        angular.forEach(terapyObject.slowvalues, function (item) {
            insertOrUpdateSlowValue(terapies, item);
        });
        function insertOrUpdateSlowValue(terapyList, item) {
            var found = false;
            angular.forEach(terapyList, function (terapy) {
                if (terapy.logdate == item.logdate) {
                    terapy.slowvalues = item.terapyvalue;
                    found = true;
                }
            });
            if (found == false) {
                terapies.push({ 'logdate': item.logdate, 'fast': 0, 'slow': item.terapyvalue });
            }
        }

        return terapies;
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
    
    function getLogsByYear() {
        var logs = [];
        var d2 = new Date($scope.date);
        angular.forEach($scope.profile.logs, function (item) {
            var d = new Date(item.logdate);
            if (d.getYear() == d2.getYear()) {
                logs.push(item);
            }
        });
        return logs;
    }
    
    function getLogsByMonth() {
        var logs = [];
        var d2 = new Date($scope.date);
        angular.forEach($scope.profile.logs, function (item) {
            var d = new Date(item.logdate);
            if ((d.getYear() == d2.getYear()) && (d.getMonth() == d2.getMonth())) {
                logs.push(item);
            }
        });
        return logs;
    }
    
    function getLogsByWeeks() {
        var logs = [];
        var weekdates = getWeekDays($scope.date);
        angular.forEach($scope.profile.logs, function (item) {
            angular.forEach(weekdates, function (weekDay) {
                var a1 = moment(item.logdate).startOf('day').toDate();
                var a2 = weekDay.startOf('day').toDate();
                if (+a1 === +a2) {
                    logs.push(item);
                }
            });
        });
        return logs;
    }
    
    function getLogsByDay() {
        var logs = [];
        angular.forEach($scope.profile.logs, function (item) {
            var a1 = moment(item.logdate).startOf('day').toDate();
            var a2 = moment($scope.date).startOf('day').toDate();
            if (+a1 === +a2) {
                logs.push(item);
            }
        });
        return logs;
    }
    
    function getTerapiesByYear() {
        var fastvalues = [];
        var slowvalues = [];
        var d2 = new Date($scope.date);
        angular.forEach($scope.profile.terapies, function (item) {
            var d = new Date(item.logdate);
            if (d.getYear() == d2.getYear()) {
                if (item.isslow) {
                    slowvalues.push(item);
                } else {
                    fastvalues.push(item);
                }
            }
        });
        return { 'fastvalues': fastvalues, 'slowvalues': slowvalues };
    }

    function getTerapiesByMonth() {
        var fastvalues = [];
        var slowvalues = [];
        var d2 = new Date($scope.date);
        angular.forEach($scope.profile.terapies, function (item) {
            var d = new Date(item.logdate);
            if ((d.getYear() == d2.getYear()) && (d.getMonth() == d2.getMonth())) {
                if (item.isslow) {
                    slowvalues.push(item);
                } else {
                    fastvalues.push(item);
                }
            }
        });
        return { 'fastvalues': fastvalues, 'slowvalues': slowvalues };
    }

    function getTerapiesByWeeks() {
        var fastvalues = [];
        var slowvalues = [];
        var weekdates = getWeekDays($scope.date);
        angular.forEach($scope.profile.terapies, function (item) {
            angular.forEach(weekdates, function (weekDay) {
                var a1 = moment(item.logdate).startOf('day').toDate();
                var a2 = weekDay.startOf('day').toDate();
                if (+a1 === +a2) {
                    if (item.isslow) {
                        slowvalues.push(item);
                    } else {
                        fastvalues.push(item);
                    }
                }
            });
        });
        return { 'fastvalues': fastvalues, 'slowvalues': slowvalues };
    }

    function getTerapiesByDay() {
        var fastvalues = [];
        var slowvalues = [];
        angular.forEach($scope.profile.terapies, function (item) {
            var a1 = moment(item.logdate).startOf('day').toDate();
            var a2 = moment($scope.date).startOf('day').toDate();
            if (+a1 === +a2) {
                if (item.isslow) {
                    slowvalues.push(item);
                } else {
                    fastvalues.push(item);
                }
            }
        });
        return { 'fastvalues': fastvalues, 'slowvalues': slowvalues };
    }
    
    function getReportDescription(reportname) {
        if (reportname == "Year") {
            return moment($scope.date).format('YYYY');
        }
        if (reportname == "Month") {
            return moment($scope.date).format('MMMM YYYY');
        }
        if (reportname == "Week") {
            return "from " + moment($scope.date).day("Sunday").format('DD MM YYYY') + " to " + moment($scope.date).day("Saturday").format('DD MM YYYY');
        }
        if (reportname == "Day") {
            return moment($scope.date).format('DD MM YYYY');
        }
        return null;
    }
}]);