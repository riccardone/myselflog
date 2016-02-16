myselflogApp.controller('ProfileController',
    ['$scope', 'datacontext', '$filter', '$routeParams', '$log', 'logger', 
    function ($scope, datacontext, $filter, $routeParams, $log, logger) {
        $scope.loading = false;
        $scope.id = $routeParams.id;
        $scope.addValue = addValue;
        $scope.remove = remove;
	    $scope.removeTerapy = removeTerapy;
        $scope.logs = [];
        $scope.friends = [];
        $scope.isTerapyCollapsed = true;
        $scope.isFoodCollapsed = true;
        $scope.oneAtATime = true;
        $scope.hstep = 1;
        $scope.mstep = 1;
        $scope.ismeridian = true;
        $scope.isBusy = true;
        $scope.changed = function () {
            console.log('Time changed to: ' + $scope.item.logTime);
        };
        $scope.resetItem = resetItem;
        $scope.foodTypes = ["Snack", "Fruit"]; //, "Lunch", "Dinner", "Chocholate", "Fruit", "Nuts", "Cake"];
        $scope.getFoodTypes = function () {
            return $scope.foodTypes;
        };
        $scope.checkFood = function (value, checked) {
            var idx = -1;
            if ($scope.item.foodTypes) {
                idx = $scope.item.foodTypes.indexOf(value);
                if (idx >= 0 && !checked) {
                    $scope.item.foodTypes.splice(idx, 1);
                } else {
                    $scope.item.foodTypes = [];
                }
            }
            
            if (idx < 0 && checked) {
                $scope.item.foodTypes.push(value);
            }
        };

        /* modal */
        //$scope.openInvite = function (email) {
        //    var modalInstance = $modal.open({
        //        templateUrl: 'mySendInviteModalContent.html',
        //        controller: 'ModalFriendInviteController',
        //        resolve: {
        //            items: function () {
        //                return {}; 
        //            }
        //        }
        //    });

        //    modalInstance.result.then(function (message) {
        //        datacontext.sendInvite($scope.selectedprofile.globalid, email, "");
        //    }, function () {
        //        $log.info('Modal dismissed at: ' + new Date());
        //    });
        //};
        /* end modal */
        
        //function toggleSpinner(on) { $scope.isBusy = on; }
        
        //$rootScope.$on(events.controllerActivateSuccess,
        //    function (data) { toggleSpinner(false); }
        //);

        //$rootScope.$on(events.spinnerToggle,
        //    function (data) { toggleSpinner(data.show); }
        //);

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

        $scope.item = { foodTypes: [] };
        $scope.report = "year";
        $scope.date = getJustToday();
        $scope.setReport = setReport;
        $scope.myOptions = { data: 'logs' };

        function resetItem() {
            $scope.item = { foodTypes: [] }; //{ "value": null, "logDate": getJustToday(), "logTime": getNow(), "message": null };
            $scope.item.logDate = getJustToday();
            $scope.item.logTime = getNow();
            $scope.valuemmol = null;
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
            logger.success("Data loaded from remote source");
        }

        function setReport(date, report) {
            $scope.report = report;
            $scope.date = date;
        }

        $scope.$watch('loading', function () {
           
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

        $scope.convertFromMmol = function () {
            if ($scope.valuemmol) {
                $scope.item.value = Math.round($scope.valuemmol * 18.0182);
            }
        }

        function addValue() {
            $scope.loading = true;
            
            // Log blood sugar level
            if ($scope.item.value > 0) {
                var log = {
                    'value': $scope.item.value,
                    'LogDate': moment().toDate(),
                    'Message': '',
                    'ProfileId': $scope.selectedprofile.globalid,
                    'isslow': true,
                    'terapyvalue': $scope.item.slowvalue,
                    'calories': 0,
                    'foodTypes': []
                };
                log.Value = $scope.item.value;
                datacontext.save(log, addSucceeded);
                log.value = 0;
            }
            // Log slow terapy
            if ($scope.item.slowvalue > 0) {
                var logSlow = {
                    'value': 0,
                    'LogDate': moment().toDate(),
                    'Message': '',
                    'ProfileId': $scope.selectedprofile.globalid,
                    'isslow': true,
                    'terapyvalue': $scope.item.slowvalue,
                    'calories': 0,
                    'foodTypes': []
                };
                datacontext.save(logSlow, addSucceeded);
            }
            // Log fast terapy
            if ($scope.item.fastvalue > 0) {
                var logFast = {
                    'value': 0,
                    'LogDate': moment().toDate(),
                    'Message': '',
                    'ProfileId': $scope.selectedprofile.globalid,
                    'isslow': false,
                    'terapyvalue': $scope.item.fastvalue,
                    'calories': 0,
                    'foodTypes': []
                };
                datacontext.save(logFast, addSucceeded);
            }

            if ($scope.item.calories > 0) {
                var logFood = {
                    'value': 0,
                    'LogDate': moment().toDate(),
                    'Message': '',
                    'ProfileId': $scope.selectedprofile.globalid,
                    'isslow': false,
                    'terapyvalue': 0,
                    'calories': 0,
                    'foodTypes': []
                };
                for (var i = 0; i < $scope.foodTypes.length; i++) {
                    if ($scope.item.foodTypes[$scope.foodTypes[i]]) {
                        logFood.foodTypes.push($scope.foodTypes[i]);
                    }
                }
                logFood.calories = $scope.item.calories;
                datacontext.save(logFood, addSucceeded);
            }
            
            function addSucceeded(value) {
                if (value.globalid) {
                    $scope.selectedprofile.logs.push(value);
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