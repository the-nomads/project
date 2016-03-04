﻿
var angApp = angular.module('angApp', [
    'ngRoute',
    'ngFacebook',
    'HomeController',
    'FacebookLoginController',
    'WeatherService',
    'StockService',
    'CalendarService',
]);


angApp.config(['$routeProvider', '$facebookProvider',
    function ($routeProvider, $facebookProvider) {

        $facebookProvider.setVersion("v2.5");
        $facebookProvider.setAppId("198266390540885");
        $facebookProvider.setPermissions("email,public_profile,user_friends,user_posts");

        $routeProvider.
            when('/', {
                redirectTo: '/home',
                title: 'Home'
			}).
            when('/home', {
                templateUrl: 'app/components/home/homeView.html',
				controller: 'HomeController',
                title: 'Home',
            }).
            when('/anotherPage', {
                templateUrl: 'app/components/anotherPage/anotherPageView.html',
                title: 'Another Page',
            });
    }
]);

angApp.run(['$rootScope', '$window', function ($rootScope, $window) {
    // If we've already installed the SDK, we're done
    if (document.getElementById('facebook-jssdk')) { return; }

    // Get the first script element, which we'll use to find the parent node
    var firstScriptElement = document.getElementsByTagName('script')[0];

    // Create a new script element and set its id
    var facebookJS = document.createElement('script');
    facebookJS.id = 'facebook-jssdk';

    // Set the new script's source to the source of the Facebook JS SDK
    facebookJS.src = '//connect.facebook.net/en_US/all.js';

    // Insert the Facebook JS SDK into the DOM
    firstScriptElement.parentNode.insertBefore(facebookJS, firstScriptElement);
}]);