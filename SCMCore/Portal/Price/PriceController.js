myApp.controller('PriceController', ['$scope', function ($scope) {

    $scope.ClickDivSupplierPriceList = function ()
    {
        $('#TableSupplierPriceListFile').hide();
    }
    $scope.ClickDivQouation = function () {
        $('#TableQouationFile').hide();
    }
    $scope.ClickDivPeyvastPrice = function () {
        $('#TablePeyvastPriceFile').hide();
    }
    $scope.ClickDivPeyvastPrice = function () {
        $('#TableStockValuePeyvast').hide();
    }
    $scope.ClickDivPreparationPriceList = function () {
        $('#TableStockValuePeyvast').hide();
    }
}]);