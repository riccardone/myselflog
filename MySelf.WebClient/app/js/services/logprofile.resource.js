'use strict';

myselflogApp.factory('logProfileResource', ['$resource', function ($resource) {
    var service = $resource('/api/LogProfile/:id', { id: '@id' });

    //service.queryAll = function (cb) {
    //    return service.query({}, cb);
    //};

    return service;
}]);