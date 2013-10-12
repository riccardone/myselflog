'use strict';

myselflogApp.factory('friendDatacontext', function ($http) {
    return {
        getAllLogsAsFriend: function (link, callback) {
            $http({ method: "GET", url: "/api/Friend/", params: { link: link } }).
                success(function(data) {
                    callback(data);
                });
        }
    };
});