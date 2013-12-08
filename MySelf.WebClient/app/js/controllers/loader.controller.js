myselflogApp.controller('LoaderController', ['$scope', '$http',
    function($scope, $http) {
        $scope.hasPendingRequests = function() {
            return $http.pendingRequests.length > 0;
        };
    }]);