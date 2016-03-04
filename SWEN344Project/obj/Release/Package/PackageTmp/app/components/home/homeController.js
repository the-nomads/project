var HomeController = angular.module("HomeController", []);

HomeController.controller('HomeController',
    ['$scope', 'WeatherService', 'StockService', 'CalendarService', 'AuthService',
        function ($scope, weatherService, stockService, calendarService, authService) {
    //'use strict';

            $scope.temp = weatherService.getWeather().farenheitTemperature;
            $scope.stocks = stockService.getStockTicker();
            $scope.calendarData = calendarService.getVisibleDays();

            authService.doOnLogin(function (user) {
                authService.getUserFeed(function (userWall) {
                    $scope.wall = userWall.data;
                });
            });
}]);