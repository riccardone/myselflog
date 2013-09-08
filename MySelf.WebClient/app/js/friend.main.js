window.friendlogApp = angular.module('friendlogApp', ['ngResource'], function($locationProvider) {
    $locationProvider.html5Mode(true);
});

friendlogApp.value('toastr', window.toastr);