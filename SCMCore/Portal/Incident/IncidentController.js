myApp.controller('IncidentController', ['$window', '$scope', '$rootScope', 'ngProgress', 'IncidentCategoryService', 'IncidentService', 'GalleryService', 'GalleryCategoryService',
    function ($window, $scope, $rootScope, ngProgress, IncidentCategoryService, IncidentService, GalleryService, GalleryCategoryService) {
        $scope.CurrentIncidentCategory = {};
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
        $scope.ReadFiles = function (FileUplod) {
            $scope.FileData = {};
            $scope.ReadFile(0, FileUplod.files);

        }
        $scope.ReadFile = function (index, files) {
            var reader = new FileReader();
            if (index >= files.length) return;
            var file = files[index];
            reader.addEventListener("load", function () {
                $scope.FileData[index] = reader.result;
                $scope.ReadFile(index + 1, files);
                if (index == files.length - 1) {
                    $scope.SaveFiles(files);
                }

            }, false);

            if (file) {
                reader.readAsDataURL(file);
            }
        }
        //-----------------------------------------------------------------

        $scope.NewIncidentCategory = function (_ParentIncident) {

            $('#DivInfoIncidentCategory').slideToggle();
            $('#btnNewIncidentCategory').fadeOut();
            $('#btnSaveIncidentCategory').val('Add');
            $('html, body').animate({
                scrollTop: $("#DivInfoIncidentCategory").offset().top
            }, 1000);
            $scope.CurrentIncidentCategory = {};
            if (_ParentIncident) {
                $scope.CurrentIncidentCategory.IDParent = _ParentIncident.IDIncidentCategory
            }
        };
        $scope.CancelIncidentCategory = function () {
            $('#DivInfoIncidentCategory').slideToggle();
            $('#btnNewIncidentCategory').fadeIn();
        };
        $scope.GetIncidentCategory = function () {
            IncidentCategoryService.GetDataWithIncidents().then(function (result) {
                $scope.IncidentCategorys = result.data;
            }).catch(function () {
            }).finally(function () {
            });
        };
        $scope.SaveIncidentCategory = function () {
            if ($('#btnSaveIncidentCategory').val() == 'Add') {
                var NewIncidentCategory = {
                    Name_Fa: $scope.CurrentIncidentCategory.Name_Fa,
                    IDParent: $scope.CurrentIncidentCategory.IDParent
                };
                IncidentCategoryService.AddIncidentCategory(NewIncidentCategory).then(function (result) {
                    $scope.CurrentIncidentCategory = {}
                    $scope.GetIncidentCategory();
                    AutoClosingSuccessAlert('Registration has been succeeded!', 3000);
                }).catch(function () {
                    AutoClosingErrorAlert('Registration has been failed!', 3000);
                }).finally(function () {
                    $scope.CancelIncidentCategory();
                });
            }
            else if ($('#btnSaveIncidentCategory').val() == 'Update') {
                $scope.UpdateIncidentCategory = {
                    IDIncidentCategory: $scope.CurrentIncidentCategory.IDIncidentCategory,
                    Name_Fa: $scope.CurrentIncidentCategory.Name_Fa,
                    IDParent: $scope.CurrentIncidentCategory.IDParent
                };
                IncidentCategoryService.UpdateIncidentCategory($scope.UpdateIncidentCategory).then(function (result) {
                    $scope.GetIncidentCategory();
                    AutoClosingSuccessAlert('Update has been succeeded!', 3000);
                }).catch(function () {
                    AutoClosingErrorAlert('Update has been failed!', 3000);
                }).finally(function () {
                    $scope.CancelIncidentCategory();
                });
            }
        };
        $scope.DeleteIncidentCategory = function (_IDIncidentCategory) {
            $scope.DelIncidentCategoryInfo = {
                IDIncidentCategory: _IDIncidentCategory
            };
            var ConfirmDelete = $window.confirm('Are you sure?');
            if (ConfirmDelete) {
                IncidentCategoryService.DeleteIncidentCategory($scope.DelIncidentCategoryInfo).then(function (result) {
                    $scope.GetIncidentCategory();
                    AutoClosingSuccessAlert('Delete has been succeeded!', 3000);
                }).catch(function () {
                    AutoClosingErrorAlert('Delete has been failed!', 3000);
                }).finally(function () {
                });
            }
        };
        $scope.EditIncidentCategory = function (_item) {
            $('#DivInfoIncidentCategory').slideDown();
            $('#btnNewIncidentCategory').fadeOut();
            $('#btnSaveIncidentCategory').val('Update');
            $scope.CurrentIncidentCategory.IDIncidentCategory = _item.IDIncidentCategory,
                $scope.CurrentIncidentCategory.IDX = _item.IDX,
                $scope.CurrentIncidentCategory.Name_Fa = _item.Name_Fa,
                $scope.CurrentIncidentCategory.IDParent = _item.IDParent,
                $('html, body').animate({
                    scrollTop: $("#DivInfoIncidentCategory").offset().top
                }, 1000);
        };
        $scope.MouseEnter = function (_item) {
            _item.ShowNavigation = true;
        };
        $scope.MouseLeave = function (_item) {
            _item.ShowNavigation = false;
        };
        $scope.ToggleIncidentCategory = function (_item) {
            if (!_item.IDIncident)
                if (_item.Open == true) {
                    _item.Open = false
                } else {
                    _item.Open = true
                };
        };
        $scope.OpenModalIncident = function (_item, mode) {
            $('#ModalIncident').modal('show');
            $scope.CurrentIncident = {};
            $scope.CurrentIncidentCategory.IDIncidentCategory = _item.IDIncidentCategory;
            $('#btnSaveIncident').val(mode);
            if (mode === 'Update') {
                $scope.EditIncident(_item);
            }
        };
        //----------------------------------------------------------------

        $scope.NewIncident = function () {
            $('#btnNewIncident').fadeOut();
            $('#btnSaveIncident').val('Add');
            $scope.CurrentIncident = {};
        };
        $scope.CancelIncident = function () {
            $scope.NewIncident();
            $('#ModalIncident').modal('hide');
        };
        $scope.SaveIncident = function () {
            if ($('#btnSaveIncident').val() == 'Add') {
                var NewIncident = {
                    IDIncidentCategory: $scope.CurrentIncidentCategory.IDIncidentCategory,
                    Name_Fa: $scope.CurrentIncident.Name_Fa,
                    PicFile: $scope.CurrentIncident.PicFile,
                    Description_Fa: $scope.CurrentIncident.Description_Fa,
                    StartDate: jing($scope.CurrentIncident.StartDate, true),
                    EndDate: jing($scope.CurrentIncident.EndDate, true)
                };
                IncidentService.AddIncident(NewIncident).then(function (result) {
                    $scope.CurrentIncident = {}
                    AutoClosingSuccessAlert('Registration has been succeeded!', 3000);
                    $scope.GetIncidentCategory();
                }).catch(function () {
                    AutoClosingErrorAlert('Registration has been failed!', 3000);
                }).finally(function () {
                    $scope.CancelIncident();
                });
            }
            else if ($('#btnSaveIncident').val() == 'Update') {
                var UpdateIncident = {
                    IDIncident: $scope.CurrentIncident.IDIncident,
                    IDIncidentCategory: $scope.CurrentIncident.IDIncidentCategory,
                    Name_Fa: $scope.CurrentIncident.Name_Fa,
                    PicFile: $scope.CurrentIncident.PicFile == undefined ? '{}' : $scope.CurrentIncident.PicFile,
                    Description_Fa: $scope.CurrentIncident.Description_Fa,
                    StartDate: jing($scope.CurrentIncident.StartDate, true),
                    EndDate: jing($scope.CurrentIncident.EndDate, true)
                };
                IncidentService.UpdateIncident(UpdateIncident).then(function (result) {
                    AutoClosingSuccessAlert('Update has been succeeded!', 3000);
                    $scope.GetIncidentCategory();
                }).catch(function () {
                    AutoClosingErrorAlert('Update has been failed!', 3000);
                }).finally(function () {
                    $scope.CancelIncident();
                });
            }
        };
        $scope.EditIncident = function (_item) {
            $('#btnSaveIncident').val('Update');
            $scope.CurrentIncident.IDIncident = _item.IDIncident;
            $scope.CurrentIncident.IDIncidentCategory = _item.IDIncidentCategory;
            $scope.CurrentIncident.Name_Fa = _item.Name_Fa;
            $scope.CurrentIncident.PicFile = $scope.CurrentIncident.PicFile == undefined ? '{}' : $scope.CurrentIncident.PicFile;
            $scope.CurrentIncident.Description_Fa = _item.Description_Fa;
            $scope.CurrentIncident.StartDate = ginj(_item.StartDate.split("T")[0].replaceAll ('-', '/'), true);
            $scope.CurrentIncident.EndDate = ginj(_item.EndDate.split("T")[0].replaceAll ('-', '/'), true);
        };
        $scope.DeleteIncident = function (_IDIncident) {
            var DelIncidentInfo = {
                IDIncident: _IDIncident
            };
            var ConfirmDelete = $window.confirm('Are you sure?');
            if (ConfirmDelete) {
                IncidentService.DeleteIncident(DelIncidentInfo).then(function (result) {
                    $scope.GetIncidentCategory();
                    AutoClosingSuccessAlert('Delete has been succeeded!', 3000);
                }).catch(function () {
                    AutoClosingErrorAlert('Delete has been failed!', 3000);
                }).finally(function () {
                });
            }
        };
        $scope.ChangeIncidentFile = function (element) {
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
            document.getElementById("urlNameIncidentPic").value = element.files[0].name;

            var preview = document.querySelector('#PreviewIncidentImage');
            var file = document.querySelector('#IncidentPicFileUpload').files[0];
            var reader = new FileReader();

            reader.addEventListener("load", function () {
                preview.src = reader.result;
                $scope.CurrentIncident.PicFile = reader.result;
            }, false);

            if (file) {
                reader.readAsDataURL(file);
            }
        };
        $scope.ClearIncidentPicFileUpload = function () {
            document.getElementById("urlNameIncidentPic").value = "";
            $('#IncidentPicFileUpload').val('');
            $('#PreviewIncidentImage').attr({ 'src': '' });
            $scope.ContentPicUrl = '';
        };

        //-------------------Modal Gallery-----------------------------
        $scope.SaveFiles = function (files) {
            ngProgress.start();
            $scope.ObjGallery = {};
            for (var i = 0; i < files.length; i++) {
                $scope.ObjGallery[i] = {
                    IDRet: $scope.SelectedIDRet,
                    IDGalleryCategory: $scope.CurrentGalleryCategory.IDGalleryCategory,
                    IDGallery: NewGuid(),
                    Name_Fa: files[i].name,
                    FileType: files[i].name.split('.').pop().toLowerCase(),
                    FileSize: files[i].size,
                    Status: 1
                };
            }
            var Param = {};
            Param.ObjGallery = $scope.ObjGallery;
            Param.ObjFiles = $scope.FileData;
            GalleryService.AddFileToGallery(Param).then(function (result) {
                AutoClosingSuccessAlert('آپلود تصاویر با موفقیت انجام شد!', 3000);
                $scope.ImageGallery();
            }).catch(function () {
            }).finally(function () {
                ngProgress.stop();
                ngProgress.complete();
            });
        }
        $scope.ImageGallery = function () {
            if ($scope.CurrentGalleryCategory.IDGalleryCategory != '') {
                $('#GalleryFileUpload').fadeIn();
                $('.divNewGalleryCategory').hide();
                $('.divEditGalleryCategoryButton').css({ 'display': 'inline-block' });
            }
            else {
                $('#GalleryFileUpload').hide();
                $('.divEditGalleryCategoryButton').fadeOut();
            }
            $scope.FillGalleryByIDGalleryCategory($scope.CurrentGalleryCategory.IDGalleryCategory);
        }
        $scope.FillGalleryByIDGalleryCategory = function (_IDGalleryCategory) {
            Param = {
                IDGalleryCategory: _IDGalleryCategory,
                IDRet: $scope.SelectedIDRet
            };
            GalleryService.FillGalleryByIDGalleryCategory(Param).then(function (result) {
                $scope.GalleryImages = result.data;
            }).catch(function () {
            }).finally(function () {
            });
        }
        $scope.CurrentGalleryCategory = {};
        $scope.GetGalleryCategory = function (_IDRet) {
            Param = {
                IDRet: _IDRet
            }
            GalleryCategoryService.GetGalleryCategoryByIDRet(Param).then(function (result) {
                $scope.GalleryCategorys = result.data;
            }).catch(function () {
            }).finally(function () {
            });
        };
        $scope.OpenGalleryModal = function (_IDRet, Name_Fa) {
            $scope.CurrentGalleryCategory.IDGalleryCategory = '';
            $scope.GalleryImages = undefined;
            $scope.GalleryCategorys = undefined;
            $scope.SelectedIDRet = _IDRet;
            $('#ModalImageGallery').modal('show');
            $scope.ImageGalleryModalTitle = 'گالری تصاویر ' + Name_Fa;
            $scope.GetGalleryCategory(_IDRet);
            $('#GalleryFileUpload').hide();
        }
        $scope.DeleteGallery = function (_IDGallery) {
            ngProgress.start();
            $scope.ObjGallery = {
                IDGallery: _IDGallery
            };
            var ConfirmDeleteTrainingCourse = $window.confirm('آیا مطئن هستید؟');
            if (ConfirmDeleteTrainingCourse) {
                GalleryService.DeleteGallery($scope.ObjGallery).then(function (result) {
                    AutoClosingSuccessAlert('حذف اطلاعات با موفقیت انجام شد!', 3000);
                    $scope.ImageGallery();
                }).catch(function () {
                }).finally(function () {
                    ngProgress.complete();
                });
            }
        }
        //------------------------------------------------------
        $scope.GetIncidentCategory();
    }]);




