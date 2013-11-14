myselflogApp.controller('ModalInstanceController',
    function($scope, $modalInstance) {
        $scope.input = {};
        $scope.ok = function () {
            alert($scope.message);
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    });