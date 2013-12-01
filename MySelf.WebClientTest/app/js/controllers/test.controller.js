myselflogApp.controller('TestController',
    ['$scope', '$log', '$timeout',
function ($scope, $log, $timeout) {
    $scope.alerts = [];
    $scope.loading = false;
    $scope.showDialog = false;

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
        //$log.log("start processing");
        //$scope.loading = true;
        $("#ciccio").modal("show");
        load();
        $("#ciccio").modal("hide");
        //$scope.addAlert("Processing data...");
        //$timeout(function () {
        //    //$scope.showDialog = true;
        //     load();
        //}, 0).then(function () {  });
        
        //$log.log("end processing");
        //$scope.loading = false;
        //$scope.closeAlert(0);
        
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