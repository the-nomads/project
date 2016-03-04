var authService = angular.module('AuthService', []);

authService.service('AuthService', ['$facebook', function ($facebook) {
    //'use strict';

    this.userLoggedIn = null;
    this.setUserLoggedIn = function(user) {
        this.userLoggedIn = user;
        for (var i in this.onLoginQueue) {
            var call = this.onLoginQueue[i];
            call(user);
        }
    }

    this.setUserLoggedOut = function() {
        this.userLoggedIn = null;
        for (var i in this.onLogoutQueue) {
            var call = this.onLogoutQueue[i];
            call(user);
        }
    }

    this.getUser = function() {
        return this.userLoggedIn;
    }

    this.getUserFeed = function (callback) {
        $facebook.api("/" + this.userLoggedIn.id + "/feed").then(function (userWall) {
            callback(userWall)
        });
    }

    this.onLoginQueue = [];
    this.doOnLogin = function(callback) {
        if (this.userLoggedIn != null) {
            callback(this.userLoggedIn);
        } else {
            this.onLoginQueue.push(callback);
        }
    }


    this.onLogoutQueue = [];
    this.doOnLogout = function (callback) {
        if (this.userLoggedIn == null) {
            callback(this.userLoggedIn);
        } else {
            this.onLogoutQueue.push(callback);
        }
    }
}]);