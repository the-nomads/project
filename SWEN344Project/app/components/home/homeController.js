var HomeController = angular.module("HomeController", []);

HomeController.controller('HomeController',
    ['$scope', 'WeatherService', 'StockService', 'CalendarService',
        function ($scope, weatherService, stockService, calendarService) {
    //'use strict';

            $scope.temp = weatherService.getWeather().farenheitTemperature;
            $scope.stocks = stockService.getStockTicker();
            $scope.calendarData = calendarService.getVisibleDays();
}]);