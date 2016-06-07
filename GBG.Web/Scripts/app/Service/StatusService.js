(function (app) {
    app.factory("statusService", ["$http", function ($http) {
        var statusService = {};
        statusService.getStatuses = function () {
            return $http.get('/api/status');
        }
        return statusService;
    }]);
})(angular.module('GBG_WebApp'));