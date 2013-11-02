myselflogApp.controller('TestController',
    ['$scope', '$log',
function ($scope, $log) {
    $scope.alerts = [];
    $scope.loading = false;

    $scope.$watch('loading', function () {
        if ($scope.loading == false) {

        } else {

        }
    });

    function sleep(milliseconds) {
        var start = new Date().getTime();
        for (var i = 0; i < 1e7; i++) {
            if ((new Date().getTime() - start) > milliseconds) {
                break;
            }
        }
    }

    $scope.processData = function () {
        $log.log("start processing");
        $scope.loading = true;
        $scope.addAlert("Processing data...");
        load();
        $log.log("end processing");
        $scope.loading = false;
        $scope.closeAlert(0);
    };

    function load() {
        sleep(10000);
    }

    $scope.addAlert = function (message) {
        $scope.alerts.push({ msg: message });
    };

    $scope.closeAlert = function (index) {
        $scope.alerts.splice(index, 1);
    };
}]);