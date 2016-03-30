var calendarService = angular.module('CalendarService', []);

calendarService.service('CalendarService', ['CaveWallAPIService', 'AuthService', function (CaveWallAPIService, authService) {
    //'use strict';

    this.getVisibleDays = function () {
        return { data: "I don't know how we want to do this... probably a Calendar angular js library of some sort..." };
    };

    

    CaveWallAPIService.makeCall("GET", "event", "all", 
        function (data) {
            // On success
            console.log("events: ")
            console.log(data);


        },
        function () {
            // On error
        });
}]);