'use strict';

myselflogApp.factory('friendInviteResource', ['$resource', function ($resource) {
    return $resource('/api/FriendInvite/:check', { }, {
        //query: { method: 'GET', params: { 'logProfileId': logProfileId, 'email': email }, isArray: false }
    });
}]);