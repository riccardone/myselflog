window.myselflogApp = angular.module('myselflogApp', ['ngResource']);

myselflogApp.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    $routeProvider.
        //when('/diary/:link', { templateUrl: '/app/views/friend.view.html', controller: 'FriendController' }).
        when('/profile/:id', { templateUrl: '/app/views/profile.view.html', controller: 'ProfileController' }).
        when('/', { templateUrl: 'app/views/profiles.view.html', controller: 'ProfilesController' }).
        otherwise({ redirectTo: '/' });
    $locationProvider.html5Mode(true);
}]);

myselflogApp.value('toastr', window.toastr);