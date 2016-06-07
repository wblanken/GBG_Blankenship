(function(app) {

    app.controller('sampleCtrl', sampleCtrl);
    sampleCtrl.$inject = ['$scope', 'sampleService', 'statusService'];

    function sampleCtrl($scope, sampleService, statusService) {
        getSamples();
        function getSamples() {
            sampleService.getSamples()
                .success(function(samps) {
                    $scope.samples = samps;
                    console.log($scope.samples);
                })
                .error(function(error) {
                    $scope.samples = 'Unable to load sample data: ' + error.message;
                    console.log($scope.status);
                });
        }

        getStatuses();
        function getStatuses() {
            statusService.getStatuses()
                .success(function(statuses) {
                    $scope.statuses = statuses;
                    console.log($scope.statuses);
                })
                .error(function(error) {
                    $scope.statuses = 'Unable to load status data: ' + error.message;
                    console.log($scope.status);
                });
        }

        $scope.selectedStatus = null;
        $scope.userNameSearch = '';

        $scope.filterSamples =
            function filterSamples() {
                sampleService.getSamples($scope.userNameSearch, $scope.selectedStatus)
                    .success(function(samples) {
                        $scope.samples = samples;
                        console.log($scope.samples);
                    })
                    .error(function(error) {
                        $scope.statuses = 'Unable to load sample data: ' + error.message;
                        console.log($scope.status);
                    });
            }

        $scope.$on('sampleAdded',
            function() {
                getSamples();
            });
    };
})(angular.module('GBG_WebApp'));