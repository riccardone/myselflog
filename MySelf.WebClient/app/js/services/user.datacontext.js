    'use strict';
 
    myselflogApp.factory('userData', ['userResource', function (userResource) {
//    debugger;
    return {
        getCurrentUser:function(callback) {
            return userResource.get(function (user) {
                if (callback)
                    callback(user);
            });
        }
    };
}]);