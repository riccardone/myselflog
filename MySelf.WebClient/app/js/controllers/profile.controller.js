myselflogApp.controller('ProfileController',
    ['$scope', 'datacontext', '$filter', '$routeParams',
    function ($scope, datacontext, $filter, $routeParams) {
        $scope.id = $routeParams.id;
        $scope.addValue = addValue;
        $scope.remove = remove;
        $scope.securityLink = "";
        $scope.graph = Morris.Line({
            element: 'diaryGraph',
            xkey: 'logdate',
            ykeys: ['value'],
            labels: ['Diary']
        });
        $scope.item = {};
        $scope.report = "year";
        $scope.date = getNow();
        $scope.setReport = setReport;
        
        function getNow() {
            return $filter('date')(new Date(), 'yyyy-MM-dd HH:mm:ss');
        }
        
        $scope.item.logDate = getNow();

        setData();

        function setData() {
            //var logid = $routeParams.id;
            if (!$scope.selectedprofile) {
                datacontext.getLog($scope.id, setDataSucceeded);
            }
        }

        function setDataSucceeded(data) {
            $scope.selectedprofile = data;
            refreshGraph();
        }
        
        function setReport(date, report) {
            $scope.report = report;
            $scope.date = date;
            refreshGraph();
        }

        function refreshGraph() {
            if ($scope.selectedprofile) {
                var logs = [];
                var d2 = new Date($scope.date);
                if ($scope.report == "year") {
                    angular.forEach($scope.selectedprofile.logs, function (log) {
                        var d = new Date(log.logdate);
                        if (d.getYear() == d2.getYear()) {
                            logs.push(log);
                        }
                    });
                }
                if ($scope.report == "month") {
                    angular.forEach($scope.selectedprofile.logs, function (log) {
                        var d = new Date(log.logdate);
                        if ((d.getYear() == d2.getYear()) && (d.getMonth() == d2.getMonth())) {
                            logs.push(log);
                        }
                    });
                }
                if ($scope.report == "day") {
                    angular.forEach($scope.selectedprofile.logs, function (log) {
                        var d = new Date(log.logdate);
                        if ((d.getYear() == d2.getYear()) && (d.getMonth() == d2.getMonth()) && (d.getDay() == d2.getDay())) {
                            logs.push(log);
                        }
                    });
                }
                $scope.graph.setData(logs);
            }
        }

        function getDaysInMonth(month, year) {
            var date = new Date(year, month, 1);
            var days = [];
            while (date.getMonth() === month) {
                days.push(new Date(date));
                date.setDate(date.getDate() + 1);
            }
            return days;
        }

        $scope.$watch('selectedprofile', function () {
            setData();
        });

        $scope.createProfile = function () {
            datacontext.createProfile("Default", createProfileSucceeded);
        };

        $scope.addSecurityLink = function (logProfileId) {
            datacontext.addSecurityLink(logProfileId, getSecureLinkSucceeded);
        };

        $scope.deleteSecurityLink = function (logProfileId) {
            datacontext.deleteSecurityLink(logProfileId, deleteSecurityLinkSucceeded);
        };

        function deleteSecurityLinkSucceeded() {
            $scope.selectedprofile.securityLink = "";
        }

        $scope.addFriend = function (email, logProfileGlobalId) {
            datacontext.addFriend(email, logProfileGlobalId);
        };

        function createProfileSucceeded() {
            $scope.getData();
        }

        function remove(log) {
            datacontext.remove(log, removeLogSucceeded);
        }

        function removeLogSucceeded(log) {
            for (var i = 0; i < $scope.selectedprofile.logs.length; i++) {
                if ($scope.selectedprofile.logs[i].globalid == log.globalid) {
                    $scope.selectedprofile.logs.splice(i, 1);
                    refreshGraph();
                }
            }
        }

        function refreshSecurityLink(logprofileid) {
            datacontext.getSecureLink(logprofileid, getSecureLinkSucceeded);
        }

        function getSecureLinkSucceeded(data) {
            $scope.selectedprofile.securityLink = data.link;
        }

        function addValue() {
            //if (logForm.$valid) {
            //var today = $filter('date')(new Date(), 'yyyy-MM-dd HH:mm:ss');
            var log = { 'Value': $scope.item.value, 'LogDate': $scope.item.logDate, 'Message': $scope.item.message, 'ProfileId': $scope.selectedprofile.globalid };
            datacontext.save(log, addSucceeded);

            function addSucceeded(value) {
                $scope.selectedprofile.logs.push(value);
                refreshGraph();
                //$scope.graph.setData($scope.selectedprofile.logs);
            }

            function addFailed(error) {
                // todo
            }
            //}
        }
    }]);