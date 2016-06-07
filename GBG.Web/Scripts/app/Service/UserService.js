(function(app) {
    app.factory("userService", ["$http", function ($http) {
        var userService = {};
        userService.getUsers = function () {
            return $http.get('/api/users');
        }
        return userService;
    }]);
})(angular.module('GBG_WebApp'));