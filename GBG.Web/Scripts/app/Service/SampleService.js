(function (app) {
    app.factory('sampleService', ['$http', function ($http) {
        var sampleService = {};
        sampleService.getSamples = function (userName, statusId) {
            if (userName && statusId) {
                return $http.get('/api/samples/user/' + userName + '/status/' + statusId.Id);
            } else if (userName) {
                return $http.get('/api/samples/user/' + userName);
            } else if (statusId) {
                return $http.get('/api/samples/status/' + statusId.Id);
            } else {
                return $http.get('/api/samples');
            }
        }
        return sampleService;
    }]);
})(angular.module('GBG_WebApp'));