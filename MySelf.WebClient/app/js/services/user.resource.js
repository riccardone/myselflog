'use strict';

myselflogApp.factory('userResource', ['$resource', function ($resource) {
    var service = $resource('/api/user/:userName', { userName: '@userName' }, {});

    service.queryAll = function (callback) {
        return service.query({}, callback);
    };

    return service;
}]);