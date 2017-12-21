/// <reference path="angular.min.js" />

var myApp = angular.module("myModule", []);


myApp.controller("myController", function ($scope, $http) {

    //  Calling the web method GetCurrencies to get the list of currencies and send it to $scope
    $http({
        method: "Get",
        url: "GlobalAerospace.asmx/GetCurrencies"
      
    })
        .then(function (response) {
            $scope.currencies = response.data;
        })


    // Function for Calculate button.  Exchange rate is returned
    $scope.calculate = function() {

        try {
            $http({
                method: "Get",
                url: "GlobalAerospace.asmx/CalculateExchangeRate",
                params: { currentCurrency: $scope.selectedCurrentCurrency, newCurrency: $scope.selectedNewCurrency }
            }).then(function(response) {
                $scope.result = response.data;

            })
        }
        catch (err){
            alert(err);
        }

        
    }
});
