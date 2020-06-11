myApp.controller('POHistoryController', ['$scope', 'ngProgress', 'RelatePurchaseOrderFileService', 'SupplierService','PurchaseOrderFileService',function ($scope, ngProgress, RelatePurchaseOrderFileService, SupplierService, PurchaseOrderFileService) {
    $scope.SelectOne = false;
    $scope.OpenPurchaseList = function () {
        $('#ulPurchaseList').slideToggle();
    }
    $scope.ClosePurchaseList = function () {
        $('#ulPurchaseList').slideUp();
    }
    $scope.FillSupplier = function () {
        SupplierService.FillSupplier().then(function (result) {
            $scope.Suppliers = result.data;
        }).catch(function () {

        }).finally(function () {

        });
    }
    $scope.FillSupplier();



    $scope.FillPurchaseOrderFile = function (_IDSupplier) {
        ngProgress.start();
        $scope.IDSupplier = _IDSupplier;
        $scope.Param = {
            IDSupplier: _IDSupplier,
            Show: "none"
        }
        PurchaseOrderFileService.GetPurchaseOrderFileInViews($scope.Param).then(function (result) {
            $scope.PurchaseOrderFiles = result.data;
            $('#divPoHistory').show();
            $('#divData').css({ 'width': (result.data.length) * 90 + 'px' });
            $scope.FillRelatePurchaseOrderFile($scope.IDSupplier);
        }).catch(function () {
            AutoClosingErrorAlert('Error in catch data !', 5000);

        }).finally(function () {
            ngProgress.complete();
            ngProgress.stop();
        });
    }
    $scope.LoadPOHistory = function (_Check, _IDPurchaseOrderFile) {
        if (_Check) {
            $('.pof' + _IDPurchaseOrderFile).fadeIn();

        }
        else {
            $('.pof' + _IDPurchaseOrderFile).hide();
        }


    }
    $scope.SelectAllItems = function (SelectAll) {
        $scope.SelectOne = $scope.SelectAll;
        $('.SelectOne').prop('checked', SelectAll);
        if (SelectAll) {
            $('.showall').fadeIn();

        }
        else {
            $('.showall').hide();
        }
    }
    $scope.FillRelatePurchaseOrderFile = function (_IDSupplier) {
        $scope.Param = {
            IDSupplier: _IDSupplier
        }
        RelatePurchaseOrderFileService.GetDataForPOHistory($scope.Param).then(function (result) {
            $scope.RelatePurchaseOrderFiles = result.data;
        }).catch(function () {
            AutoClosingErrorAlert('Error in catch data !', 5000);

        }).finally(function () {

        });
    }



    $scope.GetHSCode = function (_IDPurchaseOrderFile, _item) {
        
            var list = (_item).filter(function (n) { return n.IDPurchaseOrderFile == _IDPurchaseOrderFile });
            if (list[0] != undefined) {
                return list[0].RelatePurchaseOrderFile[0].HsCode;
            }
            else {
                return '-';
            }
        
    }
    $scope.GetIDCode = function (_IDPurchaseOrderFile, _item) {
        //for (var obj in _item) {
        //    if (_IDPurchaseOrderFile == _item[obj].IDPurchaseOrderFile) {
        //        return _item[obj].RelatePurchaseOrderFile[0].IDCode;
        //    }
        //    else {
        //        return "-";
        //    }

        //}

        var list = (_item).filter(function (n) { return n.IDPurchaseOrderFile == _IDPurchaseOrderFile });
        if (list[0] != undefined) {
            return list[0].RelatePurchaseOrderFile[0].IDCode;
        }
        else {
            return '-';
        }
    }
    $scope.GetUnderValue = function (_IDPurchaseOrderFile, _item) {
        var list = (_item).filter(function (n) { return n.IDPurchaseOrderFile == _IDPurchaseOrderFile });
        if (list[0] != undefined) {
            return list[0].RelatePurchaseOrderFile[0].UnderValue;
        }
        else {
            return '-';
        }
    }

    $scope.ClearSearch = function () {
        $scope.searchPurchase = "";
    }
    angular.element(document.querySelector('#divContainerData')).bind('scroll', function () {

        $('#divContainerParam').scrollTop($('#divContainerData').scrollTop());
    });

}]);


