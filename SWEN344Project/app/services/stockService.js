var stockService = angular.module('StockService', []);

stockService.service('StockService', [function () {
    //'use strict';

    this.defaultStocks = ["GOOG", "YHOO", "AAPL", "AMZN", "MSFT"];

    this.baseUrl = "http://query.yahooapis.com/v1/public/yql";
    this.urlAppendDetail = "&format=json&env=http://datatables.org/alltables.env";
    this.urlAppendTimeline = "&format=json&env=store://datatables.org/alltableswithkeys&callback=";

    this.getStockTicker = function (callback) {
        this.getStockDetailsMany(this.defaultStocks, callback)
    };

    this.getStockDetails = function (stockSymbol, callback) {
        var arr = [];
        arr.push(stockSymbol);
        this.getStockDetailsMany(arr, callback);
    };

    this.getStockDetailsMany = function (symbols, callback) {
        var query = "select * from yahoo.finance.quotes where symbol in ("

        for (var i in symbols) {
            var stockSymbol = symbols[i];
            query += '"' + stockSymbol + '"';
            if (i < symbols.length - 1)
                query += ",";
        }

        query += ")";
        this.executeQuery(query, true, function (data) {
            callback(data.query.results.quote);
        });
    }

    this.getStockTimeline = function (symbol, startDate, endDate, callback) {

        if (endDate > startDate) {
            var swap = endDate;
            endDate = startDate;
            startDate = swap;
        }

        //startDate%20%3D%20%222009-09-11%22%20and%20endDate%20%3D%20%222010-03-10%22
        var query = 'select * from yahoo.finance.historicaldata where symbol = "' + symbol 
            + '" and startDate = "' + $.format.date(endDate, 'yyyy-MM-dd') + 
            '" and endDate = "' + $.format.date(startDate, 'yyyy-MM-dd') + '"';

        this.executeQuery(query, false, function (data) {
            callback(data.query.results.quote);
        });
    }

    this.executeQuery = function (query, isDetail, callback) {
        $.ajax({
            url: this.baseUrl + "?q=" + encodeURIComponent(query) + (isDetail ? this.urlAppendDetail : this.urlAppendTimeline),
            cache: false,
            crossDomain: true,
            dataType: 'json',
            method: 'GET',
            success: function (result) {
                if (callback != null) {
                    callback(result);
                }
            }
        });
    }
}]);