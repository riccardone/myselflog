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
        $scope.item.logDate = $filter('date')(new Date(), 'yyyy-MM-dd HH:mm:ss');

        setData();

        function setData() {
            //var logid = $routeParams.id;
            if (!$scope.selectedprofile) {
                datacontext.getLog($scope.id, setDataSucceeded);
            }
        }
        
        function setDataSucceeded(data) {
            $scope.selectedprofile = data;
            if ($scope.selectedprofile) {
                $scope.graph.setData($scope.selectedprofile.logs);
                //var now = $filter('date')(new Date(), 'yyyy-MM-dd HH:mm:ss');
                //$scope.item.logDate = now;
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
            var today = $filter('date')(new Date(), 'yyyy-MM-dd HH:mm:ss');
            var log = { 'Value': $scope.item.value, 'LogDate': today, 'Message': $scope.item.message, 'ProfileId': $scope.selectedprofile.globalid };
            datacontext.save(log, addSucceeded);

            function addSucceeded(value) {
                $scope.selectedprofile.logs.push(value);
                $scope.graph.setData($scope.selectedprofile.logs);
            }

            function addFailed(error) {
                // todo
            }
        }
    }]);