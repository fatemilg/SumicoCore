myApp.filter('PassHsCode', function () {
    return function (HsCodeFirstTime, HsCodeOtherTime) {

        if (HsCodeFirstTime != 0) {
            return HsCodeFirstTime;
        }
        else {
            return HsCodeOtherTime;
        }
    };
});
myApp.filter('PassIDCode', function () {
    return function (IDCodeFirstTime, IDCodeOtherTime) {

        if (IDCodeFirstTime != 0) {
            return IDCodeFirstTime;
        }
        else {
            return IDCodeOtherTime;
        }
    };
});
myApp.filter('PassUnderValue', function () {
    return function (UnderValueFirstTime, UnderValueOtherTime) {
        if (UnderValueFirstTime != 0) {
            return UnderValueFirstTime;
        }
        else {
            if (UnderValueOtherTime == undefined) {
                return 0;
            }
            else {
                return UnderValueOtherTime;

            }
        }
    };
});

myApp.controller('PurchaseOrderController', ['$scope', 'ngProgress', 'PurchaseOrderFileService', 'SupplierService', 'PurchaseOrderService', 'DefineDetailProductService', 'CurrencyService','RelatePurchaseOrderFileService', function ($scope, ngProgress, PurchaseOrderFileService, SupplierService, PurchaseOrderService, DefineDetailProductService, CurrencyService, RelatePurchaseOrderFileService) {

    $scope.CurrentPurchaseOrderFile = {};
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

    };
    $scope.CheckValidationSize = function (element) {

        var FileSize = element.files[0].size;
        if (FileSize > 1024 * 1024) {

            return false;
        }
        else {
            return true;
        }



    };
    $scope.NewPurchaseOrderFile = function () {
        $scope.CurrentPurchaseOrderFile.Title = "";
        $scope.CurrentPurchaseOrderFile.DeliverDate = "";
        $scope.CurrentPurchaseOrderFile.Supplier = $scope.Suppliers[0].value;
        $scope.CurrentPurchaseOrderFile.Currency = $scope.Currencies[0].value;
        $scope.ClearExcelFileUpload();
        document.getElementById("AddPurchaseOrderFile").value = "Add";
        document.getElementById("excelFileUpload").disabled = false;

    };
    $scope.ClearExcelFileUpload = function () {

        document.getElementById("urlNamePurchaseOrderFile").value = "";
        document.getElementById("excelFileUpload").value = "";
        $scope.SelectedFile = undefined;
    };
    $scope.ChangeFile = function (element) {

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
        document.getElementById("urlNamePurchaseOrderFile").value = element.files[0].name;
        $scope.SelectedFile = element;
        $scope.ConvertExcelToJson();

    };

    $scope.AddPurchaseOrderFile = function () {
        document.getElementById("excelFileUpload").disabled = false;
        if (document.getElementById("AddPurchaseOrderFile").value == "Add") {
            var element = $scope.SelectedFile;
            if (element !== undefined) {
                var _IDPurchaseOrderFile = NewGuid();
                var _TitlePurchaseOrderFile = document.getElementById("TitlePurchaseOrderFile").value;
                var _IDSupplier = $scope.CurrentPurchaseOrderFile.Supplier;
                var _IDCurrency = $scope.CurrentPurchaseOrderFile.Currency;
                var _DeliverDate = $scope.CurrentPurchaseOrderFile.DeliverDate;

                $scope.files = [];
                for (var i = 0; i < element.files.length; i++) {
                    $scope.files.push(element.files[i])
                }
                var data = new FormData();

                for (var i in $scope.files) {
                    data.append("excelFileUpload", $scope.files[i]);
                }
                data.append("IDLogUser", _IDLogUser);
                data.append("IDPurchaseOrderFile", _IDPurchaseOrderFile);
                data.append("TitlePurchaseOrderFile", _TitlePurchaseOrderFile);
                data.append("IDSupplier", _IDSupplier);
                data.append("IDCurrency", _IDCurrency);
                data.append("DeliverDate", _DeliverDate);
                data.append("ExcelJson", JSON.stringify($scope.exceljson));

                PurchaseOrderFileService.AddPurchaseOrderFile(data).then(function (result) {
                    $scope.NewPurchaseOrderFile();
                    $scope.GetPurchaseOrderFileAndDetail(_IDSupplier);
                    AutoClosingSuccessAlert('Registration was successful !', 5000);
                }).catch(function () {
                    AutoClosingErrorAlert('Error in registration !', 5000);

                }).finally(function () {
                    ngProgress.complete();
                });
            }
            else {
                AutoClosingErrorAlert('Please select your file !', 5000);

            }
        }
        else if (document.getElementById("AddPurchaseOrderFile").value === "Update") {

            var _IDPurchaseOrderFile = $scope.CurrentPurchaseOrderFile.IDPurchaseOrderFile;
            var _Title = $scope.CurrentPurchaseOrderFile.Title;
            var _IDSupplier = $scope.CurrentPurchaseOrderFile.Supplier;
            var _IDCurrency = $scope.CurrentPurchaseOrderFile.Currency;
            var _DeliverDate = $scope.CurrentPurchaseOrderFile.DeliverDate;
            $scope.Param = {
                IDPurchaseOrderFile: _IDPurchaseOrderFile,
                IDLogUser: _IDLogUser,
                Title: _Title,
                IDSupplier: _IDSupplier,
                IDCurrency: _IDCurrency,
                DeliverDate: _DeliverDate
            };
            PurchaseOrderFileService.UpdatePurchaseOrderFile($scope.Param).then(function (result) {
                $scope.NewPurchaseOrderFile();
                $scope.GetPurchaseOrderFileAndDetail(_IDSupplier);
                AutoClosingSuccessAlert('Update was successful !', 5000);
            }).catch(function () {
                AutoClosingErrorAlert('Error in Update !', 5000);

            }).finally(function () {
                ngProgress.complete();
            });
        }


    };
    $scope.DeletePurchaseOrderFile = function (_IDPurchaseOrderFile, _IDSupplier, _FileUrl) {
        var Accept = confirm("Are you sure ?");
        if (Accept == true) {
            $scope.Param = {
                IDPurchaseOrderFile: _IDPurchaseOrderFile,
                FileUrl: _FileUrl
            };
            PurchaseOrderFileService.DeletePurchaseOrderFile($scope.Param).then(function (result) {

                $scope.NewPurchaseOrderFile();
                $scope.GetPurchaseOrderFileAndDetail(_IDSupplier);
                AutoClosingSuccessAlert('Delete was successful !', 5000);
            }).catch(function () {
                AutoClosingErrorAlert('Error in Delete !', 5000);

            }).finally(function () {
                ngProgress.complete();
            });

        }
    };
    $scope.EditPurchaseOrderFile = function (_item) {
        $scope.CurrentPurchaseOrderFile.IDPurchaseOrderFile = _item.IDPurchaseOrderFile;
        $scope.CurrentPurchaseOrderFile.Supplier = _item.IDSupplier;
        $scope.CurrentPurchaseOrderFile.Currency = _item.IDCurrency;
        $scope.CurrentPurchaseOrderFile.Title = _item.Title;
        $scope.CurrentPurchaseOrderFile.DeliverDate = _item.DeliverDate;

        document.getElementById("AddPurchaseOrderFile").value = "Update";
        document.getElementById("excelFileUpload").disabled = true;
        $('html, body').animate({
            scrollTop: $("#panelBody").offset().top - 100
        }, 1000);


    }
    $scope.CancelPurchaseOrderFile = function (_item) {
        $scope.NewPurchaseOrderFile();
    }

    $scope.GetPurchaseOrderFileAndDetail = function (_IDSupplier) {
        ngProgress.start();
        $scope.Param = {
            IDSupplier: _IDSupplier
        }
        PurchaseOrderFileService.GetPurchaseOrderFileAndDetail($scope.Param).then(function (result) {
            $scope.PurchaseOrderFiles = result.data;
            $('#TablePurchaseOrderFile').show();
        }).catch(function () {
            AutoClosingErrorAlert('Error in catch data !', 5000);


        }).finally(function () {
            ngProgress.complete();
            ngProgress.stop();
        });
    }


    $scope.ConvertExcelToJson = function () {

        var xlsxflag = false; /*Flag for checking whether excel is .xls format or .xlsx format*/
        if ($("#excelFileUpload").val().toLowerCase().indexOf(".xlsx") > 0) {
            xlsxflag = true;
        }
        else if ($("#excelFileUpload").val().toLowerCase().indexOf(".xls") > 0) {

            xlsxflag = false;
        }

        /*Checks whether the browser supports HTML5*/
        if (typeof (FileReader) != "undefined") {
            var reader = new FileReader();

            if (xlsxflag) {/*If excel file is .xlsx extension than creates a Array Buffer from excel*/

                reader.readAsArrayBuffer($("#excelFileUpload")[0].files[0]);
            }
            else {
                reader.readAsBinaryString($("#excelFileUpload")[0].files[0]);
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

    $scope.CheckPartNumberInFile = function (_exceljson) {
        PurchaseOrderFileService.CheckPartNumberInPurchaseOrderFile(_exceljson).then(function (result) {

            if (result.data.length > 0) {

                var str = '';
                for (var item in result.data) {
                    str += result.data[item].PartNumber;
                    if (item < result.data.length - 1) {
                        str = str + ',';
                    }


                }
                $scope.ClearExcelFileUpload();
                AutoClosingErrorAlert('Error : ' + str + ' not define !', 20000);


            }
            else {
                $scope.exceljson = _exceljson;
            }


        }).catch(function () {
            AutoClosingErrorAlert('Error in catch data !', 5000);
        }).finally(function () {

        });
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


    $scope.UpdateShow = function (_ShowSituation, _IDPurchaseOrderFile) {
        $scope.Param = {
            Show: _ShowSituation,
            IDPurchaseOrderFile: _IDPurchaseOrderFile
        }
        PurchaseOrderFileService.UpdateShow($scope.Param).then(function (result) {
        }).catch(function () {
            AutoClosingErrorAlert('Error in Update  !', 5000);

        }).finally(function () {
        });
    }


    $scope.txtChange = function (item) {
        $(item).css({ 'background-color': 'lightgray' })

    }

    $scope.undoOnTable = function (_index, _IDPurchaseOrderFile, _item) {
        if (_item.RelatePurchaseFirstTime != 0) {
            $('#txtHsCode' + _index + _IDPurchaseOrderFile).val(_item.RelatePurchaseFirstTime[0].HsCode);
        }
        else {
            if (_item.RelatePurchaseOtherTime != 0) {
                $('#txtHsCode' + _index + _IDPurchaseOrderFile).val(_item.RelatePurchaseOtherTime[0].HsCode);
            }
            else {
                $('#txtHsCode' + _index + _IDPurchaseOrderFile).val('');
            }
        }

        if (_item.RelatePurchaseFirstTime != 0) {
            $('#txtIDCode' + _index + _IDPurchaseOrderFile).val(_item.RelatePurchaseFirstTime[0].IDCode);
        }
        else {
            if (_item.RelatePurchaseOtherTime != 0) {
                $('#txtIDCode' + _index + _IDPurchaseOrderFile).val(_item.RelatePurchaseOtherTime[0].IDCode);
            }
            else {
                $('#txtIDCode' + _index + _IDPurchaseOrderFile).val('');
            }
        }


        if (_item.RelatePurchaseFirstTime != 0) {
            $('#txtUnderValue' + _index + _IDPurchaseOrderFile).val(_item.RelatePurchaseFirstTime[0].UnderValue);
        }
        else {
            if (_item.RelatePurchaseOtherTime != 0) {
                $('#txtUnderValue' + _index + _IDPurchaseOrderFile).val(_item.RelatePurchaseOtherTime[0].UnderValue);
            }
            else {
                $('#txtUnderValue' + _index + _IDPurchaseOrderFile).val('0');
            }
        }

        $('#txtHsCode' + _index + _IDPurchaseOrderFile).css({ 'background-color': 'white' });

        $('#txtIDCode' + _index + _IDPurchaseOrderFile).css({ 'background-color': 'white' })

        $('#txtUnderValue' + _index + _IDPurchaseOrderFile).css({ 'background-color': 'white' })






    }

    $scope.addOnTable = function (_index, _IDDefineDetailProduct, _IDSupplier, _IDPurchaseOrderFile, _IDPurchaseOrder) {
        var _HSCode, _IDCode
        if ($('#txtHsCode' + _index + _IDPurchaseOrderFile).val() == '') {
            _HSCode = 0;
        }
        else {
            _HSCode = $('#txtHsCode' + _index + _IDPurchaseOrderFile).val();
        }

        if ($('#txtIDCode' + _index + _IDPurchaseOrderFile).val() == '') {
            _IDCode = 0;
        }
        else {
            _IDCode = $('#txtIDCode' + _index + _IDPurchaseOrderFile).val();
        }

        var param = {
            hsCode: _HSCode,
            IDCode: _IDCode,
            UnderValue: $('#txtUnderValue' + _index + _IDPurchaseOrderFile).val(),
            IDDefineDetailProduct: _IDDefineDetailProduct,
            IDPurchaseOrderFile: _IDPurchaseOrderFile,
            IDPurchaseOrder: _IDPurchaseOrder,
            IDLogUser: _IDLogUser

        }
        RelatePurchaseOrderFileService.AddRelatePurchaseOrderFile(param).then(function (result) {
            $scope.GetPurchaseOrderFileAndDetail(_IDSupplier);
            AutoClosingSuccessAlert('Update successes !', 5000);
        }).catch(function () {
            AutoClosingErrorAlert('Error in catch data !', 5000);
        }).finally(function () {
            ngProgress.complete();
        });




    }


    $scope.SortPurchaseOrderFile = function () {
        $('#ModalSortPurchaseOrderFile').modal('show');
    }
    $scope.ShowSearchModal = function () {
        $scope.SearchPartNumber = "";
        $scope.FindPartNumberOnPurchaseOrders = "";
        $('#ModalSearch').modal('show');
    };
    $scope.SortTablePurchaseOrderFile = {
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
                PurchaseOrderFileService.ChangeSortInPurchaseOrderFile(myJsonString).then(function (result) {
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


    $scope.SearchOnPartNumber = function (_PartNumber) {
        
        $scope.Param = {
            PartNumber: _PartNumber
        }
        PurchaseOrderService.GetPurchaseOrderByPartNumber($scope.Param).then(function (result) {
            $scope.FindPartNumberOnPurchaseOrders = result.data;
        }).catch(function () {
            AutoClosingErrorAlert('Error in Fetch  !', 5000);

        }).finally(function () {
        });

    }

    $scope.ShowDetailPurchaseOrder = function (_IDPurchaseOrderFile) {

        if ($scope.activeRow == _IDPurchaseOrderFile) {
            $scope.activeRow = false;
        }
        else {

            $scope.activeRow = _IDPurchaseOrderFile;
        }

    }

    $scope.exportToPdfSection1 = function (divID) {
        var contents = $("#" + divID).html();
        var frame1 = $('<iframe />');
        frame1[0].name = "frame1";
        frame1.css({ "position": "absolute", "top": "-1000000px" });
        $("body").append(frame1);
        var frameDoc = frame1[0].contentWindow ? frame1[0].contentWindow : frame1[0].contentDocument.document ? frame1[0].contentDocument.document : frame1[0].contentDocument;
        frameDoc.document.open();
        frameDoc.document.write('<html><head><title> </title>');
        frameDoc.document.write('<link href=\"/Portal/Local/css/bootstrap.min.css\" rel=\"stylesheet\" />');
        frameDoc.document.write('<link href=\"/Portal/Local/css/style.css\" rel=\"stylesheet\" />');
        frameDoc.document.write('<link href=\"/Portal/Local/css/MediaPrintForPurchase-Section1.css\" rel=\"stylesheet\" />');
        frameDoc.document.write('</head><body>');
        frameDoc.document.write(contents);
        frameDoc.document.write('</body></html>');
        frameDoc.document.close();
        setTimeout(function () {
            window.frames["frame1"].focus();
            window.frames["frame1"].print();
            frame1.remove();
        }, 1000);
    }
    $scope.exportToPdfSection2 = function (divID) {
        var contents = $("#" + divID).html();
        var frame1 = $('<iframe />');
        frame1[0].name = "frame1";
        frame1.css({ "position": "absolute", "top": "-1000000px" });
        $("body").append(frame1);
        var frameDoc = frame1[0].contentWindow ? frame1[0].contentWindow : frame1[0].contentDocument.document ? frame1[0].contentDocument.document : frame1[0].contentDocument;
        frameDoc.document.open();
        frameDoc.document.write('<html><head><title> </title>');
        frameDoc.document.write('<link href=\"/Portal/Local/css/bootstrap.min.css\" rel=\"stylesheet\" />');
        frameDoc.document.write('<link href=\"/Portal/Local/css/style.css\" rel=\"stylesheet\" />');
        frameDoc.document.write('<link href=\"/Portal/Local/css/MediaPrintForPurchase-Section2.css\" rel=\"stylesheet\" />');
        frameDoc.document.write('</head><body>');
        frameDoc.document.write(contents);
        frameDoc.document.write('</body></html>');
        frameDoc.document.close();
        setTimeout(function () {
            window.frames["frame1"].focus();
            window.frames["frame1"].print();
            frame1.remove();
        }, 1000);
    }
}]);




