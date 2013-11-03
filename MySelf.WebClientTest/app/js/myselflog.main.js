﻿window.myselflogApp = angular.module('myselflogApp', ['ui.bootstrap', 'ngResource', 'n3-charts.linechart']);

myselflogApp.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    $routeProvider.
        //when('/', { templateUrl: 'app/views/test.view.html', controller: 'TestController' }).
        when('/', { templateUrl: 'app/views/chart.view.html', controller: 'ChartController' }).
        otherwise({ redirectTo: '/' });
    $locationProvider.html5Mode(true);
}]);

myselflogApp.directive("modalShow", function () {
    return {
        restrict: "A",
        scope: {
            modalVisible: "="
        },
        link: function (scope, element, attrs) {

            //Hide or show the modal
            scope.showModal = function(visible) {
                if (visible) {
                    element.modal("show");
                } else {
                    element.modal("hide");
                }
            };

            //Check to see if the modal-visible attribute exists
            if (!attrs.modalVisible) {

                //The attribute isn't defined, show the modal by default
                scope.showModal(true);

            }
            else {

                //Watch for changes to the modal-visible attribute
                scope.$watch("modalVisible", function (newValue, oldValue) {
                    scope.showModal(newValue);
                });

                //Update the visible value when the dialog is closed through UI actions (Ok, cancel, etc.)
                element.bind("hide.bs.modal", function () {
                    scope.modalVisible = false;
                    if (!scope.$$phase && !scope.$root.$$phase)
                        scope.$apply();
                });

            } b

        }
    };

});