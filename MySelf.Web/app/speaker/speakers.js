﻿(function () {
    'use strict';

    // Controller name is handy for logging
    var controllerId = 'speakers';

    // Define the controller on the module.
    // Inject the dependencies. 
    // Point to the controller definition function.
    angular.module('app').controller(controllerId,
        ['common', 'config', 'datacontext', speakers]);

    function speakers(common, config, datacontext) {
        // Using 'Controller As' syntax, so we assign this to the vm variable (for viewmodel).
        var vm = this;
        var keyCodes = config.keyCodes;
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        // Bindable properties and functions are placed on vm.
        vm.filteredSpeakers = [];
        vm.refresh = refresh;
        vm.search = search;
        vm.speakerSearch = '';
        vm.speakers = [];
        vm.title = 'Speakers';

        activate();

        function activate() {
            common.activateController([getSpeakers()], controllerId)
                .then(function () { log('Activated Speakers View'); });
        }

        function getSpeakers(forceRefresh) {
            return datacontext.getSpeakerPartials(forceRefresh)
                .then(function (data) {
                    vm.speakers = data;
                    applyFilter();
                    return vm.speakers;
                });
        }

        function refresh() { getSpeakers(true); }
        
        function search($event) {
            if ($event.keyCode === keyCodes.esc) {
                vm.speakerSearch = '';
            }
            applyFilter();
        }
        
        function applyFilter() {
            vm.filteredSpeakers = vm.speakers.filter(speakerFilter);
        }
        
        function speakerFilter(speaker) {
            var isMatch = vm.speakerSearch
                ? common.textContains(speaker.fullName, vm.speakerSearch)
                : true;
            return isMatch;
        }
    }
})();
