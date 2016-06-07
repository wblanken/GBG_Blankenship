(function(app) {
    app.directive('viewSamples', viewSamples);

    //viewSamples.$inject('sampleCtrl');

    function viewSamples() {
        return {
            restrict: 'E',
            templateUrl: '/Scripts/app/Samples/viewSamples.html',
            controller: 'sampleCtrl'
        };
    };
})(angular.module('GBG_WebApp'));