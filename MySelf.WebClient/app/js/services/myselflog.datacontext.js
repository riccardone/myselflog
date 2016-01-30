'use strict';

myselflogApp.factory('datacontext', function (logResource, logProfileResource, securityLinkResource, friendResource, friendWithLinkResource, friendInviteResource, terapyResource) {
    return {
        getLog: function (logId, callback) {
            return logResource.get({ id: logId }, function (log) {
                if (callback)
                    callback(log);
            });
        },
        getAllLogs: function (callback) {
            return logResource.queryAll(callback);
        },
        save: function (log, callback) {
            logResource.save(log, function (data) {
                if (callback)
                    callback(data);
            });
        },
        remove: function (log, callback) {
            logResource.delete({ globalid: log.globalid }, function (data) {
                if (callback)
                    callback(log);
            });
        },
        removeTerapy: function(terapy, callback) {
            terapyResource.delete({ globalid: terapy.terapyglobalid }, function () {
                if (callback)
                    callback(terapy);
            });
        },
        createProfile: function(name, callback) {
            logProfileResource.save({ name: name }, function (data) {
                if (callback)
                    callback(data);
            });
        },
        addFriend: function (email, logProfileId, callback) {
            friendResource.save({ email: email, logProfileId: logProfileId }, function (data) {
                if (callback)
                    callback(data);
            });
        },
        deleteFriend: function(email, logProfileId, callback) {
            friendResource.delete({ logprofileid: logProfileId, email: email }, function () {
                if (callback)
                    callback(email);
            });
        },
        getFriends: function (logProfileId, callback) {
            return friendResource.query({ id: logProfileId }, function(data) {
                if (callback) {
                    callback(data);
                }
            });
        },
        getSecureLink: function (logProfileId, callback) {
            securityLinkResource.get({ logProfileId: logProfileId }, function (data) {
                if (callback)
                    callback(data);
            });
        },
        addSecurityLink: function(logProfileId, callback) {
            securityLinkResource.save({ logProfileId: logProfileId }, function (data) {
                if (callback)
                    callback(data);
            });
        },
        deleteSecurityLink: function (logProfileId, callback) {
            securityLinkResource.delete({ globalId: logProfileId }, function (data) {
                if (callback)
                    callback();
            });
        },
        sendInvite: function (logprofileid, email, message, callback) {
            friendInviteResource.save({ logprofileid: logprofileid, email: email, message: message }), function (data) {
                if (callback)
                    callback(data);
            };
        }
    };
});