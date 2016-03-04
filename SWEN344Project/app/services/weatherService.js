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

    this.apiID = "fb9769e8739b7889d90188cbb3052c24";
    this.baseApiURL = "http://api.openweathermap.org/data/2.5/weather";

    this.getWeather = function (zip, callback) {
        if (zip == null) {
            zip == this.getCurrentZipCode();
        }

        // http://openweathermap.org/current

        $.ajax({
            url: this.baseApiURL + "?zip=" + zip + ",us&appid=" + this.apiID,
            cache: false,
            crossDomain: true,
            dataType: 'json',
            method: 'GET',
            success: function (result) {
                if (callback != null) {

                    var weather = {
                        zip: zip,
                        farenheitTemperature: kelvinToFarenheit(result.main.temp),
                        maxFarenheitTemperature: kelvinToFarenheit(result.main.temp_max),
                        minFarenheitTemperature: kelvinToFarenheit(result.main.temp_min),
                        humidity: result.main.humidity,
                    };

                    callback(weather);
                }
            }
        })
    };
}]);

kelvinToFarenheit = function(temp) {
    return parseFloat((((temp * 9) / 5) - 459.67).toFixed(2));
};