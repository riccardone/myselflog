window.myselflogApp = angular.module('myselflogApp', ['ngResource']);

myselflogApp.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    $routeProvider.
        when('/diary/:link', { templateUrl: '/app/views/diary.view.html', controller: 'NewFriendController' }).
        when('/profile/:id', { templateUrl: '/app/views/profile.view.html', controller: 'ProfileController' }).
        when('/', { templateUrl: 'app/views/profiles.view.html', controller: 'ProfilesController' }).
        otherwise({ redirectTo: '/' });
    $locationProvider.html5Mode(true);
}]);

myselflogApp.directive('dateTimepicker', function () {
    var linker = function(scope, element, attrs) {
        element.datetimepicker();
    };

    return {
        restrict: 'A',
        link: linker
    };
});

myselflogApp.value('toastr', window.toastr);