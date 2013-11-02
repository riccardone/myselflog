window.myselflogApp = angular.module('myselflogApp', ['ui.bootstrap', 'ngResource']);

myselflogApp.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    $routeProvider.
        when('/', { templateUrl: 'app/views/test.view.html', controller: 'TestController' }).
        otherwise({ redirectTo: '/' });
    $locationProvider.html5Mode(true);
}]);