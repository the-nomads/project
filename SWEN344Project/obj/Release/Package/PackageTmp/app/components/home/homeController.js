var HomeController = angular.module("HomeController", []);

HomeController.controller('HomeController',
    ['$scope', 'WeatherService', 'StockService', 'AuthService',
        function ($scope, weatherService, stockService, authService) {
    //'use strict';

            $scope.temp = weatherService.getWeather().farenheitTemperature;
            $scope.stocks = stockService.getStockTicker();

            $scope.loggedIn = false;

            authService.doOnLogin(function (user) {
                $scope.loggedIn = true;
                authService.getUserFeed(function (userWall) {
                    $scope.wall = userWall.data;
                });
            });

            authService.doOnLogout(function () {
                console.log("Home controller logged out")
                $scope.loggedIn = false;
                $scope.wall = [];
            });
}]);