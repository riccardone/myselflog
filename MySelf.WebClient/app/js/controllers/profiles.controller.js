myselflogApp.controller('ProfilesController',
    ['$scope', 'datacontext', '$location',
    function ($scope, datacontext, $location) {
        $scope.logprofilesasowner = [];
        $scope.logprofilesasfriend = [];
        $scope.getData = getData;

        function refresh() {
            $scope.getData();
        }

        refresh();

        function getData() {
            datacontext.getAllLogs(getDataSucceeded);
        }

        function getDataSucceeded(data) {
            $scope.logprofilesasfriend = data.logprofilesasfriend;
            $scope.logprofilesasowner = data.logprofilesasowner;
            if ($scope.logprofilesasowner.length == 1 && $scope.logprofilesasfriend.length == 0) {
                $location.path('/profile/' + $scope.logprofilesasowner[0].globalid);
            }
        }

        $scope.createProfile = function () {
            datacontext.createProfile("Default", createProfileSucceeded);
        };

        function createProfileSucceeded() {
            $scope.getData();
        }
    }]);