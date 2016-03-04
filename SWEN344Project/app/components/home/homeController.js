var HomeController = angular.module("HomeController", []);

HomeController.controller('HomeController',
    ['$scope', 'WeatherService', 'StockService', 'AuthService',
        function ($scope, weatherService, stockService, authService) {
    //'use strict';

            $scope.temp = weatherService.getWeather().farenheitTemperature;
            $scope.stocks = stockService.getStockTicker();

            $scope.loggedIn = false;

            authService.doOnLogin('homeControllerLogin', function (user) {
                $scope.loggedIn = true;
                authService.getUserFeed(function (userWall) {
                    $scope.wall = userWall.data;
                });
            });

            authService.doOnLogout('homeControllerLogout', function () {
                
                $scope.loggedIn = false;
                $scope.wall = [];
            });
}]);