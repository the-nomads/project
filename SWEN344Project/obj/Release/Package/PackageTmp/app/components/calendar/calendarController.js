var CalendarController = angular.module("CalendarController", []);

CalendarController.controller('CalendarController',
    ['$scope', 'CalendarService', 'AuthService',
        function ($scope, calendarService, authService) {
            //'use strict';

            //$scope.calendarData = calendarService.getVisibleDays();
            // see http://fullcalendar.io/docs/usage/

            $("#calendar").fullCalendar({
                header: {
                    left: 'today prev,next',
                    center: '',
                    right: 'title'
                },
                //
                // THIS KEY WON'T WORK IN PRODUCTION!!!
                // To make your own Google API key, follow the directions here:
                // http://fullcalendar.io/docs/google_calendar/
                googleCalendarApiKey: 'AIzaSyC7jEyslwOpBHRCs2XoDcAE8jRKu3eyCM0',

                // US Holidays
                events: 'en.usa#holiday@group.v.calendar.google.com',

                eventClick: function (event) {
                    // opens events in a popup window
                    window.open(event.url, 'gcalevent', 'width=700,height=600');
                    return false;
                },

                loading: function (bool) {
                    $('#loading').toggle(bool);
                }
            });


        }]);