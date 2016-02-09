window.myselflogApp = angular.module('myselflogApp', ['ngResource', 'ngRoute', 'ui.bootstrap']);

myselflogApp.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    $routeProvider.
        when('/diary/:link', { templateUrl: '/app/views/diary.view.html', controller: 'NewFriendController' }).
        when('/profile/:id', { templateUrl: '/app/views/profile.view.html', controller: 'ProfileController' }).
        when('/', { templateUrl: 'app/views/profiles.view.html', controller: 'ProfilesController' }).
        otherwise({ redirectTo: '/' });

    $locationProvider.html5Mode({
        enabled: true,
        requireBase: false
    });
}]);

//myselflogApp.directive('dateTimepicker', function () {
//    var linker = function (scope, element, attrs) {
//        element.datetimepicker();
//    };

//    return {
//        restrict: 'A',
//        link: linker
//    };
//});

myselflogApp.directive('ccSpinner', ['$window', function ($window) {
    // Description:
    //  Creates a new Spinner and sets its options
    // Usage:
    //  <div data-cc-spinner="vm.spinnerOptions"></div>
    var directive = {
        link: link,
        restrict: 'A'
    };
    return directive;

    function link(scope, element, attrs) {
        scope.spinner = null;
        scope.$watch(attrs.ccSpinner, function (options) {
            if (scope.spinner) {
                scope.spinner.stop();
            }
            scope.spinner = new $window.Spinner(options);
            scope.spinner.spin(element[0]);
        }, true);
    }
}]);

myselflogApp.value('toastr', window.toastr);
myselflogApp.value('moment', window.moment);

//var events = {
//    controllerActivateSuccess: 'controller.activateSuccess',
//    spinnerToggle: 'spinner.toggle'
//};

//var imageSettings = {
//    imageBasePath: '../content/images/photos/',
//    unknownPersonImageSource: 'unknown_person.jpg'
//};

//var keyCodes = {
//    backspace: 8,
//    tab: 9,
//    enter: 13,
//    esc: 27,
//    space: 32,
//    pageup: 33,
//    pagedown: 34,
//    end: 35,
//    home: 36,
//    left: 37,
//    up: 38,
//    right: 39,
//    down: 40,
//    insert: 45,
//    del: 46
//};

//var config = {
//    appErrorPrefix: '[CC Error] ', //Configure the exceptionHandler decorator
//    docTitle: 'CC: ',
//    events: events,
//    imageSettings: imageSettings,
//    keyCodes: keyCodes,
//    version: '1.0.0'
//};

//myselflogApp.value('config', config);

//myselflogApp.config(['$logProvider', function ($logProvider) {
//    // turn debugging off/on (no info or warn)
//    if ($logProvider.debugEnabled) {
//        $logProvider.debugEnabled(true);
//    }
//}]);

// Configure the common services via commonConfig
//myselflogApp.config(['commonConfigProvider', function (cfg) {
//    cfg.config.controllerActivateSuccessEvent = config.events.controllerActivateSuccess;
//    cfg.config.spinnerToggleEvent = config.events.spinnerToggle;
//}]);

// http://stackoverflow.com/a/15610996
myselflogApp.filter('getByLogDate', function () {
    return function (input, logdate) {
        var i = 0, len = input.length;
        for (; i < len; i++) {
            if (+input[i].logdate == logdate) {
                return input[i];
            }
        }
        return null;
    };
});