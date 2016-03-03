var stockService = angular.module('StockService', []);

stockService.service('StockService', [function () {
    //'use strict';

    this.defaultStocks = ["GOOG", "ABCD", "TODO", "1234", "LAST"];


    this.getStockTicker = function () {
        var res = [];
        for (var i in this.defaultStocks) {
            var stockSymbol = this.defaultStocks[i];
            // todo call stock api here
            var stock = {
                symbol: stockSymbol,
                otherField: "123",
            }

            res.push(stock);
        }

        return res;
    };

}]);