'use strict';

myselflogApp.factory('friendResource', ['$resource', function ($resource) {
    return $resource('/api/Friend/:id', {}, {
        queryAll: { method: 'GET', params: {}, isArray: false }
    });
}]);