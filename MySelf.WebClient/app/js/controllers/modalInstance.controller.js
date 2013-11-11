myselflogApp.controller('ModalInstanceController',
    function($scope, $modalInstance) {
        $scope.input = {};
        $scope.ok = function () {
            alert($scope.input.abc);
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    });