var HomeController = angular.module("HomeController", []);

HomeController.controller('HomeController',
    ['$scope', 'WeatherService', 'StockService', 'AuthService',
        function ($scope, weatherService, stockService, authService) {
    //'use strict';

            $scope.temp = weatherService.getWeather().farenheitTemperature;
            $scope.stocks = stockService.getStockTicker();

            authService.doOnLogin(function (user) {
                authService.getUserFeed(function (userWall) {
                    $scope.wall = userWall.data;
                });
            });
}]);