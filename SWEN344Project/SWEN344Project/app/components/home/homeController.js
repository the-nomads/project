var HomeController = angular.module("HomeController", []);

HomeController.controller('HomeController', ['$scope', function ($scope) {
    //'use strict';
	$scope.number = 18;
	$scope.getNumber = function(num) {
		return new Array(num);   
	}
}]);