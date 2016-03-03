var weatherService = angular.module('WeatherService', []);

weatherService.service('WeatherService', [function () {
    //'use strict';

    this.defaultZip = "14623";
    this.userCurrentZip = null;

    this.getCurrentZipCode = function () {
        if (this.userCurrentZip != null) {
            return this.userCurrentZip;
        }
        return this.defaultZip;
    };

    this.getWeather = function (zip) {
        if (zip == null) {
            zip == this.getCurrentZipCode();
        }

        // todo: call weather api here and populate weather

        var weather = {
            zip: zip,
            celsiusTemperature: 100,
            farenheitTemperature: 212,
            humidity: 0.5,
        };

        return weather;
    };
}]);