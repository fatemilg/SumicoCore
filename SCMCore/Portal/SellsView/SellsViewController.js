myApp.controller('SellsViewController', ['$scope', 'ngProgress', 'ProductCategoryService', 'DefineDetailProductService', 'SupplierService', 'PurchaseOrderFileService','PurchaseOrderService', function ($scope, ngProgress, ProductCategoryService, DefineDetailProductService, SupplierService, PurchaseOrderFileService, PurchaseOrderService) {
    $('#chkStockSituation').bootstrapToggle('on');
    $('#chkColumnShortDescription').bootstrapToggle();
    $('#chkColumnDescription').bootstrapToggle();
    $('.panel-body').on({
        click: function () {
            $('#ulMasterSetting').slideUp();
        }
    });
    $scope.OpenMasterSetting = function () {
        $('#ulMasterSetting').slideToggle();
    }
    $scope.ChnageSituation = function (_IDParent, el) {

        if ($('#Collapse' + _IDParent).hasClass('in')) {
            $('#Collapse' + _IDParent).collapse('hide');
        }
        else {
            $('#Collapse' + _IDParent).collapse('show');
        }

        $(el.target).parent().children().closest("#icon").toggleClass('fa-plus fa-minus');
    }

    $scope.ProductCategoryFromParentToMaster = function (_IDSupplier) {
        ngProgress.start();
        document.getElementById("txtSearch").value = "";
        $scope.search = "";
        var Param = {
            IDSupplier: _IDSupplier
        };
        ProductCategoryService.FetchProductCategoryDataFromParentToMaster(Param).then(function (result) {
            $scope.Categories = result.data[0];
            $scope.FillPurchaseOrderFile(_IDSupplier);
        }).catch(function () {

        }).finally(function () {
            ngProgress.complete();
        });
    }
    $scope.FillSupplier = function () {
        SupplierService.FillSupplier().then(function (result) {
            $scope.Suppliers = result.data;
        }).catch(function () {

        }).finally(function () {

        });
    }


    $scope.FillSupplier();

    $scope.initialStock = function () {
        $scope.Stock = true;
    }
    $scope.StockSituation = function (stock) {
        $scope.Stock = !stock;
    }


    $scope.initialColumn = function () {
        $scope.ColumnShortDescription = true;
        $scope.ColumnShortDescription = false;
    }
    $scope.ShowShortDescription = function (ColumnShortDescription) {
        $scope.ColumnShortDescription = !ColumnShortDescription;
    }
    $scope.ShowDescription = function (ColumnDescription) {
        $scope.ColumnDescription = !ColumnDescription;
    }
    $scope.FillPurchaseOrderFile = function (_IDSupplier) {
        $scope.Param = {
            IDSupplier: _IDSupplier,
            Show: true
        }
        PurchaseOrderFileService.GetPurchaseOrderFileInViews($scope.Param).then(function (result) {
            $scope.PurchaseOrderFiles = result.data;
        }).catch(function () {
            AutoClosingErrorAlert('Error in catch data !', 5000);


        }).finally(function () {

        });
    }

    $scope.Qty = function (_IDPurchaseOrderFile, _IDDefineDetailProduct) {
        var list = ($scope.PurchaseOrder).filter(function (n) { return n.IDPurchaseOrderFile == _IDPurchaseOrderFile && n.IDDefineDetailProduct == _IDDefineDetailProduct; });
        if (list[0] != undefined) {
            return list[0].Qty;
        }
        else {
            return '';
        }

    }
    $scope.FillPurchaseOrder = function () {
        PurchaseOrderService.GetPurchaseOrderInProgress().then(function (result) {
            $scope.PurchaseOrder = result.data;

        }).catch(function () {
            AutoClosingErrorAlert('Error in catch data !', 5000);


        }).finally(function () {

        });
    }
    $scope.FillPurchaseOrder();




}]);


