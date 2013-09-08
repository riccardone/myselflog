myselflogApp.controller('LogController',
    ['$scope', 'datacontext', '$filter',
    function ($scope, datacontext, $filter) {
        $scope.error = "";
        $scope.addValue = addValue;
        $scope.clearErrorMessage = clearErrorMessage;
        $scope.logprofilesasowner = [];
        $scope.logprofilesasfriend = [];
        $scope.getData = getData;
        $scope.remove = remove;
        $scope.securityLink = "";

        $scope.addSecurityLink = function(logProfileId) {
            datacontext.addSecurityLink(logProfileId, getSecureLinkSucceeded);
        };
        
        $scope.deleteSecurityLink = function (logProfileId) {
            datacontext.deleteSecurityLink(logProfileId, deleteSecurityLinkSucceeded);
        };
        
        function deleteSecurityLinkSucceeded() {
            $scope.selectedprofile.securityLink = "";
        }

        $scope.createProfile = function() {
            datacontext.createProfile("Default", createProfileSucceeded);
        };
        
        function refresh() {
            $scope.getData();
        }

        $scope.addFriend = function(email, logProfileGlobalId) {
            datacontext.addFriend(email, logProfileGlobalId);
        };
        
        function createProfileSucceeded() {
            $scope.getData();
        }
        
        refresh();
        
        function remove(log) {
            datacontext.remove(log, removeLogSucceeded);
        }
        
        function removeLogSucceeded(log) {
            refresh();
        }
        
        function refreshSecurityLink(logprofileid) {
            datacontext.getSecureLink(logprofileid, getSecureLinkSucceeded);
        }

        function getData() {
            datacontext.getAllLogs(getDataSucceeded);
        }

        function getSecureLinkSucceeded(data) {
            $scope.selectedprofile.securityLink = data.link;
        }
        
        $scope.$watch('selectedprofile', function () {
            if ($scope.selectedprofile) {
                $scope.logs = $scope.selectedprofile.logs;
                refreshSecurityLink($scope.selectedprofile.globalid);
            }
        }); // initialize the watch
        
        function getDataSucceeded(data) {
            $scope.logprofilesasfriend = data.logprofilesasfriend;
            $scope.logprofilesasowner = data.logprofilesasowner;
            if ($scope.logprofilesasowner && $scope.logprofilesasowner.length > 0) {
                
            }
        }
        
        function addValue() {
            var today = $filter('date')(new Date(), 'yyyy-MM-dd HH:mm:ss');
            var log = { 'Value': $scope.item.value, 'LogDate': today, 'Message': $scope.item.message, 'ProfileId': $scope.item.selectedProfileId };
            datacontext.save(log, addSucceeded);

            function addSucceeded(value) {
                $scope.logprofilesasowner[0].logs.push(value);
                if ($scope.logprofilesasowner && $scope.logprofilesasowner.length > 0) {
                    $scope.graph.setData($scope.logprofilesasowner[0].logs);
                }
            }

            function addFailed(error) {
                failed({ message: "Save of new value failed" });
            }
        }
        
        function failed(error) {
            $scope.error = error.message;
        }
        
        function clearErrorMessage(obj) {
            if (obj && obj.errorMessage) {
                obj.errorMessage = null;
            }
        }
    }]);