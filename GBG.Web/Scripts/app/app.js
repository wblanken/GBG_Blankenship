(function() {
    var app = angular.module("GBG_WebApp", []);

    app.controller("sampleController", function ($scope, SampleService, StatusService) {
        getSamples();
        function getSamples() {
            SampleService.getSamples()
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
            StatusService.getStatuses()
                .success(function(statuses) {
                    $scope.statuses = statuses;
                    console.log($scope.statuses);
                })
                .error(function(error) {
                    $scope.statuses = 'Unable to load sample data: ' + error.message;
                    console.log($scope.status);
                });
        }

        $scope.selectedStatus = null;
        $scope.userNameSearch = '';

        $scope.filterSamples =
            function filterSamples() {
                SampleService.getFilteredSamples($scope.userNameSearch, $scope.selectedStatus)
                    .success(function(samples) {
                        $scope.samples = samples;
                        console.log($scope.samples);
                    })
                    .error(function(error) {
                        $scope.statuses = 'Unable to load sample data: ' + error.message;
                        console.log($scope.status);
                    });
            }
    });

    app.factory("SampleService", ["$http", function ($http) {
        var SampleService = {};
        SampleService.getSamples = function () {
            return $http.get('/api/samples');
        }
        SampleService.getFilteredSamples = function (userName, statusId) {
            if (userName && statusId) {
                return $http.get('/api/samples/user/' + userName + '/status/' + statusId.Id);
            } else if (userName) {
                return $http.get('/api/samples/user/' + userName);
            } else if (statusId) {
                return $http.get('/api/samples/status/' + statusId.Id);
            } else {
                return this.getSamples();
            }
        }
        return SampleService;
    }]);

    app.factory("StatusService", ["$http", function ($http) {
        var StatusService = {};
        StatusService.getStatuses = function () {
            return $http.get('/api/status');
        }
        return StatusService;
    }]);

    app.factory("UserService", ["$http", function ($http) {
        var StatusService = {};
        StatusService.getStatuses = function () {
            return $http.get('/api/users');
        }
        return StatusService;
    }]);

})();