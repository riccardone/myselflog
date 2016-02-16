'use strict';

myselflogApp.controller('LoginController',
    function LoginController($scope, $location, userData, authService) {
        $scope.user = { userName: "", password: "" };
        $scope.login = function () {
            userData.getCurrentUser(function (user) {
                if (!!user) {
                    authService.setCurrentUser(user);
                    $location.url('/');
                }
            });
        };
        
        $scope.login();
    }
);