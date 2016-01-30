'use strict';

myselflogApp.factory('friendWithLinkDatacontext', function ($http) {
    return {
        getAllLogsAsFriend: function (link, callback) {
            $http({ method: "GET", url: "/api/FriendWithLink/", params: { link: link } }).
                success(function(data) {
                    callback(data);
                });
        }
    };
});