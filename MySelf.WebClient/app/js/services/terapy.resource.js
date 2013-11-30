'use strict';

myselflogApp.factory('terapyResource', ['$resource', function ($resource) {
    return $resource('/api/Terapy/:id', { }, {
        //query: { method: 'GET', params: { 'logProfileId': logProfileId, 'email': email }, isArray: false }
    });
}]);