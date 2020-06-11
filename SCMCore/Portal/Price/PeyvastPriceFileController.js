myApp.controller('PeyvastPriceFileController', ['$scope', 'ngProgress', 'SupplierService', 'CurrencyService', 'PeyvastPriceFileService', 'PeyvastPriceDetailService', function ($scope, ngProgress, SupplierService, CurrencyService, PeyvastPriceFileService, PeyvastPriceDetailService) {

    $scope.CurrentPeyvastPrice = {};
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

    $scope.NewPeyvastPriceFile = function () {

        $scope.CurrentPeyvastPrice.Title = "";
        $scope.CurrentPeyvastPrice.OrigDate = "";

        $scope.CurrentPeyvastPrice.Supplier = $scope.Suppliers[0].value;
        $scope.CurrentPeyvastPrice.Currency = $scope.Currencies[0].value;
        $scope.ClearExcelFileUploadPeyvastPrice();
        document.getElementById("AddPeyvastPriceFile").value = "Add";
        document.getElementById("excelFileUploadPeyvastPrice").disabled = false;
    }
    $scope.ClearExcelFileUploadPeyvastPrice = function () {

        document.getElementById("urlNameExcelPeyvastPrice").value = "";
        document.getElementById("excelFileUploadPeyvastPrice").value = "";
        $scope.SelectedExcelFile = undefined;
    }
    $scope.ChangeExcelFilePeyvastPrice = function (element) {

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
        document.getElementById("urlNameExcelPeyvastPrice").value = element.files[0].name;
        $scope.SelectedExcelFile = element;
        
        $scope.ConvertExcelToJson('excelFileUploadPeyvastPrice');

    }
    $scope.CancelPeyvastPriceFile = function (_item) {
        $scope.NewPeyvastPriceFile();
    }

    $scope.AddPeyvastPriceFile = function () {

        if (document.getElementById("AddPeyvastPriceFile").value == "Add") {
            var elementExcel = $scope.SelectedExcelFile;
            if (elementExcel == undefined) {
                AutoClosingErrorAlert('Please select your excel file !', 5000);
                return;
            }
            ngProgress.start();

            var _IDPeyvastPriceFile = NewGuid();
            var _TitlePeyvastPrice = $scope.CurrentPeyvastPrice.Title;
            var _IDSupplier = $scope.CurrentPeyvastPrice.Supplier;
            var _IDCurrency = $scope.CurrentPeyvastPrice.Currency;
            var _OrigDate = $scope.CurrentPeyvastPrice.OrigDate;
            $scope.filesExcel = [];
            for (var i = 0; i < elementExcel.files.length; i++) {
                $scope.filesExcel.push(elementExcel.files[i])
            }
            var data = new FormData();

            for (var i in $scope.filesExcel) {
                data.append("excelFileUploadPeyvastPrice", $scope.filesExcel[i]);
            }
            data.append("IDLogUser", _IDLogUser);
            data.append("IDPeyvastPriceFile", _IDPeyvastPriceFile);
            data.append("TitlePeyvastPrice", _TitlePeyvastPrice);
            data.append("IDSupplier", _IDSupplier);
            data.append("IDCurrency", _IDCurrency);
            data.append("OrigDate", _OrigDate);
            data.append("ExcelJsonPeyvastPrice", JSON.stringify($scope.excelJsonPeyvastPrice));

            PeyvastPriceFileService.AddPeyvastPriceFile(data).then(function (result) {
                $scope.NewPeyvastPriceFile();
                ngProgress.stop();
                $scope.GetPeyvastPriceFile(_IDSupplier);
                AutoClosingSuccessAlert('Registration was successful !', 5000);
            }).catch(function () {
                AutoClosingErrorAlert('Error in registration !', 5000);

            }).finally(function () {
                ngProgress.complete();
                ngProgress.stop();
            });
        }
        else if (document.getElementById("AddPeyvastPriceFile").value == "Update") {
            ngProgress.start();
            var _IDPeyvastPriceFile = $scope.CurrentPeyvastPrice.IDPeyvastPriceFile;
            var _Title = $scope.CurrentPeyvastPrice.Title;
            var _IDSupplier = $scope.CurrentPeyvastPrice.Supplier;
            var _IDCurrency = $scope.CurrentPeyvastPrice.Currency;
            var _CurrencyValue = $scope.CurrentPeyvastPrice.CurrencyValue;
            var _OrigDate = $scope.CurrentPeyvastPrice.OrigDate;

            $scope.Param = {
                IDPeyvastPriceFile: _IDPeyvastPriceFile,
                IDLogUser: _IDLogUser,
                Title: _Title,
                IDSupplier: _IDSupplier,
                IDCurrency: _IDCurrency,
                CurrencyValue: _CurrencyValue,
                OrigDate: _OrigDate

            };
            PeyvastPriceFileService.UpdatePeyvastPriceFile($scope.Param).then(function (result) {
                $scope.NewPeyvastPriceFile();
                ngProgress.stop();
                $scope.GetPeyvastPriceFile(_IDSupplier);
                AutoClosingSuccessAlert('Update was successful !', 5000);
            }).catch(function () {
                AutoClosingErrorAlert('Error in Update !', 5000);

            }).finally(function () {
                ngProgress.complete();
                ngProgress.stop();
            });
        }


    }
    $scope.DeletePeyvastPriceFile = function (_IDPeyvastPriceFile, _IDSupplier, _FileUrlExcel) {
        var Accept = confirm("Are you sure ?");
        if (Accept == true) {

            $scope.Param = {
                IDPeyvastPriceFile: _IDPeyvastPriceFile,
                FileUrlExcel: _FileUrlExcel
            };
            PeyvastPriceFileService.DeletePeyvastPriceFile($scope.Param).then(function (result) {
                $scope.NewPeyvastPriceFile();
                $scope.GetPeyvastPriceFile(_IDSupplier);
                AutoClosingSuccessAlert('Delete was successful !', 5000);
            }).catch(function () {
                AutoClosingErrorAlert('Error in Delete !', 5000);

            }).finally(function () {
                ngProgress.complete();
            });

        }
    }
    $scope.EditPeyvastPriceFile = function (_item) {
        
        $scope.CurrentPeyvastPrice.IDPeyvastPriceFile = _item.IDPeyvastPriceFile;
        $scope.CurrentPeyvastPrice.Supplier = _item.IDSupplier;
        $scope.CurrentPeyvastPrice.Currency = _item.IDCurrency;
        $scope.CurrentPeyvastPrice.Title = _item.Title;
        $scope.CurrentPeyvastPrice.CurrencyValue = _item.CurrencyValue;
        $scope.CurrentPeyvastPrice.OrigDate = _item.OrigDate.replace('T', ' ')

        document.getElementById("AddPeyvastPriceFile").value = "Update";
        document.getElementById("excelFileUploadPeyvastPrice").disabled = true;
        $('html, body').animate({
            scrollTop: $("#panelBodyPeyvastPriceFile").offset().top - 100
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
                            var exceljson = XLSX.utils.sheet_to_json(workbook.Sheets[y]);
                        }
                        else {
                            var exceljson = XLS.utils.sheet_to_row_object_array(workbook.Sheets[y]);
                        }
                        
                        $scope.CheckPartNumberInFile(exceljson);
                    }
                });

            }

        }
        else {
            alert("Sorry! Your browser does not support HTML5!");
        }
    }

    $scope.GetPeyvastPriceFile = function (_IDSupplier) {
        ngProgress.start();
        $scope.Param = {
            IDSupplier: _IDSupplier
        }
        PeyvastPriceFileService.GetPeyvastPriceFile($scope.Param).then(function (result) {

            $scope.PeyvastPriceFiles = result.data;
            $('#TablePeyvastPriceFile').show();
        }).catch(function () {
            AutoClosingErrorAlert('Error in catch data !', 5000);


        }).finally(function () {
            ngProgress.complete();
            ngProgress.stop();
        });
    }
    $scope.ShowPeyvastPriceDetail = function (_item) {

        if ($scope.activeRow == _item.IDPeyvastPriceFile) {
            $scope.activeRow = false;
        }
        else {
            $('#imgLoaderPeyvastPrice' + _item.IDPeyvastPriceFile).show();
            $scope.Param = {
                IDPeyvastPriceFile: _item.IDPeyvastPriceFile
            }
            PeyvastPriceDetailService.GetPeyvastPriceDetail($scope.Param).then(function (result) {
                $scope.PeyvastPriceDetails = result.data;
            }).catch(function () {
                AutoClosingErrorAlert('Error in catch data !', 5000);

            }).finally(function () {
                $('#imgLoaderPeyvastPrice' + _item.IDPeyvastPriceFile).hide();
                $scope.GetSumValuesOfColumns(_item.IDPeyvastPriceFile);
                $scope.activeRow = _item.IDPeyvastPriceFile;
            });
        }




    }

    $scope.CheckPartNumberInFile = function (_exceljson) {
        PeyvastPriceFileService.CheckPartNumberInPeyvastPriceFile(_exceljson).then(function (result) {
            
            if (result.data.length > 0) {

                var str = '';
                for (var item in result.data) {
                    str += result.data[item].PartNumber;
                    if (item < result.data.length - 1) {
                        str = str + ',';
                    }


                }
                $scope.ClearExcelFileUploadPeyvastPrice();
                AutoClosingErrorAlert('Error : ' + str + ' not define !', 20000);


            }
            else {
             
                $scope.excelJsonPeyvastPrice = _exceljson;
            }


        }).catch(function () {
            AutoClosingErrorAlert('Error in catch data !', 5000);
        }).finally(function () {

        });
    }

    $scope.UpdateRatioPeyvastPrice = function () {
        ngProgress.start();
        $scope.activeRow = false;
        var _IDSupplier = $scope.PeyvastPriceFiles[0].IDSupplier;
        var Param = {
            RatioRial: $scope.PeyvastPrice.RatioRial,
            RatioMarkUp: $scope.PeyvastPrice.RatioMarkUp,
            IDSupplier: _IDSupplier
        };
        PeyvastPriceFileService.UpdateRaioPeyvastPriceFile(Param).then(function (result) {
            ngProgress.stop();
            $scope.GetPeyvastPriceFile(_IDSupplier);
            AutoClosingSuccessAlert('Update was successful !', 5000);
        }).catch(function () {
            AutoClosingErrorAlert('Error in Update !', 5000);
        }).finally(function () {
            ngProgress.complete();
            ngProgress.stop();
        });
    }

    $scope.ChangePeyvastSalesPriceInput1 = function (_children) {

        _children.MarkUp1 = (((_children.SalesPrice1) - (_children.Price)) / (_children.SalesPrice1)) * 100;
        $('#SalesPriceInput1' + _children.IDPeyvastPriceDetail).css('border', 'solid 1px Blue');
    }
    $scope.ChangePeyvastSalesPriceInput2 = function (_children) {

        _children.MarkUp2 = (((_children.SalesPrice2) - (_children.FixedPrice2)) / (_children.SalesPrice2)) * 100;
        $('#SalesPriceInput2' + _children.IDPeyvastPriceDetail).css('border', 'solid 1px Blue');
    }
    $scope.GetSumValuesOfColumns = function (_IDPeyvastPriceFile) {
        $scope.Param = {
            IDPeyvastPriceFile: _IDPeyvastPriceFile
        }
        PeyvastPriceDetailService.GetSumsColumns($scope.Param).then(function (result) {
            $scope.SumPrice = result.data[0].SumPrice;
            $scope.SumFixedPrice2 = result.data[0].FixedPrice2;
            $scope.SumSalesPrice1 = result.data[0].SumSalesPrice1;
            $scope.SumSalesPrice2 = result.data[0].SumSalesPrice2;


        }).catch(function () {
            AutoClosingErrorAlert('Error in catch data !', 5000);

        }).finally(function () {
        });
    }
    $scope.ShowModalSortPeyvast = function () {
        $('#ModalSortPeyvast').modal('show');
    }
    $scope.SortTablePeyvast = {
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
                PeyvastPriceFileService.ChangeSortInPeyvastFile(myJsonString).then(function (result) {
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