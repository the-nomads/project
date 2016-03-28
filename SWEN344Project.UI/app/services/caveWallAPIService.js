var caveWallAPIService = angular.module('CaveWallAPIService', []);

caveWallAPIService.service('CaveWallAPIService', ['$facebook', function ($facebook) {
    //'use strict';

    var apiUrl = CaveWallAPIURL;
    if (!apiUrl.endsWith("/")) {
        apiUrl += "/";
    }

    this.makeCall = function (method, resource, id, callback, onError) {
        var callUrl = this.apiUrl + resource;
        if (id != null) {
            callUrl += "/" + id;
        }

        var extraHeaders = {};
        var fbResponse = $facebook.getAuthResponse();
        if (fbResponse) {

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
    };
}]);