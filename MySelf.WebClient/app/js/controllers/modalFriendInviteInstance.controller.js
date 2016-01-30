myselflogApp.controller('ModalFriendInviteController',
    function ($scope, $modalInstance) {

        //$scope.input = { 'friendInviteMessage': "Hi, you are invited to see this an health profile. Click on this link to see the profile http://www.myselflog.com/profile/" + $scope.selectedprofile.globalid };

        $scope.ok = function () {
            $modalInstance.close(); //$scope.input.friendInviteMessage);
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    });