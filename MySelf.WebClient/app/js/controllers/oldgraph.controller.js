myselflogApp.controller('GraphController',
    ['$scope',
        function ($scope) {
            initGraph();

            function initGraph() {
                if ($("#graph").length > 0) {
                    $scope.graph = Morris.Line({
                        element: 'graph',
                        xkey: 'logdate',
                        ykeys: ['value'],
                        labels: ['Diary']
                    });
                }
            }

            $scope.$watch('selectedprofile.logs', function () {
                if ($scope.selectedprofile && $scope.graph.available != false) {
                    $scope.graph.setData($scope.selectedprofile.logs);
                }
                
            }); // initialize the watch
            
            //$scope.$watch('valueAdded', function () {
            //    $scope.graph.setData($scope.logs);
            //}); // initialize the watch
        }
    ]
);