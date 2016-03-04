var StockSearchController = angular.module("StockSearchController", []);

StockSearchController.controller('StockSearchController',
    ['$scope', 'StockService', 'AuthService', '$location',
        function ($scope, stockService, authService, $location) {
            //'use strict';
            $scope.currentStockSymbol = $location.search().stock;


            $scope.stockSearch = function () {
                if ($scope.currentStockSymbol == null || $scope.currentStockSymbol == "") {
                    // TODO: friendly warning
                    alert("Please enter a Stock Symbol to look up");
                } else {
                    alert("You searched for " + $scope.currentStockSymbol);
                    // TODO look up data using StockService
                }

            }


            // this means that the there was a stock in the location's querystring
            // so automatically do the search
            if ($scope.currentStockSymbol != null) {
                $scope.stockSearch();
            }
        }]);