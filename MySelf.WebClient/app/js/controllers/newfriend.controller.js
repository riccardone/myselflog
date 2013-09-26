friendlogApp.controller('NewFriendController',
    ['$scope', 'friendDatacontext', 'logger', '$filter',
    function ($scope, friendDatacontext, logger, $filter) {
		$scope.loading = false;
		$scope.profile = { 'isLoaded': false };
		$scope.graph = Morris.Line({
				element: 'diaryGraph',
				xkey: 'logdate',
				ykeys: ['value'],
				labels: ['Diary']
			});
		$scope.setReport = setReport;
        $scope.report = "day";
        $scope.date = getNow();
        $scope.link = "";

		function getNow() {
            return toDateString(new Date()); 
        }

		function toDateString(date) {
            return $filter('date')(date, 'yyyy-MM-dd HH:mm:ss');
        }

		function setReport(date, report) {
            $scope.report = report;
            $scope.date = date;
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
            $scope.profile.push(data.logprofileasfriend);
            $scope.loading = false;
        }

		loadData();

		function loadData() {
		    readLinkElement();
		    getData();
		}

		function readLinkElement() {
		    $scope.link = $("#link").val();
		}
	}]);