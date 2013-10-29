myselflogApp.controller('ProfileController',
    ['$scope', 'datacontext', '$filter', '$routeParams',
    function ($scope, datacontext, $filter, $routeParams) {
        $scope.id = $routeParams.id;
        $scope.addValue = addValue;
        $scope.remove = remove;
        $scope.logs = [];
        //$scope.graph = Morris.Line({
        //    element: 'diaryGraph',
        //    xkey: 'logdate',
        //    ykeys: ['value'],
        //    labels: ['Diary']
        //});
        $scope.item = {};
        $scope.report = "year";
        $scope.date = getNow();
        $scope.setReport = setReport;
        $scope.myOptions = { data: 'logs' };
        
        function getNow() {
            return $filter('date')(new Date(), 'yyyy-MM-dd HH:mm:ss');
        }
        
        $scope.item.logDate = getNow();

        setData();

        function setData() {
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
                //var logs = [];
                var d2 = new Date($scope.date);
                if ($scope.report == "year") {
                    angular.forEach($scope.selectedprofile.logs, function (log) {
                        var d = new Date(log.logdate);
                        if (d.getYear() == d2.getYear()) {
                            $scope.logs.push(log);
                        }
                    });
                }
                if ($scope.report == "month") {
                    angular.forEach($scope.selectedprofile.logs, function (log) {
                        var d = new Date(log.logdate);
                        if ((d.getYear() == d2.getYear()) && (d.getMonth() == d2.getMonth())) {
                            $scope.logs.push(log);
                        }
                    });
                }
                if ($scope.report == "day") {
                    angular.forEach($scope.selectedprofile.logs, function (log) {
                        var d = new Date(log.logdate);
                        if ((d.getYear() == d2.getYear()) && (d.getMonth() == d2.getMonth()) && (d.getDay() == d2.getDay())) {
                            $scope.logs.push(log);
                        }
                    });
                }
            }
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
            $scope.selectedprofile.securitylink = "";
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

        function getSecureLinkSucceeded(data) {
            $scope.selectedprofile.securitylink = data.link;
        }

        function addValue() {
            var log = { 'Value': $scope.item.value, 'LogDate': $scope.item.logDate, 'Message': $scope.item.message, 'ProfileId': $scope.selectedprofile.globalid };
            datacontext.save(log, addSucceeded);

            function addSucceeded(value) {
                $scope.selectedprofile.logs.push(value);
                refreshGraph();
            }

            function addFailed(error) {
                // todo
            }
        }
    }]);