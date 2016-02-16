'use strict';

myselflogApp.factory('friendDatacontext',
    ['$http',
    function ($http) {
    return {
        getAllLogsAsFriend: function (link, callback) {
            $http({ method: "GET", url: "/api/Friend/", params: { id: id } }).
                success(function(data) {
                    callback(data);
                });
        }
    };
}]);