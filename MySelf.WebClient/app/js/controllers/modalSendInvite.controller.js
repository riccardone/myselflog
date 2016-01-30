myselflogApp.controller('ModalSendInviteController',
    function($scope, $modalInstance) {
        
        $scope.ok = function () {
            $modalInstance.close($scope.message);
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    });