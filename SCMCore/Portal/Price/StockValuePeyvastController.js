myApp.controller('StockValuePeyvastController', ['$scope', 'ngProgress', 'SupplierService', 'PeyvastPriceFileService', function ($scope, ngProgress, SupplierService, PeyvastPriceFileService) {

    $scope.GetSppliers = function () {
        SupplierService.FillSupplier().then(function (result) {
            $scope.Suppliers = result.data;

        }).catch(function () {
            AutoClosingErrorAlert('Error in catch data !', 5000);


        }).finally(function () {

        });
    }
    $scope.GetSppliers();
    $scope.GetStockValuePeyvast = function (_IDSupplier) {
        ngProgress.start();
        $scope.Param = {
            IDSupplier: _IDSupplier
        }
        PeyvastPriceFileService.GetStockValuePeyvast($scope.Param).then(function (result) {
            $scope.StockValuePeyvasts = result.data;
            $('#TableStockValuePeyvast').show();
        }).catch(function () {
            AutoClosingErrorAlert('Error in catch data !', 5000);

        }).finally(function () {
            ngProgress.complete();
            ngProgress.stop();
        });
    }
    $scope.ShowLastPeyvastStockValue = function (_item)
    {
        if ($scope.activeRow == _item.IDPeyvastPriceFile) {
            $scope.activeRow = false;
        }
        else {
            $scope.activeRow = _item.IDPeyvastPriceFile;

        }
    }
    $scope.ChangeRatioRialInStockValuePeyvast = function (_item,_RatioRial)
    {
        _item.PeyvastPriceDetails.forEach(function (field) {
            field.FixedPrice2 = (_RatioRial / field.CurrencyValue) * field.Price ;
        });

    }
}]);