myselflogApp.controller('ModalFriendInviteController',
    function ($scope, $modalInstance) {

        $scope.input = {};

        $scope.ok = function () {
            $modalInstance.close($scope.input.friendInviteMessage);
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    });