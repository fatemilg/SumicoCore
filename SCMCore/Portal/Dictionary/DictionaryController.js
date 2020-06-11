myApp.controller('DictionaryController', ['$scope', 'ngProgress', 'DictionaryService', '$compile', '$window', function ($scope, ngProgress, DictionaryService, $compile, $window) {

    $scope.CurrentDictionary = {};
    $scope.NewDictionary = function () {
        $('#DivInfoDictionary').slideToggle();
        $('#btnNewDictionary').fadeOut();
        $('#btnSaveDictionary').val('Add');
        $('html, body').animate({
            scrollTop: $("#DivInfoDictionary").offset().top
        }, 1000);
        $scope.CurrentDictionary = {
        };
    }
    $scope.CancelDictionary = function () {
        $('#DivInfoDictionary').slideToggle();
        $('#btnNewDictionary').fadeIn();
    }
    $scope.GetDictionary = function () {
        DictionaryService.GetDictionary().then(function (result) {
            $scope.Dictionarys = result.data;
        }).catch(function () {
        }).finally(function () {
        });
    };
    $scope.SaveDictionary = function () {
        if ($('#btnSaveDictionary').val() == 'Add') {
            $scope.ObjNewDictionary = {
                IDDictionary: NewGuid(),
                Title: $scope.CurrentDictionary.Title,
                Value: $scope.CurrentDictionary.Value,
                Abstract: $scope.CurrentDictionary.Abstract,
                PicUrl: $scope.CurrentDictionary.PicUrl,
                Status: 1,
                KeyWord: $scope.CurrentDictionary.KeyWord,
                SourceText: $scope.CurrentDictionary.SourceText,
                MetaTag: $scope.CurrentDictionary.MetaTag,
                MetaDescription: $scope.CurrentDictionary.MetaDescription,
            };
            DictionaryService.AddDictionary($scope.ObjNewDictionary).then(function (result) {
                $scope.CurrentDictionary = {}
                $scope.GetDictionary();
                $scope.ClearPicFileUpload();
                AutoClosingSuccessAlert('Registration has been succeeded!', 3000);
            }).catch(function () {
                AutoClosingErrorAlert('Registration has been failed!', 3000);
            }).finally(function () {
                $scope.CancelDictionary();
            });
        }
        else if ($('#btnSaveDictionary').val() == 'Update') {
            $scope.UpdateDictionary = {
                IDDictionary: $scope.CurrentDictionary.IDDictionary,
                Title: $scope.CurrentDictionary.Title,
                Value: $scope.CurrentDictionary.Value,
                Abstract: $scope.CurrentDictionary.Abstract,
                PicUrl: $scope.CurrentDictionary.PicUrl,
                Status: $scope.CurrentDictionary.Status,
                KeyWord: $scope.CurrentDictionary.KeyWord,
                SourceText: $scope.CurrentDictionary.SourceText,
                MetaTag: $scope.CurrentDictionary.MetaTag,
                MetaDescription: $scope.CurrentDictionary.MetaDescription,
            };
            DictionaryService.UpdateDictionary($scope.UpdateDictionary).then(function (result) {
                $scope.GetDictionary();
                $scope.ClearPicFileUpload();
                AutoClosingSuccessAlert('Update has been succeeded!', 3000);
            }).catch(function () {
                AutoClosingErrorAlert('Update has been failed!', 3000);
            }).finally(function () {
                $scope.CancelDictionary();
            });
        }
    };
    $scope.DeleteDictionary = function (_IDDictionary) {
        $scope.DelDictionaryInfo = {
            IDDictionary: _IDDictionary
        };
        var ConfirmDelete = $window.confirm('Are you sure?');
        if (ConfirmDelete) {
            DictionaryService.DeleteDictionary($scope.DelDictionaryInfo).then(function (result) {
                $scope.GetDictionary();
                AutoClosingSuccessAlert('Delete has been succeeded!', 3000);
            }).catch(function () {
                AutoClosingErrorAlert('Delete has been failed!', 3000);
            }).finally(function () {
            });
        }
    };
    $scope.EditDictionary = function (_item) {
        $('#DivInfoDictionary').slideDown();
        $('#btnNewDictionary').fadeOut();
        $('#btnSaveDictionary').val('Update');
        $scope.CurrentDictionary = _item;

        $('html, body').animate({
            scrollTop: $("#DivInfoDictionary").offset().top
        }, 1000);
    }
    $scope.ChangeFile = function (element) {
        var validType = $scope.CheckValidationType(element)
        var validSize = $scope.CheckValidationSize(element)
        if (!validType) {
            AutoClosingErrorAlert('Select the file with Image extension(jpg , png, gif ) !', 5000);
            return;
        }
        if (!validSize) {
            AutoClosingErrorAlert('The file size should be less than 1 MB !', 5000);
            return;
        }
        document.getElementById("urlNameDictionaryPic").value = element.files[0].name;
        var preview = document.querySelector('#PreviewDictionaryImage');
        var file = document.querySelector('#PicFileUpload').files[0];
        var reader = new FileReader();
        reader.addEventListener("load", function () {
            preview.src = reader.result;
            $scope.CurrentDictionary.PicUrl = reader.result;
        }, false);

        if (file) {
            reader.readAsDataURL(file);
        }
    }
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
    $scope.ClearPicFileUpload = function () {

        document.getElementById("urlNameDictionaryPic").value = "";
        $('#PicFileUpload').val('');
        $('#PreviewDictionaryImage').attr({ 'src': '' });
        $scope.CurrentDictionary.PicUrl = '';
    }
    $scope.ToggleActivation = function () {
        ngProgress.start();
        Param = {
            IDDictionary: $scope.CurrentDictionary.IDDictionary
        };
        DictionaryService.ToggleActivation(Param).then(function (result) {
            $scope.CurrentDictionary.Active = !$scope.CurrentDictionary.Active;

        }).catch(function () {
        }).finally(function () {
            ngProgress.stop();
            ngProgress.complete();
        });
    };
    $scope.GetDictionary();
}]);




