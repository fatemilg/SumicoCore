myApp.controller('QouationFileController', ['$scope', 'ngProgress', 'SupplierService', 'CurrencyService', 'QouationFileService', 'QouationDetailService', 'DefineDetailProductService', function ($scope, ngProgress, SupplierService, CurrencyService, QouationFileService, QouationDetailService, DefineDetailProductService) {

    $('#chkExcelModeQouation').bootstrapToggle('off');
    $scope.CurrentQouation = {};
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

    $scope.NewQouationFile = function () {

        $scope.CurrentQouation.Title = "";
        $scope.CurrentQouation.OrigDate = "";
        $scope.CurrentQouation.Supplier = $scope.Suppliers[0].value;
        $scope.CurrentQouation.Currency = $scope.Currencies[0].value;
        $scope.ClearExcelFileUploadQouation();
        $scope.ClearPdfFileUploadQouation();
        $scope.ClearEmailFileUploadQouation();
        document.getElementById("AddQouationFile").value = "Add";
        document.getElementById("PdfFileUploadQouation").disabled = false;
        document.getElementById("excelFileUploadQouation").disabled = false;
    }
    $scope.ClearExcelFileUploadQouation = function () {

        document.getElementById("urlNameExcelQouation").value = "";
        document.getElementById("excelFileUploadQouation").value = "";
        document.getElementById("PdfFileUploadQouation").value = "";
        $scope.SelectedExcelFile = undefined;
        $scope.SelectedPdfFile = undefined;
    }
    $scope.ChangeExcelFileQouation = function (element) {

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
        document.getElementById("urlNameExcelQouation").value = element.files[0].name;
        $scope.SelectedExcelFile = element;
        $scope.ConvertExcelToJson('excelFileUploadQouation');

    }
    $scope.CancelQouationFile = function (_item) {
        $scope.NewQouationFile();
    }

    $scope.AddQouationFile = function () {

        if (document.getElementById("AddQouationFile").value == "Add") {
            var data = new FormData();

            //Excell
            if ($scope.ExcelModeQouation) {
                var elementExcel = $scope.SelectedExcelFile;
                if (elementExcel == undefined) {
                    AutoClosingErrorAlert('Please select your excel file !', 5000);
                    return;
                }
                else {
                    $scope.filesExcel = [];
                    for (var i = 0; i < elementExcel.files.length; i++) {
                        $scope.filesExcel.push(elementExcel.files[i])
                    }
                    for (var i in $scope.filesExcel) {
                        data.append("excelFileUploadQouation", $scope.filesExcel[i]);
                    }
                    data.append("ExcelJsonQouation", JSON.stringify($scope.excelJsonQouation));

                }
            }
            //Pdf
            var elementPdf = $scope.SelectedPdfFile;
            if (elementPdf == undefined) {
                AutoClosingErrorAlert('Please select your pdf file !', 5000);
                return;
            }
            else {
                $scope.filesPdf = [];
                for (var i = 0; i < elementPdf.files.length; i++) {
                    $scope.filesPdf.push(elementPdf.files[i])
                }
                for (var i in $scope.filesPdf) {
                    data.append("PdfFileUploadQouation", $scope.filesPdf[i]);
                }
            }
           //Email
            var elementEmail = $scope.SelectedEmailFile;
            if (elementEmail != undefined) {
        
                $scope.filesEmail = [];
                for (var i = 0; i < elementEmail.files.length; i++) {
                    $scope.filesEmail.push(elementEmail.files[i])
                }
                for (var i in $scope.filesEmail) {
                    data.append("EmailFileUploadQouation", $scope.filesEmail[i]);
                }
            }
            ngProgress.start();

            var _IDQouationFile = NewGuid();
            var _TitleQouation = document.getElementById("TitleQouation").value;
            var _IDSupplier = $scope.CurrentQouation.Supplier;
            var _IDCurrency = $scope.CurrentQouation.Currency;
            var _OrigDate = $scope.CurrentQouation.OrigDate;


            data.append("IDLogUser", _IDLogUser);
            data.append("IDQouationFile", _IDQouationFile);
            data.append("TitleQouation", _TitleQouation);
            data.append("IDSupplier", _IDSupplier);
            data.append("IDCurrency", _IDCurrency);
            data.append("OrigDate", _OrigDate);
            data.append("ExcelModeQouation", $scope.ExcelModeQouation);

            QouationFileService.AddQouationFile(data).then(function (result) {
                $scope.NewQouationFile();
                ngProgress.stop();
                $scope.GetQouationFile(_IDSupplier);
                AutoClosingSuccessAlert('Registration was successful !', 5000);
            }).catch(function () {
                AutoClosingErrorAlert('Error in registration !', 5000);

            }).finally(function () {
                ngProgress.complete();
                ngProgress.stop();
            });
        }
        else if (document.getElementById("AddQouationFile").value == "Update") {
            ngProgress.start();
            var _IDQouationFile = $scope.CurrentQouation.IDQouationFile;
            var _Title = $scope.CurrentQouation.Title;
            var _IDSupplier = $scope.CurrentQouation.Supplier;
            var _IDCurrency = $scope.CurrentQouation.Currency;
            var _OrigDate = $scope.CurrentQouation.OrigDate;
            $scope.Param = {
                IDQouationFile: _IDQouationFile,
                IDLogUser: _IDLogUser,
                Title: _Title,
                IDSupplier: _IDSupplier,
                IDCurrency: _IDCurrency,
                OrigDate: _OrigDate,
                NewFileUrlEmail: $scope.CurrentQouation.NewFileUrlEmail,
                OldFileUrlEmail: $scope.CurrentQouation.OldFileUrlEmail,
                OldFileSizeEmail: $scope.CurrentQouation.OldFileSizeEmail,
                TitleQoutationEmailFile: document.getElementById("urlNameEmailQouation").value
            };
            QouationFileService.UpdateQouationFile($scope.Param).then(function (result) {
                $scope.NewQouationFile();
                ngProgress.stop();
                $scope.GetQouationFile(_IDSupplier);
                AutoClosingSuccessAlert('Update was successful !', 5000);
            }).catch(function () {
                AutoClosingErrorAlert('Error in Update !', 5000);

            }).finally(function () {
                ngProgress.complete();
                ngProgress.stop();
            });
        }


    }
    $scope.DeleteQouationFile = function (_IDQouationFile, _IDSupplier, _FileUrlExcel, _FileUrlPdf, _FileUrlEmail) {
        var Accept = confirm("Are you sure ?");
        if (Accept == true) {

            $scope.Param = {
                IDQouationFile: _IDQouationFile,
                FileUrlExcel: _FileUrlExcel,
                FileUrlPdf: _FileUrlPdf,
                FileUrlEmail: _FileUrlEmail
            };
            QouationFileService.DeleteQouationFile($scope.Param).then(function (result) {
                $scope.NewQouationFile();
                $scope.GetQouationFile(_IDSupplier);
                AutoClosingSuccessAlert('Delete was successful !', 5000);
            }).catch(function () {
                AutoClosingErrorAlert('Error in Delete !', 5000);

            }).finally(function () {
                ngProgress.complete();
            });

        }
    }
    $scope.EditQouationFile = function (_item) {
        $scope.CurrentQouation.IDQouationFile = _item.IDQouationFile;
        $scope.CurrentQouation.Supplier = _item.IDSupplier;
        $scope.CurrentQouation.Currency = _item.IDCurrency;
        $scope.CurrentQouation.Title = _item.Title;
        $scope.CurrentQouation.OrigDate = _item.OrigDate.replace('T', ' ');

        document.getElementById("AddQouationFile").value = "Update";
        document.getElementById("excelFileUploadQouation").disabled = true;
        document.getElementById("PdfFileUploadQouation").disabled = true;
        if (_item.FileUrlEmail != undefined)
        {
            document.getElementById("urlNameEmailQouation").value = _item.FileUrlEmail.split('@')[1];
            $scope.CurrentQouation.OldFileUrlEmail = _item.FileUrlEmail;
            $scope.CurrentQouation.OldFileSizeEmail = _item.FileSizeEmail;
        }
        else
        {
            document.getElementById("urlNameEmailQouation").value = "";
            $scope.CurrentQouation.OldFileUrlEmail = undefined;
            $scope.CurrentQouation.OldFileSizeEmail = undefined;
            $scope.CurrentQouation.NewFileUrlEmail = undefined;
        }
        
        if (_item.ExcelMode)
        {
            $('#divExcelFileQouation').show();
            $('#chkExcelModeQouation').bootstrapToggle('on');
        }
        else
        {
            $('#divExcelFileQouation').hide();
            $('#chkExcelModeQouation').bootstrapToggle('off');
        }
        $('html, body').animate({
            scrollTop: $("#panelBodyQouationFile").offset().top - 100
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
                            $scope.excelJsonQouation = XLSX.utils.sheet_to_json(workbook.Sheets[y]);
                        }
                        else {
                            $scope.excelJsonQouation = XLS.utils.sheet_to_row_object_array(workbook.Sheets[y]);
                        }
                    }
                });

            }

        }
        else {
            alert("Sorry! Your browser does not support HTML5!");
        }
    }

    $scope.GetQouationFile = function (_IDSupplier) {
        ngProgress.start();
        $scope.Param = {
            IDSupplier: _IDSupplier
        }
        QouationFileService.GetQouationFile($scope.Param).then(function (result) {

            $scope.QouationFiles = result.data;
            $('#TableQouationFile').show();
        }).catch(function () {
            AutoClosingErrorAlert('Error in catch data !', 5000);


        }).finally(function () {
            ngProgress.complete();
            ngProgress.stop();
        });
    }
    $scope.ShowQouationDetail = function (_item) {

        if ($scope.activeRow == _item.IDQouationFile) {
            $scope.activeRow = false;
        }
        else {
            $('#imgLoaderQouation' + _item.IDQouationFile).show();
            $scope.Param = {
                IDQouationFile: _item.IDQouationFile
            }
            QouationDetailService.GetQouationDetail($scope.Param).then(function (result) {
                $scope.QouationDetails = result.data;
            }).catch(function () {
                AutoClosingErrorAlert('Error in catch data !', 5000);

            }).finally(function () {
                $('#imgLoaderQouation' + _item.IDQouationFile).hide();
                $scope.GetSumValuesOfColumns(_item.IDQouationFile);
                $scope.activeRow = _item.IDQouationFile;
            });
        }



    }

    $scope.ClearPdfFileUploadQouation = function () {

        document.getElementById("urlNamePdfQouation").value = "";
        document.getElementById("PdfFileUploadQouation").value = "";
        $scope.SelectedPdfFile = undefined;
    }
    $scope.ChangePdfFileQouation = function (element) {

        var validType = $scope.CheckValidationType(element)
        var validSize = $scope.CheckValidationSize(element)
        if (!validType) {
            AutoClosingErrorAlert('Select the file with pdf extension !', 5000);
            return;
        }
        if (!validSize) {
            AutoClosingErrorAlert('The file size should be less than 1 MB !', 5000);
            return;
        }
        document.getElementById("urlNamePdfQouation").value = element.files[0].name;
        $scope.SelectedPdfFile = element;
    }

    $scope.ClearEmailFileUploadQouation = function () {

        document.getElementById("urlNameEmailQouation").value = "";
        document.getElementById("EmailFileUploadQouation").value = "";
        $scope.SelectedEmailFile = undefined;
        $scope.CurrentQouation.EmailFileQouation = "";
    }
    $scope.ChangeEmailFileUploadQouation = function (element) {

        var validType = $scope.CheckValidationType(element)
        var validSize = $scope.CheckValidationSize(element)
        if (!validType) {
            AutoClosingErrorAlert('Select the file with email extension !', 5000);
            return;
        }
        if (!validSize) {
            AutoClosingErrorAlert('The file size should be less than 1 MB !', 5000);
            return;
        }
        document.getElementById("urlNameEmailQouation").value = element.files[0].name;
        if (document.getElementById("AddQouationFile").value == "Add")
        {
            $scope.SelectedEmailFile = element;
        }
        else if (document.getElementById("AddQouationFile").value == "Update")
        {
            var file = document.querySelector('#EmailFileUploadQouation').files[0];
            var reader = new FileReader();

            reader.addEventListener("load", function () {
                $scope.CurrentQouation.NewFileUrlEmail = reader.result;
            }, false);

            if (file) {
                reader.readAsDataURL(file);
            }
        }
    
    }

    $scope.UpdateRatioQouationFile = function () {
        ngProgress.start();
        $scope.activeRow = false;
        var _IDSupplier = $scope.QouationFiles[0].IDSupplier;
        var Param = {
            RatioRial: $scope.Qouation.RatioRial,
            RatioTransfer: $scope.Qouation.RatioTransfer,
            RatioMarkUp: $scope.Qouation.RatioMarkUp,
            IDSupplier: _IDSupplier
        };
        QouationFileService.UpdateRatioQouationFile(Param).then(function (result) {
            ngProgress.stop();
            $scope.GetQouationFile(_IDSupplier);
            AutoClosingSuccessAlert('Update was successful !', 5000);
        }).catch(function () {
            AutoClosingErrorAlert('Error in Update !', 5000);
        }).finally(function () {
            ngProgress.complete();
            ngProgress.stop();
        });
    }

    $scope.ChangeQouationSalesPriceInput = function (_children) {

        _children.MarkUp = (((_children.SalesPrice) - (_children.FixedPrice)) / (_children.SalesPrice)) * 100;
        $('#SalesPriceInput' + _children.IDQouationDetail).css('border', 'solid 1px Blue');
    }
    $scope.GetSumValuesOfColumns = function (_IDQouationFile) {
        $scope.Param = {
            IDQouationFile: _IDQouationFile
        }
        QouationDetailService.GetSumsColumns($scope.Param).then(function (result) {
            $scope.SumPrice = result.data[0].SumPrice;
            $scope.SumFixedPrice = result.data[0].SumFixedPrice;
            $scope.SumSalesPrice = result.data[0].SumSalesPrice;
            $scope.SumQty = result.data[0].SumQty;

        }).catch(function () {
            AutoClosingErrorAlert('Error in catch data !', 5000);

        }).finally(function () {
        });
    }


    $scope.initialExcelModeQouation = function () {
        $scope.ExcelModeQouation = false;

    }
    $scope.ChangExcelModeQouation = function (ExcelModeQouation) {
        $scope.ExcelModeQouation = !ExcelModeQouation;
        if ($scope.ExcelModeQouation) {
            $('#divExcelFileQouation').show();
        }
        else {
            $('#divExcelFileQouation').hide();

        }
    }
    $scope.ShowAddDetailQouation = function (_item) {
        $('#ModalAddDetailQouation').modal('show');
        $scope.IDSupplier = _item.IDSupplier;
        $scope.activeRow = false;
        $scope.PerForQouationDetail = 1;

        if (_item.RatioChfToEu == undefined)
        {
            $scope.ChfEuQouationDetail = 1;

        }
        else
        {
            $scope.ChfEuQouationDetail = _item.RatioChfToEu;

        }
        $scope.HeadetInModalAddDetailQouation = _item.Title;
        $scope.IDQouationFile = _item.IDQouationFile;
        $scope.Param = {
            IDQouationFile: _item.IDQouationFile
        }
        QouationDetailService.GetQouationDetail($scope.Param).then(function (result) {
            $scope.QouationDetails = result.data;
        }).catch(function () {
            AutoClosingErrorAlert('Error in catch data !', 5000);

        }).finally(function () {
        });
    }


    $scope.AddRowDetailInQouation = function () {
        $scope.QouationDetails.push({
            'PartNumber': "",
            'ShortDescription': "",
            'Description': "",
            'Qty': "",
            'Price': ""
        });
    };
    $scope.DeleteRowDetailInQouation = function () {
        var Accept = confirm("Are you sure ?");
        if (Accept == true) {
            var ItemsSelected = [];
            var ItemsNotSelected = [];
            var countSelected = 0;
            $scope.selectedAllQouationDetailManual = false;
            angular.forEach($scope.QouationDetails, function (item) {
                if (!item.selected) {
                    ItemsNotSelected.push(item);
                }
                else {
                    ItemsSelected.push(item);
                    countSelected++;
                }
            });
            if (countSelected == 0)
            {
                AutoClosingErrorAlert('no item selected!', 5000);
            }
            else
            {
                $scope.QouationDetails = ItemsNotSelected;

                var Param = {
                    ItemsSelected: ItemsSelected
                }
                QouationDetailService.DeleteSelectedQouationDetail(Param).then(function (result) {
                    AutoClosingSuccessAlert('Delete was successful !', 5000);
                }).catch(function () {
                    AutoClosingErrorAlert('Error in delete !', 5000);
                }).finally(function () {
                    ngProgress.complete();
                    ngProgress.stop();
                });
            }

        }
    };
    $scope.checkAllQouationDetailManual = function () {
        if ($scope.selectedAllQouationDetailManual) {
            $scope.selectedAllQouationDetailManual = true;
        } else {
            $scope.selectedAllQouationDetailManual = false;
        }
        angular.forEach($scope.QouationDetails, function (item) {
            item.selected = $scope.selectedAllQouationDetailManual;
        });
    };
    $scope.GetDefineDetailProduct = function () {
        DefineDetailProductService.GetAutoCompleteDefineDetailProduct().then(function (result) {
            $scope.DefineDetailProducts = result.data;
        }).catch(function () {
        }).finally(function () {
        });
    }
    $scope.GetDefineDetailProduct();

    $scope.SelectedItem = function (_item, _rowNumber) {
        $scope.QouationDetails[_rowNumber].ShortDescription = _item.ShortTechnicalDescription;
        $scope.QouationDetails[_rowNumber].Description = _item.TechnicalDescription;
        $scope.QouationDetails[_rowNumber].PartNumber = _item.Title;

    }
    $scope.SaveAllDetailInQouation = function () {
        ngProgress.start();
        var Param = {
            AllDetailJson: JSON.stringify($scope.QouationDetails),
            IDQouationFile: $scope.IDQouationFile,
            IDLogUser: _IDLogUser,
            RatioChfToEu: $scope.ChfEuQouationDetail
        }
        QouationDetailService.AddAllQouationDetail(Param).then(function (result) {
            $('#ModalAddDetailQouation').modal('hide');
            ngProgress.stop();
            $scope.GetQouationFile($scope.IDSupplier);
            AutoClosingSuccessAlert('Save all was successful !', 5000);
        }).catch(function () {
            AutoClosingErrorAlert('Error in save all !', 5000);
        }).finally(function () {
            ngProgress.complete();
            ngProgress.stop();
        });
    }
    $scope.ChangePriceInModalAddDetailQouation = function (_Price,_index)
    {
        var num = (_Price / $scope.PerForQouationDetail) * $scope.ChfEuQouationDetail;
        $scope.QouationDetails[_index].Price = num.toFixed(2);
    }
    $scope.ShowModalSearchQouation = function () {
        $scope.SearchPartNumberQouation = "";
        $scope.FindPartNumberOnQouation = "";
        $('#ModalSearchQouation').modal('show');
    }
    $scope.ShowModalSortQouation = function () {
        $('#ModalSortQouation').modal('show');
    }
    $scope.SearchOnPartNumberQouation = function (_PartNumber) {
     
        $scope.Param = {
            PartNumber: _PartNumber
        }
        QouationDetailService.GetQouationDetailByPartNumber($scope.Param).then(function (result) {
            $scope.FindPartNumberOnQouation = result.data;
        }).catch(function () {
            AutoClosingErrorAlert('Error in Fetch  !', 5000);

        }).finally(function () {
        });

    }

    $scope.SortTableQouation = {
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
                QouationFileService.ChangeSortInQouationFile(myJsonString).then(function (result) {
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