friendlogApp.controller('NewFriendController',
    ['$scope', 'friendDatacontext', 'logger', '$filter', 'moment',
    function ($scope, friendDatacontext, logger, $filter, moment) {
		$scope.loading = false;
		$scope.profile = { 'isLoaded': false };
		$scope.graph = Morris.Line({
				element: 'diaryGraph',
				xkey: 'logdate',
				ykeys: ['value'],
				labels: ['Diary']
			});
		$scope.setReport = setReport;
        $scope.date = moment();
        $scope.link = "";
        $scope.reports = [
         { val: moment($scope.date).day(), name: "Day" },
         { val: moment($scope.date).month(), name: "Month" },
         { val: moment($scope.date).year(), name: "Year" }
        ];
        $scope.selectedreport = $scope.reports[0];

        //function getNow() {
        //    var aaa = moment().startOf('hour').fromNow();
        //    return toDateString(new Date()); 
        //}

		//function toDateString(date) {
        //    return $filter('date')(date, 'yyyy-MM-dd HH:mm:ss');
        //}

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