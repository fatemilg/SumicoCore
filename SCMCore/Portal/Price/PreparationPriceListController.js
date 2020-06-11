myApp.controller('PreparationPriceListController', ['$scope', 'ngProgress', 'SupplierService', 'PreparationPriceListService', 'PreparationPriceListDetailService', 'QouationDetailService', function ($scope, ngProgress, SupplierService, PreparationPriceListService, PreparationPriceListDetailService, QouationDetailService) {
    $scope.CurrentPreparationPriceList = {};
    $scope.activeRowWithOutCategory = {};
    $scope.activeRowByCategory = {};

    $scope.InitialForLazyLoading = function () {
        $scope.PaginationNumber = 1;
        $scope.PaginationSize = 100;
        $scope.PreparationPriceDetailsWithOutCategory = [];
    }
    $scope.GetSppliers = function () {
        SupplierService.FillSupplier().then(function (result) {
            $scope.Suppliers = result.data;

        }).catch(function () {
            AutoClosingErrorAlert('Error in catch data !', 5000);


        }).finally(function () {

        });
    }
    $scope.GetSppliers();

    $scope.GetPreparationPriceList = function (_IDSupplier) {
        ngProgress.start();
        $scope.Param = {
            IDSupplier: _IDSupplier
        }
        PreparationPriceListService.GetPreparationPriceList($scope.Param).then(function (result) {
            $scope.PreparationPriceLists = result.data;
            $('#TablePreparationPriceList').show();
        }).catch(function () {
            AutoClosingErrorAlert('Error in catch data !', 5000);

        }).finally(function () {
            ngProgress.complete();
            ngProgress.stop();
        });
    }

    $scope.NewPreparationPriceList = function () {
        $scope.CurrentPreparationPriceList.Title = "";
        $scope.CurrentPreparationPriceList.Supplier = $scope.Suppliers[0].value;
        document.getElementById("drpSupplierInPreparationPriceList").disabled = false;
        document.getElementById("AddPreparationPriceList").value = "Add";
    }
    $scope.CancelPreparationPriceList = function (_item) {
        $scope.NewPreparationPriceList();
    }

    $scope.AddPreparationPriceList = function () {
        var _IDSupplier = $scope.CurrentPreparationPriceList.Supplier;
        if (document.getElementById("AddPreparationPriceList").value == "Add") {
            ngProgress.start();
            var _IDPreparationPriceList = NewGuid();
            var _Title = $scope.CurrentPreparationPriceList.Title;

            $scope.Param = {
                IDPreparationPriceList: _IDPreparationPriceList,
                IDLogUser: _IDLogUser,
                Title: _Title,
                IDSupplier: _IDSupplier
            };
            PreparationPriceListService.AddPreparationPriceList($scope.Param).then(function (result) {
                $scope.NewPreparationPriceList();
                ngProgress.stop();
                $scope.GetPreparationPriceList(_IDSupplier);
                AutoClosingSuccessAlert('Add was successful !', 5000);
            }).catch(function () {
                AutoClosingErrorAlert('Error in Add !', 5000);

            }).finally(function () {
                ngProgress.complete();
                ngProgress.stop();
            });

        }
        else if (document.getElementById("AddPreparationPriceList").value == "Update") {
            $scope.Param = {
                IDPreparationPriceList: $scope.CurrentPreparationPriceList.IDPreparationPriceList,
                IDLogUser: _IDLogUser,
                Title: $scope.CurrentPreparationPriceList.Title
            };
            PreparationPriceListService.UpdatePreparationPriceList($scope.Param).then(function (result) {
                $scope.NewPreparationPriceList();
                ngProgress.stop();
                $scope.GetPreparationPriceList(_IDSupplier);
                AutoClosingSuccessAlert('Update was successful !', 5000);
            }).catch(function () {
                AutoClosingErrorAlert('Error in Add !', 5000);

            }).finally(function () {
                ngProgress.complete();
                ngProgress.stop();
            });
        }


    }

    $scope.ShowPreparationPriceListDetailWithOutCategory = function (_item) {
        $scope.HeaderModalPreparationPriceListWithOutCategory = _item.Title;
        $scope.PercentageInModalWithOutCategory = "";
        $('#imgLoaderPreparationWithOutCategory' + _item.IDPreparationPriceList).show();
        $scope.Param = {
            IDPreparationPriceList: _item.IDPreparationPriceList
        }
        PreparationPriceListDetailService.GetPreparationPriceListDetailWithOutCategory($scope.Param).then(function (result) {
            $scope.PreparationPriceDetails = result.data;
            $scope.InitialForLazyLoading();
            $scope.GetPreparationDetailByPageNumber($scope.PaginationNumber, $scope.PreparationPriceDetails);
            $scope.IDPreparationPriceList = _item.IDPreparationPriceList;
        }).catch(function () {
            AutoClosingErrorAlert('Error in catch data !', 5000);

        }).finally(function () {
            $('#imgLoaderPreparationWithOutCategory' + _item.IDPreparationPriceList).hide();
            $('#ModalPreparationPriceListWithOutCategory').modal('show');
        });

    }
    $scope.ShowPreparationPriceListDetailByCategory = function (_item) {
        if ($scope.activeRow == _item.IDPreparationPriceList) {
            $scope.activeRow = false;
        }
        else {
            $('#imgLoaderPreparationByCategory' + _item.IDPreparationPriceList).show();
            $scope.Param = {
                IDPreparationPriceList: _item.IDPreparationPriceList
            }
            PreparationPriceListDetailService.GetPreparationPriceListDetailByCategory($scope.Param).then(function (result) {
                $scope.PreparationPriceDetailsByCategory = result.data[0];
                $scope.IDPreparationPriceList = _item.IDPreparationPriceList;
            }).catch(function () {
                AutoClosingErrorAlert('Error in catch data !', 5000);

            }).finally(function () {
                $('#imgLoaderPreparationByCategory' + _item.IDPreparationPriceList).hide();
                $scope.activeRow = _item.IDPreparationPriceList;
            });
        }
    }


    $scope.GetPreparationDetailByPageNumber = function (_PaginationNumber, _Source) {
        for (i = (_PaginationNumber - 1) * $scope.PaginationSize; i < $scope.PaginationSize * _PaginationNumber; i++) {
            $scope.PreparationPriceDetailsWithOutCategory.push(_Source[i])
        }
        if (_PaginationNumber > 1) {
            $scope.$apply();
        }

    }
    $scope.EditPreparationPriceList = function (_item) {
        $scope.CurrentPreparationPriceList.IDPreparationPriceList = _item.IDPreparationPriceList;
        $scope.CurrentPreparationPriceList.Supplier = _item.IDSupplier;
        $scope.CurrentPreparationPriceList.Title = _item.Title;
        document.getElementById("AddPreparationPriceList").value = "Update";
        document.getElementById("drpSupplierInPreparationPriceList").disabled = true;
        $('html, body').animate({
            scrollTop: $("#panelBodyPreparationPriceList").offset().top - 100
        }, 1000);


    }
    $scope.DeletePreparationPriceList = function (_IDPreparationPriceList, _IDSupplier) {
        var Accept = confirm("Are you sure ?");
        if (Accept == true) {
            $scope.Param = {
                IDPreparationPriceList: _IDPreparationPriceList
            };
            PreparationPriceListService.DeletePreparationPriceList($scope.Param).then(function (result) {
                $scope.NewPreparationPriceList();
                $scope.GetPreparationPriceList(_IDSupplier);
                AutoClosingSuccessAlert('Delete was successful !', 5000);
            }).catch(function () {
                AutoClosingErrorAlert('Error in Delete !', 5000);

            }).finally(function () {
                ngProgress.complete();
            });

        }


    }


    $scope.ScrollDiv = function (elem) {
        if ((elem.offsetHeight + Math.round(elem.scrollTop)) == (elem.scrollHeight) ||
            (elem.offsetHeight + Math.round(elem.scrollTop)) == (elem.scrollHeight - 1)) {
            $scope.PaginationNumber = $scope.PaginationNumber + 1;
            $scope.GetPreparationDetailByPageNumber($scope.PaginationNumber, $scope.PreparationPriceDetails);
        }

    }
    $scope.SearchPartNumberInPreparationDetail = function (_PartNumber) {
        if (_PartNumber.length >= 4) {
            var list = ($scope.PreparationPriceDetails).filter(function (n) { return n.PartNumber.includes(_PartNumber) });
            $scope.PreparationPriceDetailsWithOutCategory = list;

        }
        else if (_PartNumber.length == 0) {
            $scope.InitialForLazyLoading();
            $scope.GetPreparationDetailByPageNumber($scope.PaginationNumber, $scope.PreparationPriceDetails);
        }

    }
    $scope.SearchShortDescriptionInPreparationDetail = function (_SearchShortDescription) {
        if (_SearchShortDescription.length > 4) {
            var list = ($scope.PreparationPriceDetails).filter(function (n) { return n.ShortDescription.includes(_SearchShortDescription) });
            $scope.PreparationPriceDetailsWithOutCategory = list;

        }
        else if (_SearchShortDescription.length == 0) {
            $scope.InitialForLazyLoading();
            $scope.GetPreparationDetailByPageNumber($scope.PaginationNumber, $scope.PreparationPriceDetails);
        }

    }

    $scope.UpdateShowPreparation = function (_ShowSituation, _IDPreparationPriceList) {
        for (i = 0; i < $scope.PreparationPriceLists.length; i++) {
            if ($scope.PreparationPriceLists[i].IDPreparationPriceList != _IDPreparationPriceList) {
                $scope.PreparationPriceLists[i].Show = false;
            }
        }
        $scope.Param = {
            Show: _ShowSituation,
            IDPreparationPriceList: _IDPreparationPriceList
        }
        PreparationPriceListService.UpdatePreparationPriceListByShow($scope.Param).then(function (result) {
        }).catch(function () {
            AutoClosingErrorAlert('Error in Update  !', 5000);

        }).finally(function () {
        });
    }
    $scope.ChangeEndUserPrice = function (_children) {
        if (_children.SPFixedPrice != undefined) {
            _children.MarkUpForSPlist = (((_children.EndUserPrice) - (_children.SPFixedPrice)) / (_children.EndUserPrice)) * 100;

        }
        if (_children.QOFixedPrice != undefined) {
            _children.MarkUpForQoutation = (((_children.EndUserPrice) - (_children.QOFixedPrice)) / (_children.EndUserPrice)) * 100;

        }
        if (_children.PEYFixedPrice != undefined) {
            _children.MarkUpForPeyvast = (((_children.EndUserPrice) - (_children.PEYFixedPrice)) / (_children.EndUserPrice)) * 100;
        }
    }


    $scope.ChangeSituation = function (id, el) {
        $('.Collapse' + id).fadeToggle();

        $(el.target).parent().children().closest("#icon").toggleClass('fa-minus fa-plus');
    }



    $scope.UpdateEndUserPrice = function (_item)
    {

        $scope.Param = {
            EndUserPrice: _item.EndUserPrice,
            MarkUpForSPlist: _item.MarkUpForSPlist,
            MarkUpForQoutation: _item.MarkUpForQoutation,
            MarkUpForPeyvast: _item.MarkUpForPeyvast,
            IDPreparationPriceListDetail: _item.IDPreparationPriceListDetail
        }
        PreparationPriceListDetailService.UpdateEndUserPriceByIDPreparationPriceListDetail($scope.Param).then(function (result) {
            AutoClosingSuccessAlert('update was successful !', 5000);
        }).catch(function () {
            AutoClosingErrorAlert('Error in Update  !', 5000);

            }).finally(function () {

        });
    }
    $scope.IncreaseAllEndUserPriceForWithOutCategory = function (_Percentage, _IDPreparationPriceList) {

        $scope.Param = {
            Percentage: _Percentage,
            IDPreparationPriceList: _IDPreparationPriceList
        }
        PreparationPriceListDetailService.IncreaseAllEndUserPriceForWithOutCategory($scope.Param).then(function (result) {
            AutoClosingSuccessAlert('All endUser prices are increased !', 5000);
            $('#ModalPreparationPriceListWithOutCategory').modal('hide');

        }).catch(function () {
            AutoClosingErrorAlert('Error in Update  !', 5000);

        }).finally(function () {

        });
    }
    $scope.DecreaseAllEndUserPriceForWithOutCategory = function (_Percentage, _IDPreparationPriceList) {

        $scope.Param = {
            Percentage: _Percentage,
            IDPreparationPriceList: _IDPreparationPriceList
        }
        PreparationPriceListDetailService.DecreaseAllEndUserPriceForWithOutCategory($scope.Param).then(function (result) {
            AutoClosingSuccessAlert('All endUser prices are decreased !', 5000);
            $('#ModalPreparationPriceListWithOutCategory').modal('hide');

        }).catch(function () {
            AutoClosingErrorAlert('Error in Update  !', 5000);

        }).finally(function () {

        });
    }

    $scope.IncreaseAllEndUserPriceForByCategory = function (_Percentage, _IDPreparationPriceList) {

        $scope.Param = {
            Percentage: _Percentage,
            IDPreparationPriceList: _IDPreparationPriceList
        }
        PreparationPriceListDetailService.IncreaseAllEndUserPriceForByCategory($scope.Param).then(function (result) {
            AutoClosingSuccessAlert('All endUser prices are increased !', 5000);
        }).catch(function () {
            AutoClosingErrorAlert('Error in Update  !', 5000);

        }).finally(function () {

        });
    }
    $scope.DecreaseAllEndUserPriceForByCategory = function (_Percentage, _IDPreparationPriceList) {

        $scope.Param = {
            Percentage: _Percentage,
            IDPreparationPriceList: _IDPreparationPriceList
        }
        PreparationPriceListDetailService.DecreaseAllEndUserPriceForByCategory($scope.Param).then(function (result) {
            AutoClosingSuccessAlert('All endUser prices are decreased !', 5000);

        }).catch(function () {
            AutoClosingErrorAlert('Error in Update  !', 5000);

        }).finally(function () {

        });
    }

    $scope.ShowListOfQoutationsByPartNumber = function (_children) {
        $scope.SelectRowForChangingQoutation = _children;
        $scope.CurrentItemInModalListOfQoutation = {};
        $scope.CurrentItemInModalListOfQoutation.PartNumber = _children.PartNumber;
        $scope.CurrentItemInModalListOfQoutation.IDPreparationPriceListDetail = _children.IDPreparationPriceListDetail;
        $scope.CurrentItemInModalListOfQoutation.EndUserPrice = _children.EndUserPrice;
        $scope.Param = {
            PartNumber: _children.PartNumber
        }
        QouationDetailService.GetQouationDetailByPartNumber($scope.Param).then(function (result) {
            $scope.ListOfPartNumbersInModalListOfQoutationByPartNumber = result.data;
        }).catch(function () {
            AutoClosingErrorAlert('Error in Get  !', 5000);

        }).finally(function () {
            $('#ModalListOfQoutationByPartNumber').modal('show');
        });
    }
    $scope.SaveQoutationDetainInPreparationListDetail = function (_Item) {

        $scope.Param = {
            IDPreparationPriceListDetail: $scope.CurrentItemInModalListOfQoutation.IDPreparationPriceListDetail,
            QOFixedPrice: _Item.FixedPrice,
            QOMarkUp: _Item.MarkUp,
            QOSalesPrice: _Item.SalesPrice,
            EndUserPrice: $scope.CurrentItemInModalListOfQoutation.EndUserPrice.replace(/,/g, '')
        }
        PreparationPriceListDetailService.UpdateQoutationDetainInPreparationPriceListDetail($scope.Param).then(function (result) {
            $scope.SelectRowForChangingQoutation.QOFixedPrice = _Item.FixedPrice;
            $scope.SelectRowForChangingQoutation.QOMarkUp = _Item.MarkUp;
            $scope.SelectRowForChangingQoutation.QOSalesPrice = _Item.SalesPrice;
            AutoClosingSuccessAlert('Update was successful !', 5000);

        }).catch(function () {
            AutoClosingErrorAlert('Error in update  !', 5000);

        }).finally(function () {
            $('#ModalListOfQoutationByPartNumber').modal('show');
        });
    }

}]);

