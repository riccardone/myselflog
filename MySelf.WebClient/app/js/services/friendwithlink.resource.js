'use strict';

myselflogApp.factory('friendWithLinkResource', ['$resource', function ($resource) {
    return $resource('/api/FriendWithLink/:link', {}, {
        queryAll: { method: 'GET', params: {}, isArray: false }
    });
}]);