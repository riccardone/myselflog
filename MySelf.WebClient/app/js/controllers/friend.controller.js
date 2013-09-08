friendlogApp.controller('FriendController',
    ['$scope', 'friendDatacontext', 'logger',
    function ($scope, friendDatacontext, logger) {
        $scope.loading = false;
        $scope.error = "";
        $scope.logprofilesasfriend = [];
        $scope.getData = getData;
        $scope.link = "";
        $scope.graph = Morris.Line({
            element: 'diaryGraph',
            xkey: 'logdate',
            ykeys: ['value'],
            labels: ['Diary']
        });
        
        loadData();
        
        function loadData() {
            readLinkElement();
            getData();
        }
        
        function readLinkElement() {
            $scope.link = $("#link").val();
        }

        function getData() {
            $scope.loading = true;
            $scope.error = "";
            if ($scope.link && $scope.link.length > 0) {
                var link = $scope.link;
                friendDatacontext.getAllLogsAsFriend(link, getDataSucceeded);
            } else {
                $scope.error = "link not found";
            }
        }

        function getDataSucceeded(data) {
            $scope.logprofilesasfriend.push(data.logprofileasfriend);
            if ($scope.logprofilesasfriend.length > 0) {
                var logs = $scope.logprofilesasfriend[0].logs;
                $scope.graph.setData(logs);
                logger.info("logs loaded");
            }
            $scope.loading = false;
        }
    }]);