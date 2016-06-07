(function(app) {
    app.directive('addSample', addSample);

    var controller = function ($scope, $http, userService, statusService) {
        var addCtrl = this;

        addCtrl.selectedUserId = null;
        addCtrl.selectedStatusId = null;
        addCtrl.newBarCode = null;
        addCtrl.users = {};
        addCtrl.statuses = {};

        getUsers();
        function getUsers() {
            userService.getUsers()
                .success(function(users) {
                    addCtrl.users = users;
                    addCtrl.selectedUserId = users[0].Id;
                    console.log(addCtrl.users);
                })
                .error(function(error) {
                    $scope.samples = 'Unable to load user data: ' + error.message;
                    console.log($scope.status);
                });
        };

        getStatuses();
        function getStatuses() {
            statusService.getStatuses()
                .success(function(statuses) {
                    addCtrl.statuses = statuses;
                    addCtrl.selectedStatusId = statuses[0].Id;
                    console.log(addCtrl.statuses);
                })
                .error(function(error) {
                    $scope.statuses = 'Unable to load status data: ' + error.message;
                    console.log($scope.status);
                });
        };

        addCtrl.submit = function() {
            var sampleData = {
                "BarCode": addCtrl.newBarCode, "CreatedBy": addCtrl.selectedUserId,
                "StatusId": addCtrl.selectedStatusId, "User": addCtrl.users[addCtrl.selectedUserId],
                "Status": addCtrl.statuses[addCtrl.selectedStatusId]
            }
            $http.post('/api/samples', sampleData)
                .success(function() {
                    console.log("Sampled added");
                    $scope.$emit('sampleAdded');
                })
            .error(function(message) {
                    console.log(message);
                });
        };
    }

    function addSample() {
        return {
            restrict: 'E',
            templateUrl: '/Scripts/app/Samples/addSample.html',
            controller: controller,
            controllerAs: 'addCtrl'
        };
    };
})(angular.module('GBG_WebApp'));