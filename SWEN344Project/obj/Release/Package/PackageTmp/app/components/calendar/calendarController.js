var CalendarController = angular.module("CalendarController", []);

CalendarController.controller('CalendarController',
    ['$scope', 'CalendarService', 'AuthService',
        function ($scope, calendarService, authService) {
    //'use strict';

            //$scope.calendarData = calendarService.getVisibleDays();
    // see http://fullcalendar.io/docs/usage/
    $("#calendar").fullCalendar({
    });
}]);