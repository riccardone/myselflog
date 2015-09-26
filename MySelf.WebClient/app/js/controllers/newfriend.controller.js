myselflogApp.controller('NewFriendController',
    ['$scope', 'datacontext', '$filter', 'moment', '$routeParams', '$route', 'valuesService',
function ($scope, datacontext, $filter, moment, $routeParams, $route, valuesService) {
    $scope.loading = false;
    $scope.profile = { 'isLoaded': false, 'logs': [] };
    $scope.setReport = setReport;
    $scope.date = moment();
    $scope.link = "";
    $scope.graph = {};
    $scope.showTerapies = false;
    $scope.graph = {};
    $scope.graph = Morris.Line({
        element: 'diaryGraph',
        xkey: 'logdate',
        ykeys: ['medicalvalue', 'slow', 'fast'],
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
            sum += parseInt($scope.logs[i].medicalvalue);
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

    function getDataSucceeded(data) {
        $scope.profile = data.logprofileasfriend;
        refreshGraph();
        $scope.loading = false;
    }

    function getData() {
        $scope.loading = true;
        $scope.error = "";
        if ($scope.link && $scope.link.length > 0) {
            var link = $scope.link;
            datacontext.getAllLogsAsFriend(link, getDataSucceeded);
        } else {
            $scope.error = "link not found";
        }
    }

    function loadData() {
        $scope.link = $routeParams.link;
        getData();
    }

    function refreshGraph() {
        if ($scope.profile) {
            var logs = valuesService.getLogs($scope.selectedreport.name, $scope.profile.logs, $scope.date);
            var graphData = [];
            angular.forEach(logs, function (item) {
                if (item.medicalvalue && item.medicalvalue > 0) {
                    graphData.push({ 'logdate': item.logdate, 'medicalvalue': item.medicalvalue });
                }
            });
            if ($scope.showTerapies) {
                var terapies = valuesService.getTerapies($scope.selectedreport.name, $scope.profile.terapies, $scope.date);
                angular.forEach(terapies.slow, function (item) {
                    graphData.push(item);
                });
                angular.forEach(terapies.fast, function (item) {
                    graphData.push(item);
                });
                $scope.terapies = terapies;
            }
            $scope.graph.setData(graphData);
            $scope.selectedreport.description = getReportDescription($scope.selectedreport.name);
            $scope.logs = logs;
            $scope.selectedreport.average = getAverage();
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