(function () {
    'use strict';

    // Factory name is handy for logging
    var serviceId = 'model';

    // Define the factory on the module.
    // Inject the dependencies. 
    // Point to the factory definition function.
    angular.module('app').factory(serviceId, model);

    function model() {
        // Define the functions and properties to reveal.
        var entityNames = {
            attendee: 'Person',
            person: 'Person',
            speaker: 'Person',
            session: 'Session',
            room: 'Room',
            track: 'Track',
            timeslot: 'TimeSlot'
        };

        var service = {
            configureMetadataStore: configureMetadataStore,
            entityNames: entityNames
        };

        return service;

        function configureMetadataStore(metadataStore) {
            registerPerson(metadataStore);
            registerSession(metadataStore);
            registerTimeSlot(metadataStore);
        }

        //#region Internal Methods        
        
        function registerPerson(metadataStore) {
            metadataStore.registerEntityTypeCtor('Person', Person);

            function Person() {
                this.isSpeaker = false;
            }

            Object.defineProperty(Person.prototype, 'fullName', {
                get: function () {
                    var fn = this.firstName;
                    var ln = this.lastName;
                    return ln ? fn + ' ' + ln : fn;
                }
            });
        }


        function registerSession(metadataStore) {
            metadataStore.registerEntityTypeCtor('Session', Session);

            function Session() { }

            Object.defineProperty(Session.prototype, 'tagsFormatted', {
                get: function () {
                    return this.tags ? this.tags.replace(/\|/g, ', ') : this.tags;
                },
                set: function (value) {
                    this.tags = value.replace(/\, /g, '|');
                }
            });
        }
        
        function registerTimeSlot(metadataStore) {
            metadataStore.registerEntityTypeCtor('TimeSlot', TimeSlot);
            
            function TimeSlot() { }

            Object.defineProperty(TimeSlot.prototype, 'name', {
               get : function() {
                   // formatted dates are good!
                   var start = this.start;
                   var value = moment.utc(start).format('ddd hh:mm a');
                   return value;
               } 
            });

        }

        //#endregion
    }
})();