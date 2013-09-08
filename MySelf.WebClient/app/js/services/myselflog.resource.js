'use strict';

myselflogApp.factory('logResource', ['$resource', function ($resource) {
    return $resource('/api/Log/:id', {}, {
        queryAll: {
            method: 'GET',
            cache: true,
            params: {},
            isArray: false
        }
    });
}]);