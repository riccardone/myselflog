myselflogApp.controller('ProfileController',
    ['$scope', 'datacontext', '$filter', '$routeParams', '$modal', '$log',
    function ($scope, datacontext, $filter, $routeParams, $modal, $log) {
        $scope.loading = false;
        $scope.id = $routeParams.id;
        $scope.addValue = addValue;
        $scope.remove = remove;
        $scope.removeTerapy = removeTerapy;
        $scope.logs = [];
        $scope.friends = [];
        $scope.isTerapyCollapsed = true;
        $scope.oneAtATime = true;
        $scope.hstep = 1;
        $scope.mstep = 1;
        $scope.ismeridian = true;
        $scope.changed = function () {
            console.log('Time changed to: ' + $scope.item.logTime);
        };
        $scope.resetItem = resetItem;

        /* modal */
        $scope.openInvite = function (email) {

            var modalInstance = $modal.open({
                templateUrl: 'mySendInviteModalContent.html',
                controller: 'ModalFriendInviteController',
                resolve: {
                    items: function () {
                        return {}; 
                    }
                }
            });

            modalInstance.result.then(function (message) {
                datacontext.sendInvite($scope.selectedprofile.globalid, email, "");
            }, function () {
                $log.info('Modal dismissed at: ' + new Date());
            });
        };
        /* end modal */

        /* date picker */
        $scope.today = function () {
            $scope.logdate = getNow();
        };
        $scope.today();

        $scope.showWeeks = true;
        $scope.toggleWeeks = function () {
            $scope.showWeeks = !$scope.showWeeks;
        };

        $scope.clear = function () {
            $scope.logdate = null;
        };

        // Disable weekend selection
        $scope.disabled = function (date, mode) {
            return (mode === 'day' && (date.getDay() === 0 || date.getDay() === 6));
        };

        $scope.toggleMin = function () {
            $scope.minDate = ($scope.minDate) ? null : new Date();
        };
        $scope.toggleMin();

        $scope.open = function () {
            $timeout(function () {
                $scope.opened = true;
            });
        };

        $scope.dateOptions = {
            'year-format': "'yy'",
            'starting-day': 1,
            'showWeeks': false
        };
        /* end date picker */

        $scope.item = {};
        $scope.report = "year";
        $scope.date = getJustToday();
        $scope.setReport = setReport;
        $scope.myOptions = { data: 'logs' };

        function resetItem() {
            $scope.item = {}; //{ "value": null, "logDate": getJustToday(), "logTime": getNow(), "message": null };
            $scope.item.logDate = getJustToday();
            $scope.item.logTime = getNow();
        }

        function getNow() {
            return $filter('date')(new Date(), 'yyyy-MM-dd HH:mm:ss');
        }
        
        function getJustToday() {
            return $filter('date')(new Date(), 'yyyy-MM-dd');
        }

        $scope.item.logDate = getJustToday();

        setData();

        function setData() {
            if (!$scope.selectedprofile) {
                datacontext.getLog($scope.id, setDataSucceeded);
            }
        }

        function setDataSucceeded(data) {
            $scope.selectedprofile = data;
            $scope.friends = data.friends;
        }

        function setReport(date, report) {
            $scope.report = report;
            $scope.date = date;
        }

        $scope.$watch('loading', function () {
<<<<<<< HEAD
           
=======
            
>>>>>>> 154c55165a6b1327326f4e47030724f74799ea15
        });

        $scope.$watch('selectedprofile', function () {
            setData();
        });

        $scope.createProfile = function () {
            datacontext.createProfile("Default", createProfileSucceeded);
        };

        $scope.addSecurityLink = function (logProfileId) {
            datacontext.addSecurityLink(logProfileId, getSecureLinkSucceeded);
        };

        $scope.deleteSecurityLink = function (logProfileId) {
            datacontext.deleteSecurityLink(logProfileId, deleteSecurityLinkSucceeded);
        };
        
        function deleteSecurityLinkSucceeded() {
            $scope.selectedprofile.securitylink = "";
        }

        $scope.removeFriend = function (email) {
            $scope.loading = true;
            datacontext.deleteFriend(email, $scope.selectedprofile.globalid, deleteFriendSucceeded);
        };

        function deleteFriendSucceeded(email) {
            $scope.friends.splice();
            for (var i = 0; i < $scope.friends.length; i++) {
                if ($scope.friends[i].email == email) {
                    $scope.friends.splice(i, 1);
                }
            }
            $scope.loading = false;
        }

        $scope.addFriend = function (email, logProfileGlobalId) {
            $scope.loading = true;
            datacontext.addFriend(email, logProfileGlobalId, addFriendSucceeded);
        };
        
        function addFriendSucceeded(data) {
            $scope.friends.push(data);
            $scope.loading = false;
        }

        function createProfileSucceeded() {
            $scope.getData();
        }
        
        function removeTerapy(terapy) {
            datacontext.removeTerapy(terapy, removeTerapySucceeded);
        }
        
        function removeTerapySucceeded(terapy) {
            for (var i = 0; i < $scope.selectedprofile.terapies.length; i++) {
                if ($scope.selectedprofile.terapies[i].terapyglobalid == terapy.terapyglobalid) {
                    $scope.selectedprofile.terapies.splice(i, 1);
                }
            }
        }

        function remove(log) {
            datacontext.remove(log, removeLogSucceeded);
        }

        function removeLogSucceeded(log) {
            for (var i = 0; i < $scope.selectedprofile.logs.length; i++) {
                if ($scope.selectedprofile.logs[i].globalid == log.globalid) {
                    $scope.selectedprofile.logs.splice(i, 1);
                }
            }
        }

        function getSecureLinkSucceeded(data) {
            $scope.selectedprofile.securitylink = data.link;
        }

        function addValue() {
            $scope.loading = true;

            var d1 = moment($scope.item.logDate).toDate();
            var d2 = moment($scope.item.logTime).toDate();
            
            d1.setHours(d2.getHours());
            d1.setMinutes(d2.getMinutes());
            d1.setSeconds(d2.getSeconds());
            d1.setSeconds(d2.getMilliseconds());

            var log = {
                'Value': $scope.item.value,
                'LogDate': d1,
                'Message': $scope.item.message,
                'ProfileId': $scope.selectedprofile.globalid,
                'isslow': $scope.item.isslow,
                'terapyvalue': $scope.item.terapyvalue
            };
            datacontext.save(log, addSucceeded);

            function addSucceeded(value) {
                if (value.globalid) {
                    $scope.selectedprofile.logs.push(value);
<<<<<<< HEAD
                    //refreshGraph();
=======
>>>>>>> 154c55165a6b1327326f4e47030724f74799ea15
                }
                if (value.terapyglobalid) {
                    $scope.selectedprofile.terapies.push(value);
                }
                $scope.loading = false;
                resetItem();
            }

            function addFailed(error) {
                // todo
                $scope.loading = false;
            }
        }
    }]);