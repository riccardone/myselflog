'use strict';

myselflogApp.factory('friendWithLinkDatacontext',
    ['$http', 
    function ($http) {
    return {
        getAllLogsAsFriend: function (link, callback) {
            $http({ method: "GET", url: "/api/FriendWithLink/", params: { link: link } }).
                success(function(data) {
                    callback(data);
                });
        }
    };
}]);