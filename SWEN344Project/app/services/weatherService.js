var weatherService = angular.module('WeatherService', []);

weatherService.service('WeatherService', [function () {
    //'use strict';

    this.defaultZip = "14623";
    this.userCurrentZip = null;

    this.getCurrentZipCode = function () {
        if (this.userCurrentZip != null) {
            return this.userCurrentZip;
        } else {
            var temp = Cookies.get("userWeatherZip");
            if (temp != null && temp != "" && !isNaN(parseInt(temp, 10))) {
                this.userCurrentZip = temp.toString();
                return this.userCurrentZip;
            }
        }


        return this.defaultZip;
    };

    this.setUserDefaultZipCode = function (zip) {
        Cookies.set("userWeatherZip", zip.toString(), { expires: 200 });
        this.userCurrentZip = zip;
    }

    // http://openweathermap.org/current
    this.apiID = "fb9769e8739b7889d90188cbb3052c24";
    this.baseApiURL = "http://api.openweathermap.org/data/2.5/";

    this.getWeather = function (zip, callback) {
        if (zip == null) {
            zip == this.getCurrentZipCode();
        }

        var dayData = null;
        var forecastData = null;

        // try and read daily data from the cookie as a cache
        var tmp = Cookies.get("dailyData_" + zip);
        if (tmp != null && tmp != "") {
            dayData = JSON.parse(tmp);
        } else {
            $.ajax({
                url: this.baseApiURL + "weather?zip=" + zip + ",us&appid=" + this.apiID,
                cache: false,
                crossDomain: true,
                dataType: 'json',
                method: 'GET',
                success: function (result) {
                    dayData = result;

                    // Save the data in a cookie for 10 minutes to cache it
                    // expires in 10 minutes (1 day / 24 hours per day / 60 minutes per hour * 10 = 10 minutes
                    Cookies.set("dailyData_" + zip, dayData, { expires: (((1 / 24) / 60) * 10) });

                    if (forecastData != null) {
                        weatherCallbackHelper(zip, dayData, forecastData, callback);
                    }
                },
                error: function () {
                    dayData = { errorLoading: true };

                    if (forecastData != null) {
                        weatherCallbackHelper(zip, dayData, forecastData, callback);
                    }
                }
            });
        }

        // try and read hourly data from the cookie as a cache
            tmp = null;
        tmp = Cookies.get("hourlyData_" + zip);
        if (tmp != null && tmp != "") {
            forecastData = JSON.parse(tmp);
        } else {

            $.ajax({
                url: this.baseApiURL + "forecast?zip=" + zip + ",us&appid=" + this.apiID + "&cnt=5",
                cache: false,
                crossDomain: true,
                dataType: 'json',
                method: 'GET',
                success: function (result) {
                    forecastData = result;

                    // Save the data in a cookie for 10 minutes to cache it
                    // expires in 10 minutes (1 day / 24 hours per day / 60 minutes per hour * 10 = 10 minutes
                    Cookies.set("hourlyData_" + zip, forecastData, { expires: (((1 / 24) / 60) * 10) });

                    if (dayData != null) {
                        weatherCallbackHelper(zip, dayData, forecastData, callback);
                    }
                },
                error: function () {
                    forecastData = { errorLoading: true };

                    if (dayData != null) {
                        weatherCallbackHelper(zip, dayData, forecastData, callback);
                    }
                }
            });
        }

        // We found all the data in cookies as the "cache"
        if (dayData != null && forecastData != null) {
            // If we don't do setTimeout, the call becomes synchronous
            // which messes up angular. So, setTimeout to make it asynchronous
            setTimeout(function () {
                weatherCallbackHelper(zip, dayData, forecastData, callback);
            }, 1);
        }
    };
}]);

kelvinToFarenheit = function(temp) {
    return parseFloat((((temp * 9) / 5) - 459.67).toFixed(2));
};

weatherCallbackHelper = function (zip, daily, forecast, callback) {
    var weather = parseWeatherData(daily);
    weather.zip = zip;
    weather.hourly = [];

    if (forecast.errorLoading) {
        weather.errorLoadingHourly = true;
    } else {
        for (var i in forecast.list) {
            var hourlyData = forecast.list[i];
            var formatted = parseWeatherData(hourlyData);
            formatted.time = new Date(hourlyData.dt_txt);
            weather.hourly.push(formatted);
        }
    }

    if (callback != null) {
        callback(weather)
    }
};

parseWeatherData = function (data) {
    var weather = {};
    if (data.errorLoading) {
        weather.errorLoading = true;
    } else {
        weather.farenheitTemperature = kelvinToFarenheit(data.main.temp);
        weather.maxFarenheitTemperature = kelvinToFarenheit(data.main.temp_max);
        weather.minFarenheitTemperature = kelvinToFarenheit(data.main.temp_min);
        weather.humidity = data.main.humidity;
        // TODO: add more detail

        if (data.weather != null && data.weather[0] != null && data.weather[0].description != null) {
            weather.description = data.weather[0].description;
        }
    }

    return weather;
};