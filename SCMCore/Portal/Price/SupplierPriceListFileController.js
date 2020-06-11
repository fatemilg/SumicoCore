myApp.controller('SupplierPriceListFileController', ['$scope', '$filter', 'ngProgress', 'SupplierService', 'CurrencyService', 'SupplierPriceListFileService', 'SupplierPriceListDetailService', function ($scope,$filter, ngProgress, SupplierService, CurrencyService, SupplierPriceListFileService, SupplierPriceListDetailService) {

    $scope.CurrentSupplierPriceList = {};
    $scope.SupplierPrice = {};
    $scope.CheckValidationType = function (element) {

        var validFilesTypes = element.attributes['data-FileType'].value.split(',');
        var FileType = element.files[0].name.split('.').pop();
        var isValidFileType = false;
        for (var item in validFilesTypes) {
            if (FileType == validFilesTypes[item]) {
                isValidFileType
                    = true;
                break;
            }
        }
        if (!isValidFileType) {

            return false;
        }
        else {
            return true;
        }

    }
    $scope.CheckValidationSize = function (element) {

        var FileSize = element.files[0].size;
        if (FileSize > 1024 * 1024) {

            return false;
        }
        else {
            return true;
        }



    }

    $scope.NewSupplierPriceListFile = function () {

        $scope.CurrentSupplierPriceList.Title = "";
        $scope.CurrentSupplierPriceList.Supplier = $scope.Suppliers[0].value;
        $scope.CurrentSupplierPriceList.Currency = $scope.Currencies[0].value;
        $scope.ClearExcelFileUploadSupplierPriceList();
        document.getElementById("AddSupplierPriceListFile").value = "Add";
        document.getElementById("excelFileUploadSupplierPriceList").disabled = false;

    }
    $scope.ClearExcelFileUploadSupplierPriceList = function () {

        document.getElementById("urlNameSupplierPriceList").value = "";
        document.getElementById("excelFileUploadSupplierPriceList").value = "";
        $scope.SelectedFile = undefined;
    }
    $scope.ChangeFileSupplierPriceList = function (element) {

        var validType = $scope.CheckValidationType(element)
        var validSize = $scope.CheckValidationSize(element)
        if (!validType) {
            AutoClosingErrorAlert('Select the file with Excel extension(xlsx,xls) !', 5000);
            return;
        }
        if (!validSize) {
            AutoClosingErrorAlert('The file size should be less than 1 MB !', 5000);
            return;
        }
        document.getElementById("urlNameSupplierPriceList").value = element.files[0].name;
        $scope.SelectedFile = element;
        $scope.ConvertExcelToJson('excelFileUploadSupplierPriceList');

    }
    $scope.CancelSupplierPriceListFile = function (_item) {
        $scope.NewSupplierPriceListFile();
    }

    $scope.AddSupplierPriceListFile = function () {
    
        document.getElementById("excelFileUploadSupplierPriceList").disabled = false;
        if (document.getElementById("AddSupplierPriceListFile").value == "Add") {
            var element = $scope.SelectedFile;
            if (element == undefined) {
                AutoClosingErrorAlert('Please select your excel file !', 5000);
                return;
            }
               ngProgress.start();
                var _IDSupplierPriceListFile = NewGuid();
                var _TitleSupplierPriceList = document.getElementById("TitleSupplierPriceList").value;
                var _IDSupplier = $scope.CurrentSupplierPriceList.Supplier;
                var _IDCurrency = $scope.CurrentSupplierPriceList.Currency;
                $scope.files = [];
                for (var i = 0; i < element.files.length; i++) {
                    $scope.files.push(element.files[i])
                }
                var data = new FormData();

                for (var i in $scope.files) {
                    data.append("excelFileUploadSupplierPriceList", $scope.files[i]);
                }
                data.append("IDLogUser", _IDLogUser);
                data.append("IDSupplierPriceListFile", _IDSupplierPriceListFile);
                data.append("TitleSupplierPriceList", _TitleSupplierPriceList);
                data.append("IDSupplier", _IDSupplier);
                data.append("IDCurrency", _IDCurrency);
                data.append("ExcelJsonSupplierPriceList", JSON.stringify($scope.excelJsonSupplierPriceList));

                SupplierPriceListFileService.AddSupplierPriceListFile(data).then(function (result) {
                    $scope.NewSupplierPriceListFile();
                    ngProgress.stop();
                    $scope.GetSupplierPriceListFile(_IDSupplier);
                    AutoClosingSuccessAlert('Registration was successful !', 5000);
                }).catch(function () {
                    AutoClosingErrorAlert('Error in registration !', 5000);

                }).finally(function () {
                    ngProgress.complete();
                    ngProgress.stop();
                });
       
        }
        else if (document.getElementById("AddSupplierPriceListFile").value == "Update") {
            ngProgress.start();
            var _IDSupplierPriceListFile = $scope.CurrentSupplierPriceList.IDSupplierPriceListFile;
            var _Title = $scope.CurrentSupplierPriceList.Title;
            var _IDSupplier = $scope.CurrentSupplierPriceList.Supplier;
            var _IDCurrency = $scope.CurrentSupplierPriceList.Currency;
            $scope.Param = {
                IDSupplierPriceListFile: _IDSupplierPriceListFile,
                IDLogUser: _IDLogUser,
                Title: _Title,
                IDSupplier: _IDSupplier,
                IDCurrency: _IDCurrency
            };
            SupplierPriceListFileService.UpdateSupplierPriceListFile($scope.Param).then(function (result) {
                $scope.NewSupplierPriceListFile();
                ngProgress.stop();
                $scope.GetSupplierPriceListFile(_IDSupplier);
                AutoClosingSuccessAlert('Update was successful !', 5000);
            }).catch(function () {
                AutoClosingErrorAlert('Error in Update !', 5000);

            }).finally(function () {
                ngProgress.complete();
                ngProgress.stop();
            });
        }


    }
    $scope.DeleteSupplierPriceListFile = function (_IDSupplierPriceListFile, _IDSupplier, _FileUrl) {
        var Accept = confirm("Are you sure ?");
        if (Accept == true) {
            $scope.Param = {
                IDSupplierPriceListFile: _IDSupplierPriceListFile,
                FileUrl: _FileUrl
            };
            SupplierPriceListFileService.DeleteSupplierPriceListFile($scope.Param).then(function (result) {
                $scope.NewSupplierPriceListFile();
                $scope.GetSupplierPriceListFile(_IDSupplier);
                AutoClosingSuccessAlert('Delete was successful !', 5000);
            }).catch(function () {
                AutoClosingErrorAlert('Error in Delete !', 5000);

            }).finally(function () {
                ngProgress.complete();
            });

        }
    }
    $scope.EditSupplierPriceListFile = function (_item) {
        $scope.CurrentSupplierPriceList.IDSupplierPriceListFile = _item.IDSupplierPriceListFile;
        $scope.CurrentSupplierPriceList.Supplier = _item.IDSupplier;
        $scope.CurrentSupplierPriceList.Currency = _item.IDCurrency;
        $scope.CurrentSupplierPriceList.Title = _item.Title;

        document.getElementById("AddSupplierPriceListFile").value = "Update";
        document.getElementById("excelFileUploadSupplierPriceList").disabled = true;
        $('html, body').animate({
            scrollTop: $("#panelBodySupplierPriceListFile").offset().top - 100
        }, 1000);


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

    $scope.GetCurrencies = function () {
        CurrencyService.GetCurrency().then(function (result) {
            $scope.Currencies = result.data;

        }).catch(function () {
            AutoClosingErrorAlert('Error in catch data !', 5000);


        }).finally(function () {

        });
    }
    $scope.GetCurrencies();
    $scope.ConvertExcelToJson = function (IDUploadFile) {
        var xlsxflag = false; /*Flag for checking whether excel is .xls format or .xlsx format*/
        if ($("#" + IDUploadFile).val().toLowerCase().indexOf(".xlsx") > 0) {
            xlsxflag = true;
        }
        else if ($("#" + IDUploadFile).val().toLowerCase().indexOf(".xls") > 0) {

            xlsxflag = false;
        }

        /*Checks whether the browser supports HTML5*/
        if (typeof (FileReader) != "undefined") {
            var reader = new FileReader();

            if (xlsxflag) {/*If excel file is .xlsx extension than creates a Array Buffer from excel*/

                reader.readAsArrayBuffer($("#" + IDUploadFile)[0].files[0]);
            }
            else {
                reader.readAsBinaryString($("#" + IDUploadFile)[0].files[0]);
            }

            reader.onload = function (e) {

                var data = e.target.result;
                /*Converts the excel data in to object*/
                if (xlsxflag) {
                    var workbook = XLSX.read(data, { type: 'binary' });
                }
                else {
                    var workbook = XLS.read(data, { type: 'binary' });
                }
                /*Gets all the sheetnames of excel in to a variable*/
                var sheet_name_list = workbook.SheetNames;
                sheet_name_list.forEach(function (y) { /*Iterate through all sheets*/
                    /*Convert the cell value to Json*/
                    if (y == "Sheet1") {
                        if (xlsxflag) {
                            $scope.excelJsonSupplierPriceList = XLSX.utils.sheet_to_json(workbook.Sheets[y]);
                        }
                        else {
                            $scope.excelJsonSupplierPriceList = XLS.utils.sheet_to_row_object_array(workbook.Sheets[y]);
                        }

                    }
                });

            }

        }
        else {
            alert("Sorry! Your browser does not support HTML5!");
        }
    }

    $scope.GetSupplierPriceListFile = function (_IDSupplier) {
        ngProgress.start();
        $scope.Param = {
            IDSupplier: _IDSupplier
        }
        SupplierPriceListFileService.GetSupplierPriceListFile($scope.Param).then(function (result) {
            $scope.SupplierPriceListFiles = result.data;
            $('#TableSupplierPriceListFile').show();
        }).catch(function () {
            AutoClosingErrorAlert('Error in catch data !', 5000);
        }).finally(function () {
            ngProgress.complete();
            ngProgress.stop();
        });
    }
    $scope.ShowSupplierPriceListDetail = function (_item) {
        if ($scope.activeRow == _item.IDSupplierPriceListFile) {
            $scope.activeRow = false;
        }
        else {
            $('#imgLoaderSupplierPrice' + _item.IDSupplierPriceListFile).show();
            $scope.Param = {
                IDSupplierPriceListFile: _item.IDSupplierPriceListFile
            }
            SupplierPriceListDetailService.GetSupplierPriceListDetail($scope.Param).then(function (result) {
                $scope.SupplierPriceListDetails = result.data;
            }).catch(function () {
                AutoClosingErrorAlert('Error in catch data !', 5000);

            }).finally(function () {
                $('#imgLoaderSupplierPrice' + _item.IDSupplierPriceListFile).hide();
                $scope.GetSumValuesOfColumns(_item.IDSupplierPriceListFile);
                $scope.activeRow = _item.IDSupplierPriceListFile;
            });
        }
    }

    $scope.UpdateRatioSupplierPriceList = function () {
        ngProgress.start();
        $scope.activeRow = false;
        var _IDSupplier = $scope.SupplierPriceListFiles[0].IDSupplier;
        var Param = {
            RatioRial: $scope.SupplierPrice.RatioRial,
            RatioTransfer: $scope.SupplierPrice.RatioTransfer,
            RatioMarkUp: $scope.SupplierPrice.RatioMarkUp,
            IDSupplier: _IDSupplier
        };
        SupplierPriceListFileService.UpdateRaioSupplierPriceListFile(Param).then(function (result) {
            ngProgress.stop();
            $scope.GetSupplierPriceListFile(_IDSupplier);
            AutoClosingSuccessAlert('Update was successful !', 5000);
        }).catch(function () {
            AutoClosingErrorAlert('Error in Update !', 5000);
        }).finally(function () {
            ngProgress.complete();
            ngProgress.stop();
        });
    }
    $scope.ChangeSupplierSalesPriceInput = function (_children) {

        _children.MarkUp = (((_children.SalesPrice) - (_children.FixedPrice)) / (_children.SalesPrice)) * 100;
        $('#SalesPriceInput' + _children.IDSupplierPriceListDetail).css('border', 'solid 1px Blue');
    }
    $scope.GetSumValuesOfColumns = function (_IDSupplierPriceListFile) {
        $scope.Param = {
            IDSupplierPriceListFile: _IDSupplierPriceListFile
        }
        SupplierPriceListDetailService.GetSumsColumns($scope.Param).then(function (result) {
            $scope.SumPrice = result.data[0].SumPrice;
            $scope.SumFixedPrice = result.data[0].SumFixedPrice;
            $scope.SumSalesPrice = result.data[0].SumSalesPrice;
            
        }).catch(function () {
            AutoClosingErrorAlert('Error in catch data !', 5000);

        }).finally(function () {
        });
    }

    $scope.ShowModalSortSupplierPrice = function () {
        $('#ModalSortSupplierPrice').modal('show');
    }
    $scope.SortTableSupplierPrice = {
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
                SupplierPriceListFileService.ChangeSortInSupplierPriceFile(myJsonString).then(function (result) {
                    if (result.data == false) {
                        AutoClosingErrorAlert('Error in movement!', 5000);
                        return;
                    }

                }).catch(function () {
                    AutoClosingErrorAlert('Error in movement!', 5000);

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