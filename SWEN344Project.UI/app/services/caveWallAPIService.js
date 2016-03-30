var caveWallAPIService = angular.module('CaveWallAPIService', []);

var staticInt = 0;
caveWallAPIService.service('CaveWallAPIService', ['$facebook', 'AuthService', function ($facebook, authService) {
    //'use strict';

    var apiUrl = CaveWallAPIURL;
    if (!apiUrl.endsWith("/")) {
        apiUrl += "/";
    }

    this.makeCall = function (method, resource, id, callback, onError) {
        var callHandle = "APISvcCall_" + String(staticInt);
        staticInt++;
        authService.doOnLogin(callHandle, function () {
            var callUrl = apiUrl + resource;
            if (id != null) {
                callUrl += "/" + id;
            }

            var extraHeaders = {};
            var fbResponse = $facebook.getAuthResponse();
            if (fbResponse) {
                extraHeaders.accessToken = fbResponse.accessToken;
            }

            $.ajax({
                url: callUrl,
                cache: false,
                crossDomain: true,
                dataType: 'json',
                method: method,
                headers: extraHeaders,
                success: function (result) {
                    if (callback) {
                        callback(result);
                    }
                },
                error: function () {
                    if (onError) {
                        onError();
                    }
                }
            });
        });
    };
}]);