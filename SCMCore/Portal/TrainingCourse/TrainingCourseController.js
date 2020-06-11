myApp.controller('TrainingCourseController', ['$scope', '$compile', '$window', 'ngProgress', 'TrainingCourseCategoryService', 'TrainingCourseService', 'PersonelInCompanyService',
    'TrainingCourseUserService', 'TrainingCourseBatchService', 'TrainingCourseBatchTrainingCourseService',
    'WorkTypeService', 'GalleryService', 'GalleryCategoryService', 'ContentModuleRetFactory',
    function ($scope, $compile, $window, ngProgress, TrainingCourseCategoryService, TrainingCourseService, PersonelInCompanyService,
        TrainingCourseUserService, TrainingCourseBatchService, TrainingCourseBatchTrainingCourseService, WorkTypeService, GalleryService,
        GalleryCategoryService, ContentModuleRetFactory) {
        $scope.CurrentTrainingCourse = {};
        $scope.CurrentTrainingCourseUser = {};
        $scope.CurrentTrainingCourseBatch = {};
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

        $scope.InitiateContentModuleList = function () {
            ContentModuleRetFactory.InitiateContentModuleList();
        };
        //-------------------Training Course ----------------------------
        $scope.AddNewTrainingCourse = function () {
            $('#DivInfoTrainingCourse').slideToggle();
            $('#btnNewTrainingCourse').fadeOut();
            $('#btnSaveTrainingCourse').val('Add');
            $('html, body').animate({
                scrollTop: $("#DivInfoTrainingCourse").offset().top
            }, 1000);
            $scope.CurrentTrainingCourse = {
            };
        }
        $scope.CancelTrainingCourse = function () {
            $('#DivInfoTrainingCourse').slideToggle();
            $('#btnNewTrainingCourse').fadeIn();
        }
        $scope.GetTrainingCourse = function () {
            TrainingCourseService.GetTrainingCourse().then(function (result) {
                $scope.TrainingCourses = result.data;
            }).catch(function () {
            }).finally(function () {
            });
        };
        $scope.SaveTrainingCourse = function () {
            if ($('#btnSaveTrainingCourse').val() == 'Add') {
                $scope.NewTrainingCourse = {
                    IDTrainingCourse: NewGuid(),
                    IDTrainingCourseCategory: $scope.CurrentTrainingCourse.IDTrainingCourseCategory,
                    Name_Fa: $scope.CurrentTrainingCourse.Name_Fa,
                    EndDate: jing($scope.CurrentTrainingCourse.EndDate, true),
                    PicFile: $scope.CurrentTrainingCourse.PicFile,
                    Description: $scope.CurrentTrainingCourse.Description,
                    Status: 1
                };
                TrainingCourseService.AddTrainingCourse($scope.NewTrainingCourse).then(function (result) {
                    $scope.CurrentTrainingCourse = {}
                    $scope.GetTrainingCourse();
                    AutoClosingSuccessAlert('Registration has been succeeded!', 3000);
                }).catch(function () {
                    AutoClosingErrorAlert('Registration has been failed!', 3000);
                }).finally(function () {
                    $scope.CancelTrainingCourse();
                });
            }
            else if ($('#btnSaveTrainingCourse').val() == 'Update') {
                $scope.UpdateTrainingCourse = {
                    IDTrainingCourse: $scope.CurrentTrainingCourse.IDTrainingCourse,
                    IDTrainingCourseCategory: $scope.CurrentTrainingCourse.IDTrainingCourseCategory,
                    Name_Fa: $scope.CurrentTrainingCourse.Name_Fa,
                    EndDate: jing($scope.CurrentTrainingCourse.EndDate, true),
                    Description: $scope.CurrentTrainingCourse.Description,
                    PicFile: $scope.CurrentTrainingCourse.PicFile
                };
                TrainingCourseService.UpdateTrainingCourse($scope.UpdateTrainingCourse).then(function (result) {
                    $scope.GetTrainingCourse();
                    AutoClosingSuccessAlert('Update has been succeeded!', 3000);
                }).catch(function () {
                    AutoClosingErrorAlert('Update has been failed!', 3000);
                }).finally(function () {
                    $scope.CancelTrainingCourse();
                });
            }
        };
        $scope.DeleteTrainingCourse = function (_IDTrainingCourse) {
            $scope.DelTrainingCourseInfo = {
                IDTrainingCourse: _IDTrainingCourse
            };
            var ConfirmDelete = $window.confirm('Are you sure?');
            if (ConfirmDelete) {
                TrainingCourseService.DeleteTrainingCourse($scope.DelTrainingCourseInfo).then(function (result) {
                    $scope.GetTrainingCourse();
                    AutoClosingSuccessAlert('Delete has been succeeded!', 3000);
                }).catch(function () {
                    AutoClosingErrorAlert('Delete has been failed!', 3000);
                }).finally(function () {
                });
            }
        };
        $scope.EditTrainingCourse = function (_item) {
            $('#DivInfoTrainingCourse').slideDown();
            $('#btnNewTrainingCourse').fadeOut();
            $('#btnSaveTrainingCourse').val('Update');
            $scope.CurrentTrainingCourse.IDTrainingCourse = _item.IDTrainingCourse;
            $scope.CurrentTrainingCourse.IDTrainingCourseCategory = _item.IDTrainingCourseCategory;
            $scope.CurrentTrainingCourse.Name_Fa = _item.Name_Fa;
            $scope.CurrentTrainingCourse.Description = _item.Description;
            $scope.CurrentTrainingCourse.EndDate = ginj(_item.EndDate.split("T")[0].replace('-', '/').replace('-', '/'), true);
            document.querySelector('#PreviewTrainingCourseImage').src = '/' + _item.PicUrl;
            $scope.CurrentTrainingCourse.PicFile = {};

        }
        $scope.GetTrainingCourseCategory = function () {
            TrainingCourseCategoryService.GetNestedTrainingCourseCategory().then(function (result) {
                $scope.TrainingCourseCategories = result.data;
            }).catch(function () {
            }).finally(function () {
            });
        };
        $scope.AddTrainingCourseCategory = function () {
            $('#ModalTrainingCourseCategory').modal('show');
        };
        $scope.AddTrainedUser = function (_IDTrainingCourse) {
            $scope.SelectedIDTrainingCourse = _IDTrainingCourse;
            $('#ModalTrainingCourseUser').modal('show');
            $scope.GetPersonelInCompany();
            $scope.GetTrainingCourseUser();
            $scope.GetWorkType();
        }
        $scope.setSortColumn = function (propertyName) {
            if ($scope.orderProperty === propertyName) {
                $scope.orderProperty = '-' + propertyName;
            } else if ($scope.orderProperty === '-' + propertyName) {
                $scope.orderProperty = propertyName;
            } else {
                $scope.orderProperty = propertyName;
            }
        }
        $scope.ChangeTrainingCourseFile = function (element) {
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
            document.getElementById("urlNameTrainingCoursePic").value = element.files[0].name;

            var preview = document.querySelector('#PreviewTrainingCourseImage');
            var file = document.querySelector('#TrainingCoursePicFileUpload').files[0];
            var reader = new FileReader();

            reader.addEventListener("load", function () {
                preview.src = reader.result;
                $scope.CurrentTrainingCourse.PicFile = reader.result;
            }, false);

            if (file) {
                reader.readAsDataURL(file);
            }
        }
        $scope.ClearTrainingCoursePicFileUpload = function () {
            document.getElementById("urlNameTrainingCoursePic").value = "";
            $('#TrainingCoursePicFileUpload').val('');
            $('#PreviewTrainingCourseImage').attr({ 'src': '' });
        }
        //---------------------------------------------------------------------------------------


        //-------------------Modal Training Course Category--------------------------------
        $scope.CurrentTrainingCourseCategory = {};
        $scope.AddNewTrainingCourseCategory = function (_IDTrainingCourseCategory) {
            $('#DivInfoTrainingCourseCategory').slideDown();
            $('#btnNewTrainingCourseCategory').fadeOut();
            $('#btnSaveTrainingCourseCategory').val('Add');
            $('html, body').animate({
                scrollTop: $("#DivInfoTrainingCourseCategory").offset().top
            }, 1000);
            $scope.CurrentTrainingCourseCategory = {};
            $scope.CurrentTrainingCourseCategory.IDParent = _IDTrainingCourseCategory;
        }
        $scope.CancelTrainingCourseCategory = function () {
            $('#DivInfoTrainingCourseCategory').slideToggle();
            $('#btnNewTrainingCourseCategory').fadeIn();
        }
        $scope.SaveTrainingCourseCategory = function () {
            if ($('#btnSaveTrainingCourseCategory').val() == 'Add') {
                $scope.NewTrainingCourseCategory = {
                    IDTrainingCourseCategory: NewGuid(),
                    IDParent: $scope.CurrentTrainingCourseCategory.IDParent,
                    Name_Fa: $scope.CurrentTrainingCourseCategory.Name_Fa,
                    ShortDescription: $scope.CurrentTrainingCourseCategory.ShortDescription,
                    Caption: $scope.CurrentTrainingCourseCategory.Caption,
                    Description: $scope.CurrentTrainingCourseCategory.Description,
                    PicFile: $scope.CurrentTrainingCourseCategory.PicFile,
                    AttachPDFFile: $scope.CurrentTrainingCourseCategory.AttachPDFFile
                };
                TrainingCourseCategoryService.AddTrainingCourseCategory($scope.NewTrainingCourseCategory).then(function (result) {
                    $scope.CurrentTrainingCourseCategory = {}
                    $scope.GetTrainingCourseCategory();
                    AutoClosingSuccessAlert('Registration has been succeeded!', 3000);
                }).catch(function () {
                    AutoClosingErrorAlert('Registration has been failed!', 3000);
                }).finally(function () {
                    $scope.CancelTrainingCourseCategory();
                });
            }
            else if ($('#btnSaveTrainingCourseCategory').val() == 'Update') {
                $scope.UpdateTrainingCourseCategory = {
                    IDTrainingCourseCategory: $scope.CurrentTrainingCourseCategory.IDTrainingCourseCategory,
                    IDParent: $scope.CurrentTrainingCourseCategory.IDParent,
                    Name_Fa: $scope.CurrentTrainingCourseCategory.Name_Fa,
                    PicFile: $scope.CurrentTrainingCourseCategory.PicFile,
                    ShortDescription: $scope.CurrentTrainingCourseCategory.ShortDescription,
                    Caption: $scope.CurrentTrainingCourseCategory.Caption,
                    Description: $scope.CurrentTrainingCourseCategory.Description,
                    AttachPDFFile: $scope.CurrentTrainingCourseCategory.AttachPDFFile
                };
                TrainingCourseCategoryService.UpdateTrainingCourseCategory($scope.UpdateTrainingCourseCategory).then(function (result) {
                    $scope.GetTrainingCourseCategory();
                    AutoClosingSuccessAlert('Update has been succeeded!', 3000);
                }).catch(function () {
                    AutoClosingErrorAlert('Update has been failed!', 3000);
                }).finally(function () {
                    $scope.CancelTrainingCourseCategory();
                });
            }
        };
        $scope.DeleteTrainingCourseCategory = function (_IDTrainingCourseCategory, Child) {
            if (Child == undefined) {
                $scope.DelTrainingCourseCategoryInfo = {
                    IDTrainingCourseCategory: _IDTrainingCourseCategory
                };
                var ConfirmDelete = $window.confirm('Are you sure?');
                if (ConfirmDelete) {
                    TrainingCourseCategoryService.DeleteTrainingCourseCategory($scope.DelTrainingCourseCategoryInfo).then(function (result) {
                        $scope.GetTrainingCourseCategory();
                        AutoClosingSuccessAlert('Delete has been succeeded!', 3000);
                    }).catch(function () {
                        AutoClosingErrorAlert('Delete has been failed!', 3000);
                    }).finally(function () {
                    });
                }
            }
            else {
                AutoClosingErrorAlert('This item has sub category!', 3000);
            }
        };
        $scope.EditTrainingCourseCategory = function (_item) {
            $('#DivInfoTrainingCourseCategory').slideDown();
            $('#btnNewTrainingCourseCategory').fadeOut();
            $('#btnSaveTrainingCourseCategory').val('Update');
            $scope.CurrentTrainingCourseCategory.IDTrainingCourseCategory = _item.IDTrainingCourseCategory;
            $scope.CurrentTrainingCourseCategory.IDParent = _item.IDParent;
            $scope.CurrentTrainingCourseCategory.Name_Fa = _item.Name_Fa;
            $scope.CurrentTrainingCourseCategory.ShortDescription = _item.ShortDescription;
            $scope.CurrentTrainingCourseCategory.Caption = _item.Caption;
            $scope.CurrentTrainingCourseCategory.Description = _item.Description;
            $scope.CurrentTrainingCourseCategory.AttachPDFUrl = _item.AttachPDFUrl;
            document.querySelector('#PreviewTrainingCourseCategoryImage').src = '/' + _item.PicUrl;
            $scope.CurrentTrainingCourseCategory.PicFile = {};
            $scope.CurrentTrainingCourseCategory.AttachPDFFile = {};
        }
        $scope.ChangeTrainingCourseCategoryFile = function (element) {

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
            document.getElementById("urlNameTrainingCourseCategoryPic").value = element.files[0].name;



            var preview = document.querySelector('#PreviewTrainingCourseCategoryImage');
            var file = document.querySelector('#TrainingCourseCategoryPicFileUpload').files[0];
            var reader = new FileReader();

            reader.addEventListener("load", function () {
                preview.src = reader.result;
                $scope.CurrentTrainingCourseCategory.PicFile = reader.result;
            }, false);

            if (file) {
                reader.readAsDataURL(file);
            }
        }
        $scope.ClearTrainingCourseCategoryPicFileUpload = function () {
            document.getElementById("urlNameTrainingCourseCategoryPic").value = "";
            $('#TrainingCourseCategoryPicFileUpload').val('');
            $('#PreviewTrainingCourseCategoryImage').attr({ 'src': '' });
        }
        $scope.ToggleCollpaseTrainingCourseCategory = function (event, IDTrainingCourseCategory) {
            if (IDTrainingCourseCategory != undefined) {
                $(event.currentTarget).parent().find('#List' + IDTrainingCourseCategory).slideToggle();
                $(event.currentTarget).parent().find('#icon' + IDTrainingCourseCategory).toggleClass('fa-angle-double-left fa-angle-double-down');
            }

        }
        $scope.ChangeTrainingCourseCategoryAttachPDF = function (element) {
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
            document.getElementById("urlNameTrainingCourseCategoryAttachPDF").value = element.files[0].name;

            var file = document.querySelector('#TrainingCourseCategoryAttachPDFFileUpload').files[0];
            var reader = new FileReader();

            reader.addEventListener("load", function () {
                $scope.CurrentTrainingCourseCategory.AttachPDFFile = reader.result;
            }, false);

            if (file) {
                reader.readAsDataURL(file);
            }
        }
        $scope.ClearTrainingCourseCategoryAttachPDFFileUpload = function () {
            document.getElementById("urlNameTrainingCourseCategoryAttachPDF").value = "";
            $('#TrainingCourseCategoryAttachPDFFileUpload').val('');
            $scope.ContentAttachPDFUrl = '';
        }
        //------------------------------------------------------

        //-------------------Modal Training Course User
        $scope.GetPersonelInCompany = function () {
            PersonelInCompanyService.GetPersonelInCompany().then(function (result) {
                $scope.PersonelInCompanies = result.data;
            }).catch(function () {
            }).finally(function () {
            });
        };
        $scope.CurrentTrainingCourseUser = {};
        $scope.AddNewTrainingCourseUser = function () {
            $('#DivInfoTrainingCourseUser').slideToggle();
            $('#btnNewTrainingCourseUser').fadeOut();
            $('#btnSaveTrainingCourseUser').val('Add');
            $('html, body').animate({
                scrollTop: $("#DivInfoTrainingCourseUser").offset().top
            }, 1000);
            $scope.CurrentTrainingCourseUser = {
            };
        }
        $scope.CancelTrainingCourseUser = function () {
            $('#DivInfoTrainingCourseUser').slideToggle();
            $('#btnNewTrainingCourseUser').fadeIn();
        }
        $scope.GetTrainingCourseUser = function () {
            var Param = {
                IDTrainingCourse: $scope.SelectedIDTrainingCourse
            };
            TrainingCourseUserService.GetTrainingCourseUser_ByIDTrainingCourse(Param).then(function (result) {
                $scope.TrainingCourseUsers = result.data;
            }).catch(function () {
            }).finally(function () {
            });
        };
        $scope.GetWorkType = function () {
            WorkTypeService.GetWorkType().then(function (result) {
                $scope.WorkTypes = result.data;
            }).catch(function () {
            }).finally(function () {
            });
        };
        $scope.SaveTrainingCourseUser = function () {
            if ($('#btnSaveTrainingCourseUser').val() == 'Add') {
                $scope.NewTrainingCourseUser = {
                    IDTrainingCourseUser: NewGuid(),
                    IDTrainingCourse: $scope.SelectedIDTrainingCourse,
                    IDUser: $scope.CurrentTrainingCourseUser.IDUser,
                    IDWorkType: $scope.CurrentTrainingCourseUser.IDWorkType,
                    CertificationID: $scope.CurrentTrainingCourseUser.CertificationID
                };
                TrainingCourseUserService.AddTrainingCourseUser($scope.NewTrainingCourseUser).then(function (result) {
                    $scope.CurrentTrainingCourseUser = {}
                    $scope.GetTrainingCourseUser();
                    AutoClosingSuccessAlert('Registration has been succeeded!', 3000);
                }).catch(function () {
                    AutoClosingErrorAlert('Registration has been failed!', 3000);
                }).finally(function () {
                    $scope.CancelTrainingCourseUser();
                });
            }
            else if ($('#btnSaveTrainingCourseUser').val() == 'Update') {
                $scope.UpdateTrainingCourseUser = {
                    IDTrainingCourseUser: $scope.CurrentTrainingCourseUser.IDTrainingCourseUser,
                    IDTrainingCourse: $scope.SelectedIDTrainingCourse,
                    IDUser: $scope.CurrentTrainingCourseUser.IDUser,
                    CertificationID: $scope.CurrentTrainingCourseUser.CertificationID
                };
                TrainingCourseUserService.UpdateTrainingCourseUser($scope.UpdateTrainingCourseUser).then(function (result) {
                    $scope.GetTrainingCourseUser();
                    AutoClosingSuccessAlert('Update has been succeeded!', 3000);
                }).catch(function () {
                    AutoClosingErrorAlert('Update has been failed!', 3000);
                }).finally(function () {
                    $scope.CancelTrainingCourseUser();
                });
            }
        };
        $scope.DeleteTrainingCourseUser = function (_IDTrainingCourseUser) {
            $scope.DelTrainingCourseUserInfo = {
                IDTrainingCourseUser: _IDTrainingCourseUser
            };
            var ConfirmDelete = $window.confirm('Are you sure?');
            if (ConfirmDelete) {
                TrainingCourseUserService.DeleteTrainingCourseUser($scope.DelTrainingCourseUserInfo).then(function (result) {
                    $scope.GetTrainingCourseUser();
                    AutoClosingSuccessAlert('Delete has been succeeded!', 3000);
                }).catch(function () {
                    AutoClosingErrorAlert('Delete has been failed!', 3000);
                }).finally(function () {
                });
            }
        };
        $scope.EditTrainingCourseUser = function (_item) {
            $('#DivInfoTrainingCourseUser').slideDown();
            $('#btnNewTrainingCourseUser').fadeOut();
            $('#btnSaveTrainingCourseUser').val('Update');
            $scope.CurrentTrainingCourseUser.IDTrainingCourseUser = _item.IDTrainingCourseUser,
                $scope.CurrentTrainingCourseUser.IDUser = _item.IDUser,
                $scope.CurrentTrainingCourseUser.CertificationID = _item.CertificationID,
                $scope.CurrentTrainingCourseUser.IDWorkType = _item.IDWorkType,
                $('html, body').animate({
                    scrollTop: $("#DivInfoTrainingCourseUser").offset().top
                }, 1000);
        }
        //------------------------------------------------------



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

        //-------------------Modal TrainingCourseBatch-----------------------------
        $scope.OpenModalTrainingCourseBatch = function () {
            $('#ModalTrainingCourseBatch').modal('show');
            $scope.GetTrainingCourseBatch();
        }
        $scope.NewTrainingCourseBatch = function () {
            $('#DivInfoTrainingCourseBatch').slideToggle();
            $('#btnNewTrainingCourseBatch').fadeOut();
            $('#btnSaveTrainingCourseBatch').val('Add');
            $scope.CurrentTrainingCourseBatch = {
            };
        }
        $scope.CancelTrainingCourseBatch = function () {
            $('#DivInfoTrainingCourseBatch').slideToggle();
            $('#btnNewTrainingCourseBatch').fadeIn();
        }
        $scope.GetTrainingCourseBatch = function () {
            TrainingCourseBatchService.GetTrainingCourseBatch().then(function (result) {
                $scope.TrainingCourseBatchs = result.data;
            }).catch(function () {
            }).finally(function () {
            });
        };
        $scope.SaveTrainingCourseBatch = function () {
            if ($('#btnSaveTrainingCourseBatch').val() == 'Add') {
                var NewTrainingCourseBatch = {
                    Name_Fa: $scope.CurrentTrainingCourseBatch.Name_Fa,
                    Description: $scope.CurrentTrainingCourseBatch.Description
                };
                TrainingCourseBatchService.AddTrainingCourseBatch(NewTrainingCourseBatch).then(function (result) {
                    $scope.CurrentTrainingCourseBatch = {}
                    $scope.GetTrainingCourseBatch();
                    AutoClosingSuccessAlert('Registration has been succeeded!', 3000);
                }).catch(function () {
                    AutoClosingErrorAlert('Registration has been failed!', 3000);
                }).finally(function () {
                    $scope.CancelTrainingCourseBatch();
                });
            }
            else if ($('#btnSaveTrainingCourseBatch').val() == 'Update') {
                var UpdateTrainingCourseBatch = {
                    IDTrainingCourseBatch: $scope.CurrentTrainingCourseBatch.IDTrainingCourseBatch,
                    Name_Fa: $scope.CurrentTrainingCourseBatch.Name_Fa,
                    Description: $scope.CurrentTrainingCourseBatch.Description
                };
                TrainingCourseBatchService.UpdateTrainingCourseBatch(UpdateTrainingCourseBatch).then(function (result) {
                    $scope.GetTrainingCourseBatch();
                    AutoClosingSuccessAlert('Update has been succeeded!', 3000);
                }).catch(function () {
                    AutoClosingErrorAlert('Update has been failed!', 3000);
                }).finally(function () {
                    $scope.CancelTrainingCourseBatch();
                });
            }
        };
        $scope.DeleteTrainingCourseBatch = function (_IDTrainingCourseBatch) {
            $scope.DelTrainingCourseBatchInfo = {
                IDTrainingCourseBatch: _IDTrainingCourseBatch
            };
            var ConfirmDelete = $window.confirm('Are you sure?');
            if (ConfirmDelete) {
                TrainingCourseBatchService.DeleteTrainingCourseBatch($scope.DelTrainingCourseBatchInfo).then(function (result) {
                    $scope.GetTrainingCourseBatch();
                    AutoClosingSuccessAlert('Delete has been succeeded!', 3000);
                }).catch(function () {
                    AutoClosingErrorAlert('Delete has been failed!', 3000);
                }).finally(function () {
                });
            }
        };
        $scope.EditTrainingCourseBatch = function (_item) {
            $('#DivInfoTrainingCourseBatch').slideDown();
            $('#btnNewTrainingCourseBatch').fadeOut();
            $('#btnSaveTrainingCourseBatch').val('Update');
            $scope.CurrentTrainingCourseBatch.IDTrainingCourseBatch = _item.IDTrainingCourseBatch;
            $scope.CurrentTrainingCourseBatch.Name_Fa = _item.Name_Fa;
            $scope.CurrentTrainingCourseBatch.Description = _item.Description;
        }

        $scope.OpenTrainingCourseBatchAssignModal = function (_item) {
            $('#ModalTrainingCourseBatchAssign').modal('show');
            $scope.GetTrainingCourseBatchTainingCourseByIDTrainingCourse(_item.IDTrainingCourse);
            $scope.SelectedIDRet = _item.IDTrainingCourse;
        }
        $scope.GetTrainingCourseBatchTainingCourseByIDTrainingCourse = function (_IDTrainingCourse) {
            var Param = {
                IDTrainingCourse: _IDTrainingCourse
            }
            TrainingCourseBatchTrainingCourseService.GetTrainingCourseBatchTainingCourseByIDTrainingCourse(Param).then(function (result) {
                $scope.SelectedTrainingCourseBatchs = result.data;
            }).catch(function () {
            }).finally(function () {
            });
        };
        $scope.ToggleSelectTrainingCourseBatch = function (_item) {
            var Param = {
                IDTrainingCourse: $scope.SelectedIDRet,
                IDTrainingCourseBatch: _item.IDTrainingCourseBatch
            }
            TrainingCourseBatchTrainingCourseService.ToggleSelectTrainingCourseBatch(Param).then(function (result) {

            }).catch(function () {
            }).finally(function () {
            });
        };

        $scope.InitiateContentModule = function (obj) {
            obj.IDRet = obj.IDTrainingCourseBatch;
            ContentModuleRetFactory.InitiateContentModuleRetModal(obj);
        };
        //-----------------------------------------------------------------

        $scope.GetTrainingCourseCategory();
        $scope.GetTrainingCourse();
    }]);




