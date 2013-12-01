'use strict';

myselflogApp.factory('securityLinkResource', ['$resource', function ($resource) {
    return $resource('/api/SecurityLink/:id', {}, {
        queryAll: { method: 'GET', params: {}, isArray: false }
    });
}]);