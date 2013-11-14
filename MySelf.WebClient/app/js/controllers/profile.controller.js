myselflogApp.controller('ProfileController',
    ['$scope', 'datacontext', '$filter', '$routeParams', '$modal', '$log',
    function ($scope, datacontext, $filter, $routeParams, $modal, $log) {
        $scope.loading = false;
        $scope.id = $routeParams.id;
        $scope.addValue = addValue;
        $scope.remove = remove;
        $scope.logs = [];
        $scope.friends = [];
        //$scope.graph = Morris.Line({
        //    element: 'diaryGraph',
        //    xkey: 'logdate',
        //    ykeys: ['value'],
        //    labels: ['Diary']
        //});
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
                controller: 'ModalSendInviteController',
                resolve: {
                    items: function () {
                        return $scope.message;
                    }
                }
            });

            modalInstance.result.then(function (selectedItem) {
                $scope.message = selectedItem;
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
            $scope.item = { "value": "", "logDate": getJustToday(), "logTime": getNow(), "message": "" };
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
            refreshGraph();
        }

        function setReport(date, report) {
            $scope.report = report;
            $scope.date = date;
            refreshGraph();
        }

        function refreshGraph() {
            if ($scope.selectedprofile) {
                //var logs = [];
                var d2 = new Date($scope.date);
                if ($scope.report == "year") {
                    angular.forEach($scope.selectedprofile.logs, function (log) {
                        var d = new Date(log.logdate);
                        if (d.getYear() == d2.getYear()) {
                            $scope.logs.push(log);
                        }
                    });
                }
                if ($scope.report == "month") {
                    angular.forEach($scope.selectedprofile.logs, function (log) {
                        var d = new Date(log.logdate);
                        if ((d.getYear() == d2.getYear()) && (d.getMonth() == d2.getMonth())) {
                            $scope.logs.push(log);
                        }
                    });
                }
                if ($scope.report == "day") {
                    angular.forEach($scope.selectedprofile.logs, function (log) {
                        var d = new Date(log.logdate);
                        if ((d.getYear() == d2.getYear()) && (d.getMonth() == d2.getMonth()) && (d.getDay() == d2.getDay())) {
                            $scope.logs.push(log);
                        }
                    });
                }
            }
        }

        $scope.alerts = [];
        
        $scope.addAlert = function (message) {
            $scope.alerts.push({ msg: message });
        };
        
        $scope.closeAlert = function (index) {
            $scope.alerts.splice(index, 1);
        };

        $scope.$watch('loading', function () {
            if ($scope.loading == false) {
                if ($scope.alerts.length > 0) {
                    $scope.closeAlert(0);
                }
            } else {
                $scope.addAlert("Processing data, please wait...");
            }
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

        function remove(log) {
            datacontext.remove(log, removeLogSucceeded);
        }

        function removeLogSucceeded(log) {
            for (var i = 0; i < $scope.selectedprofile.logs.length; i++) {
                if ($scope.selectedprofile.logs[i].globalid == log.globalid) {
                    $scope.selectedprofile.logs.splice(i, 1);
                    refreshGraph();
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

            var log = { 'Value': $scope.item.value, 'LogDate': d1, 'Message': $scope.item.message, 'ProfileId': $scope.selectedprofile.globalid };
            datacontext.save(log, addSucceeded);

            function addSucceeded(value) {
                $scope.selectedprofile.logs.push(value);
                refreshGraph();
                $scope.loading = false;
                resetItem();
            }

            function addFailed(error) {
                // todo
                $scope.loading = false;
            }
        }
    }]);