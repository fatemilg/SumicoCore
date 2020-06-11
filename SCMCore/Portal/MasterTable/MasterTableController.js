myApp.controller('MasterTableController', ['$scope', 'ngProgress', 'ProductCategoryService', 'DefineDetailProductService', 'SupplierService', 'PurchaseOrderFileService','PurchaseOrderService',function ($scope, ngProgress, ProductCategoryService, DefineDetailProductService, SupplierService, PurchaseOrderFileService, PurchaseOrderService) {

    $('#chkStockSituation').bootstrapToggle('on');
    $('.panel-body').on({
        click: function () {
            $('#ulMasterSetting').slideUp();
        }
    });

    $scope.OpenMasterSetting = function () {
        $('#ulMasterSetting').slideToggle();
    }
    $scope.ChangeSituation = function (id, el) {
        $('.Collapse' + id).fadeToggle();

        $(el.target).parent().children().closest("#icon").toggleClass('fa-minus fa-plus');
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
            $('#ContainerCategory').show();
            $('#BottomHeader').show();

        }).catch(function () {

        }).finally(function () {
            ngProgress.complete();
            ngProgress.stop();
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


    $scope.FillPurchaseOrder = function () {
        PurchaseOrderService.GetPurchaseOrderInProgress().then(function (result) {
            $scope.PurchaseOrder = result.data;

        }).catch(function () {
            AutoClosingErrorAlert('Error in catch data !', 5000);


        }).finally(function () {

        });
    }
    $scope.FillPurchaseOrder();

    $scope.Qty = function (_IDPurchaseOrderFile, _IDDefineDetailProduct) {
        var list = ($scope.PurchaseOrder).filter(function (n) { return n.IDPurchaseOrderFile == _IDPurchaseOrderFile && n.IDDefineDetailProduct == _IDDefineDetailProduct; });
        if (list[0] != undefined) {
            return list[0].Qty;
        }
        else {
            return '';
        }

    }
    $scope.Price = function (_IDPurchaseOrderFile, _IDDefineDetailProduct) {
        var list = ($scope.PurchaseOrder).filter(function (n) { return n.IDPurchaseOrderFile == _IDPurchaseOrderFile && n.IDDefineDetailProduct == _IDDefineDetailProduct; });
        if (list[0] != undefined) {
            return list[0].Price;
        }
        else {
            return '';
        }
    }

    $scope.ClearSearch = function () {
        $scope.search = "";
    }
    $scope.ShowModalSortInCrm = function (_item) {
        $('#ModalSortInCrm').modal('show');

        $scope.ChildForSortTables = _item.Childs;
        $scope.HeaderForSortInCrm = _item.CategoryMasterName;


    }
    $scope.SortInCrm = {
        pull: true,
        put: true,
        sort: true,
        animation: 150,
        accept: function (sourceItemHandleScope, destSortableScope) {

        },
        onStart: function (evt) {

        },
        onEnd: function (evt) {
            if (evt.oldIndex != evt.newIndex) {
                var myJsonString = JSON.stringify(evt.models);
                DefineDetailProductService.ChangeSortInDefineDetailProduct(myJsonString).then(function (result) {
                    if (result.data == false) {
                        AutoClosingErrorAlert('Error In Sort!', 5000);
                        return;
                    }

                }).catch(function () {
                    AutoClosingErrorAlert('Error In Sort!', 5000);

                }).finally(function () {

                });
            }

        },
        onAdd: function (evt) {

        },
        onRemove: function (evt) {

        },

        onFilter: function (evt) {

        },
        onSort: function (evt) {

        }

    };



}]);


