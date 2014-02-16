﻿(function () { 
    'use strict';
    
    var controllerId = 'sidebar';
    angular.module('app').controller(controllerId,
        ['$location', '$route', 'config', 'routes', sidebar]);

    function sidebar($location, $route, config, routes) {
        var vm = this;
        var keyCodes = config.keyCodes;

        vm.isCurrent = isCurrent;
        vm.search = search;
        vm.searchText = '';

        activate();

        function activate() { getNavRoutes(); }
        
        function getNavRoutes() {
            vm.navRoutes = routes.filter(function(r) {
                return r.config.settings && r.config.settings.nav;
            }).sort(function(r1, r2) {
                return r1.config.settings.nav > r2.config.settings.nav;
            });
        }
        
        function isCurrent(route) {
            if (!route.config.title || !$route.current || !$route.current.title) {
                return '';
            }
            var menuName = route.config.title;
            return $route.current.title.substr(0, menuName.length) === menuName ? 'current' : '';
        }
        
        function search($event) {
            if ($event.keyCode === keyCodes.esc) {
                vm.searchText = '';
                return;
            }
            
            if ($event.type === 'click' || $event.keyCode === keyCodes.enter) {
                var route = '/sessions/search/';
                $location.path(route + vm.searchText);
            }
        }

    };
})();