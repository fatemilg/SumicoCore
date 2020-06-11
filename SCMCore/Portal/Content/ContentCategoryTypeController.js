myApp.controller('ContentCategoryTypeController', ['$scope', '$compile', '$window', 'ngProgress', 'ContentCategoryService', 'ContentCategoryTypeService',
    'ContentService', 'GalleryService', 'GalleryCategoryService', 'ContentCategoryContentService', 'ContentDictionaryService', 'ContentModuleService',
    'ContentModuleRetService', 'ContentRelateService', 'ContentModuleRetFactory', 'SentEmailFactory',
    function ($scope, $compile, $window, ngProgress, ContentCategoryService, ContentCategoryTypeService, ContentService, GalleryService, GalleryCategoryService,
        ContentCategoryContentService, ContentDictionaryService, ContentModuleService, ContentModuleRetService, ContentRelateService, ContentModuleRetFactory,
        SentEmailFactory) {
        $scope.CurrentContentCategory = {};
        $scope.ContentCategoryName_Fa = {};
        $scope.CurrentNewGalleryCategory = {};
        $scope.CurrentGalleryCategory = {};
        $scope.SelectedContentDictionary = [];
        $scope.CurrentModule = {};


        $scope.FillContentCategoryType = function () {
            ngProgress.start();
            $scope.Authorization = {
                IDLogUser: _IDLogUser
            };
            ContentCategoryTypeService.FillContentCategoryTypeComplete($scope.Authorization).then(function (result) {
                $scope.ContentCategoryTypes = result.data;
            }).catch(function () {
            }).finally(function () {
                ngProgress.stop();
                ngProgress.complete();
            });
        };

        $scope.ToggleCollpase = function (event, IDContentCategoryType, IDContentCategory) {
            if (IDContentCategory != undefined) {
                $(event.currentTarget).parent().find('#List' + IDContentCategoryType + IDContentCategory).slideToggle();
                $(event.currentTarget).parent().find('#icon' + IDContentCategoryType + IDContentCategory).toggleClass('fa-angle-double-left fa-angle-double-down');
            }
            else {
                $(event.currentTarget).parent().find('#List' + IDContentCategoryType).slideToggle();
                $(event.currentTarget).parent().find('#icon' + IDContentCategoryType).toggleClass('fa-angle-double-left fa-angle-double-down');
            }
        }

        $scope.OpenNavigationMenu = function (IDContentCategoryType, IDContentCategory) {
            $('.NavigationInTree').hide();
            if (IDContentCategory != undefined) {
                $('#Navigation' + IDContentCategoryType + IDContentCategory).css({ display: 'inline-block' });
            }
            else {
                var Navigation = $('#Navigation' + IDContentCategoryType);
                Navigation.css({ display: 'inline-block' });
                Navigation.find('#Edit').css({ display: 'none' });
                Navigation.find('#Remove').css({ display: 'none' });
                Navigation.find('#NewContent').css({ display: 'none' });

            }
        }

        $scope.OpenNavigationContent = function (event, IDContentCategoryContent) {
            $('.NavigationInTree').hide();
            var Navigation = $('#Navigation' + IDContentCategoryContent);
            Navigation.css({ display: 'inline-block' });

        }

        $scope.CloseNavigationMenu = function () {
            $('.NavigationInTree').hide();
        }

        $scope.AddNewCategoryNode = function (event, _IDContentCategoryType, _IDContentCategory) {
            $('.InfoOfTree').hide();
            $('.btn-tree').css({ display: 'inline-block' });
            $(event.currentTarget).hide();
            if (_IDContentCategory != undefined) {
                var InfoOfTree = $('#InfoOfTree' + _IDContentCategoryType + _IDContentCategory)
                $('#InfoOfTree' + _IDContentCategoryType + _IDContentCategory).css({ display: 'inline-block' });
                InfoOfTree.find('#Edit').hide();
            }
            else {
                var InfoOfTree = $('#InfoOfTree' + _IDContentCategoryType)
                $('#InfoOfTree' + _IDContentCategoryType).css({ display: 'inline-block' });
                InfoOfTree.find('#Edit').hide();
            }
        };

        $scope.SaveNewContentCategory = function (event, _IDContentCategoryType, _IDContentCategory) {
            $scope.NewContentCategory = {
                IDContentCategory: NewGuid(),
                IDContentCategoryType: _IDContentCategoryType,
                IDParent: _IDContentCategory,
                Name_Fa: $scope.CurrentContentCategory.Name_Fa
            };
            ContentCategoryService.AddContentCategory($scope.NewContentCategory).then(function (result) {
                AutoClosingSuccessAlert('ثبت اطلاعات با موفقیت انجام شد!', 3000);
                $scope.HideInfoOFTree(event, _IDContentCategoryType, _IDContentCategory);
                $scope.FillContentCategoryType();
            }).catch(function () {
            }).finally(function () {
            });
        };

        $scope.SaveEditContentCategory = function (event, _IDContentCategoryType, _IDContentCategory, EditedName_Fa) {
            ngProgress.start();
            $scope.UpdateContentCategory = {
                IDContentCategory: _IDContentCategory,
                Name_Fa: EditedName_Fa
            };
            ContentCategoryService.UpdateContentCategory($scope.UpdateContentCategory).then(function (result) {
                AutoClosingSuccessAlert('ویرایش اطلاعات با موفقیت انجام شد!', 3000);
                $scope.CancelEdit(event, _IDContentCategoryType, _IDContentCategory, EditedName_Fa);

            }).catch(function () {
            }).finally(function () {
                ngProgress.stop();
                ngProgress.complete();
            });
        };

        $scope.EditCategory = function (event, _Name_Fa) {
            $('.txtEditName_Fa').removeClass('form-control');
            $('.txtEditName_Fa').attr({ disabled: 'disabled' });
            $('.txtEditName_Fa').addClass('txtTreeItem');


            var txtEditName_Fa = $(event.currentTarget).parent().parent().children('.TreeItem').find('.txtEditName_Fa');
            txtEditName_Fa.removeClass('txtTreeItem');
            txtEditName_Fa.removeAttr('disabled');
            txtEditName_Fa.addClass('form-control');
            txtEditName_Fa.css({ 'width': '80%', 'display': 'inline-block' });

            var Navigation = $(event.currentTarget).parent();
            Navigation.find('#Add').addClass('display-none');
            Navigation.find('#Edit').addClass('display-none');
            Navigation.find('#Remove').addClass('display-none');
            Navigation.find('#NewContent').addClass('display-none');
            Navigation.find('#Cancel').removeClass('display-none');
            Navigation.find('#SaveEdit').removeClass('display-none');

        }

        $scope.HideInfoOFTree = function (event, IDContentCategoryType, IDContentCategory) {
            $(event.currentTarget).parent().parent().children('.NavigationInTree').find('#Add').css({ display: 'inline-block' });
            $scope.CurrentContentCategory.Name_Fa = '';
            $('.InfoOfTree').hide();
        };

        $scope.DeleteCategory = function (event, _IDContentCategoryType, _IDContentCategory) {
            $scope.DeleteContentCategory = {
                IDContentCategory: _IDContentCategory
            };
            var ConfirmDelete = $window.confirm('آیا مطئن هستید؟');
            if (ConfirmDelete) {
                ContentCategoryService.DeleteContentCategory($scope.DeleteContentCategory).then(function (result) {
                    AutoClosingSuccessAlert('حذف اطلاعات با موفقیت انجام شد!', 3000);
                    $scope.HideInfoOFTree(event, _IDContentCategoryType, _IDContentCategory);
                    $scope.FillContentCategoryType();
                }).catch(function () {
                }).finally(function () {
                });
            }

        };

        $scope.CancelEdit = function (event, IDContentCategoryType, IDContentCategory, EditedName_Fa) {
            var Navigation = $(event.currentTarget).parent();
            Navigation.find('#Add').removeClass('display-none');
            Navigation.find('#Edit').removeClass('display-none');
            Navigation.find('#Remove').removeClass('display-none');
            Navigation.find('#NewContent').removeClass('display-none');
            Navigation.find('#Cancel').addClass('display-none');
            Navigation.find('#SaveEdit').addClass('display-none');

            $('.txtEditName_Fa').removeClass('form-control');
            $('.txtEditName_Fa').attr({ disabled: 'disabled' });
            $('.txtEditName_Fa').addClass('txtTreeItem');
            Navigation.parent().children('.TreeItem').find('.txtEditName_Fa').val(EditedName_Fa);
        };

        $scope.AddNewContent = function (event, _IDContentCategory, CategoryName) {
            $scope.SelectedContent = undefined;
            $scope.ContentModalTitle = 'افزودن مطلب برای ' + CategoryName;
            $scope.IDContentCategoryForNewContent = _IDContentCategory;
            $('#btnSaveContent').val('Add');
            $('#ModalContent').modal('show');
            $scope.GetContent = undefined;
            $scope.CurrentContent = {};
            $scope.ClearPicFileUpload();
        }

        $scope.CancelContent = function () {
            $scope.CurrentContent = {};
            $('#ModalContent').modal('hide');
            $scope.ClearPicFileUpload();
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
            document.getElementById("urlNameContentPic").value = element.files[0].name;



            var preview = document.querySelector('#PreviewContentImage');
            var file = document.querySelector('#PicFileUpload').files[0];
            var reader = new FileReader();

            reader.addEventListener("load", function () {
                preview.src = reader.result;
                $scope.CurrentContent.PicFile = reader.result;
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
            document.getElementById("urlNameContentPic").value = "";
            $('#PicFileUpload').val('');
            $('#PreviewContentImage').attr({ 'src': '' });
            $scope.ContentPicUrl = '';
        }

        $scope.SaveContent = function () {
            if ($scope.CurrentContent.PicFile !== undefined && $scope.CurrentContent.PicFile !== '') {
                $scope.ObjContent = {
                    IDContent: NewGuid(),
                    IDContentCategory: $scope.IDContentCategoryForNewContent,
                    Name_Fa: $scope.CurrentContent.Name_Fa,
                    Abstract_Fa: $scope.CurrentContent.Abstract_Fa,
                    MetaTags: $scope.CurrentContent.MetaTags,
                    MetaDescriptions: $scope.CurrentContent.MetaDescriptions,
                    Description_Fa: $scope.CurrentContent.Description_Fa,
                    PicUrl: $scope.ContentPicUrl,
                    Status: 1
                };
                $scope.Authorization = {
                    IDLogUser: _IDLogUser
                };


                var Param = {};
                Param.Authorization = $scope.Authorization;
                Param.PicFile = $scope.CurrentContent.PicFile;
                Param.Content = $scope.ObjContent;
                if ($scope.GetContent == undefined) {
                    ContentService.AddContent(Param).then(function (result) {
                        AutoClosingSuccessAlert('ثبت اطلاعات با موفقیت انجام شد!', 3000);
                        $scope.FillContentCategoryType();
                        $scope.CurrentContent = {};
                        $scope.ClearPicFileUpload();
                        $('#ModalContent').modal('hide');
                        $scope.GetContent = undefined;


                    }).catch(function () {
                    }).finally(function () {
                    });
                }
                else {
                    $scope.ObjContent.IDContent = $scope.GetContent.IDContent;
                    ContentService.UpdateContent(Param).then(function (result) {
                        AutoClosingSuccessAlert('ویرایش اطلاعات با موفقیت انجام شد!', 3000);
                        $scope.FillContentCategoryType();
                        $scope.CurrentContent = {};
                        $scope.ClearPicFileUpload();
                        $scope.GetContent = undefined;

                        $('#ModalContent').modal('hide');

                    }).catch(function () {
                    }).finally(function () {
                        ngProgress.stop();
                        ngProgress.complete();
                    });
                }
            }
            else {
                AutoClosingWarningAlert("لطفا برای این مطلب یک عکس انتخاب کنید!", 4000)
            }

        }

        $scope.EditContent = function (event, _Content) {
            $scope.SelectedContent = _Content;
            $scope.IDContentCategoryForNewContent = _Content.IDContentCategory;
            $scope.ContentPicUrl = null;
            $scope.ActivationContent = _Content.Active;
            $scope.GetContent = {
                IDContent: _Content.IDContent
            };
            ContentService.FillContentDataByID($scope.GetContent).then(function (result) {
                $scope.CurrentContent = result.data[0];
                document.querySelector('#PreviewContentImage').src = '/' + $scope.CurrentContent.PicUrl;
                $scope.CurrentContent.PicFile = {};
                $('#btnSaveContent').val('Update');
                $('#ModalContent').modal('show');
                $scope.ContentModalTitle = 'ویرایش  ' + $scope.CurrentContent.Name_Fa;
            }).catch(function () {
            }).finally(function () {
            });


        }

        $scope.DeleteContent = function (event, _IDContent) {
            $scope.ObjContent = {
                IDContent: _IDContent
            };
            var ConfirmDeleteContent = $window.confirm('آیا مطئن هستید؟');
            if (ConfirmDeleteContent) {
                ContentService.DeleteContent($scope.ObjContent).then(function (result) {
                    AutoClosingSuccessAlert('حذف اطلاعات با موفقیت انجام شد!', 3000);
                    $scope.ObjContent = {};
                    $scope.FillContentCategoryType();
                }).catch(function () {
                }).finally(function () {
                });
            }
        }

        $scope.OpenOtherContentCategory = function (_IDRet, Name_Fa) {
            $scope.CountCategory = 0;
            $scope.SelectedIDRet = _IDRet;
            $scope.SelectedName = Name_Fa;
            $('#ModalOtherContentCategory').modal('show');
            $scope.OtherContentCategoryModalTitle = 'افزودن ' + Name_Fa + ' به دسته بندی های دیگر';
        }

        $scope.ToggleList = function (_Category) {
            if (_Category.ShowChild) {
                _Category.ShowChild = false;
            }
            else {
                _Category.ShowChild = true;
            }
            $scope.CheckRelateContent(_Category);
        }

        $scope.PreventDeleteLessThanOneItem = function (_Source) {
            for (key in _Source) {
                var item = _Source[key];
                if (item.hasOwnProperty('ContentCategory')) {
                    $scope.PreventDeleteLessThanOneItem(item.ContentCategory)
                }
                else {
                    if (item.hasOwnProperty('Content')) {
                        if ($scope.CheckExist(item))
                            $scope.CountCategory++;
                    }
                }
            }
        }

        $scope.ToggleOtherContentCategory = function (_item, event) {
            if ($scope.CheckExist(_item)) {
                $scope.CountCategory = 0;
                $scope.PreventDeleteLessThanOneItem($scope.ContentCategoryTypes);
                if ($scope.CountCategory > 1) {
                    var objContentCategoryContent = {
                        IDContentCategory: _item.IDContentCategory,
                        IDContent: $scope.SelectedIDRet
                    };
                    ContentCategoryContentService.DeleteContentCategoryContent(objContentCategoryContent).then(function (result) {
                        AutoClosingSuccessAlert('حذف اطلاعات با موفقیت انجام شد!', 3000);
                        for (i = 0; i < _item.Content.length; i++) {
                            if (_item.Content[i].IDContent == $scope.SelectedIDRet)
                                _item.Content.splice(i, 1);;
                        }
                    }).catch(function () {
                    }).finally(function () {
                    });
                }
                else {
                    AutoClosingWarningAlert('برای هر مطلب باید حداقل یک دسته وجود داشته باشد.', 5000);
                    $(event.currentTarget).prop("checked", true)
                }
            }
            else {
                var objContentCategoryContent = {
                    IDContentCategory: _item.IDContentCategory,
                    IDContent: $scope.SelectedIDRet,
                    Sort: _item.Sort
                };
                ContentCategoryContentService.AddContentCategoryContent(objContentCategoryContent).then(function (result) {
                    AutoClosingSuccessAlert('ثبت اطلاعات با موفقیت انجام شد!', 3000);
                    if (!_item.Content) {
                        _item.Content = [];
                    }
                    _item.Content.push({
                        'IDContent': $scope.SelectedIDRet, 'Name_Fa': $scope.SelectedName, 'IDContentCategoryContent': NewGuid()
                    });
                }).catch(function () {
                }).finally(function () {
                });

            }
        }

        $scope.ChangeContentDescriptionMode = function () {
            if ($scope.CurrentContent.PureTextMode == undefined)
                $scope.CurrentContent.PureTextMode = true;
            else {
                $scope.CurrentContent.PureTextMode = undefined;
            }
        }

        $scope.CheckExist = function (_item) {
            if (_item.Content) {
                for (i = 0; i < _item.Content.length; i++) {
                    if (_item.Content[i].IDContent == $scope.SelectedIDRet) return true;
                }
                return false;
            }
        }

        $scope.GetDictionary = function () {
            var objDictionary = {
                IDContent: $scope.SelectedIDRet
            };
            ContentDictionaryService.GetContentDictionaryByIDContent(objDictionary).then(function (result) {
                $scope.Dictionaries = result.data;

            }).catch(function () {
            }).finally(function () {
            });
        };

        $scope.GetSelectedDictionaryByIDContent = function () {
            var objDictionary = {
                IDContent: $scope.SelectedIDRet
            };
            ContentDictionaryService.GetSelectedDataByIDContent(objDictionary).then(function (result) {
                $scope.SelectedContentDictionary = result.data;

            }).catch(function () {
            }).finally(function () {
            });
        };

        $scope.OpenDictionaryModal = function (_IDRet, Name_Fa) {
            $scope.SelectedIDRet = _IDRet;
            $scope.SelectedName = Name_Fa;
            $('#ModalDictionary').modal('show');
            $scope.DictionaryModalTitle = 'افزودن واژگان دیکشنری به  ' + Name_Fa;
            $scope.GetDictionary();
            $scope.GetSelectedDictionaryByIDContent();
        }

        $scope.ToggleContentDictionary = function (_item) {
            ngProgress.start();
            var objDictionary = {
                IDContent: $scope.SelectedIDRet,
                IDDictionary: _item.IDDictionary
            };
            if (_item.Exist) {
                ContentDictionaryService.DeleteContentDictionary(objDictionary).then(function (result) {
                    delete _item.Exist;
                    for (i = 0; i < $scope.SelectedContentDictionary.length; i++) {
                        if ($scope.SelectedContentDictionary[i].IDDictionary == _item.IDDictionary)
                            $scope.SelectedContentDictionary.splice(i, 1);
                    }
                }).catch(function () {
                }).finally(function () {
                    ngProgress.stop();
                    ngProgress.complete();
                });
            }
            else {
                ContentDictionaryService.AddContentDictionary(objDictionary).then(function (result) {
                    _item.Exist = true;
                    $scope.SelectedContentDictionary.push({
                        IDContent: $scope.SelectedIDRet,
                        IDDictionary: _item.IDDictionary,
                        KeyWord: _item.KeyWord,
                        Sort: _item.Sort
                    })
                }).catch(function () {
                }).finally(function () {
                    ngProgress.stop();
                    ngProgress.complete();
                });
            }
        }

        $scope.SortTableSelectedContentDictionary = {
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
                    ngProgress.start();
                    var myJsonString = JSON.stringify(evt.models);
                    var objContentDictionary = {
                        JsonContentDictionary: myJsonString
                    }
                    ContentDictionaryService.ChangeSortInContentDictionary(objContentDictionary).then(function (result) {

                    }).catch(function () {
                        AutoClosingErrorAlert('Error in movement!', 5000);
                    }).finally(function () {
                        ngProgress.stop();
                        ngProgress.complete();
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

        $scope.ImageGallery = function () {
            if ($scope.CurrentGalleryCategory.IDGalleryCategory != undefined) {
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

        $scope.GetGalleryCategory = function () {
            GalleryCategoryService.GetGalleryCategoryByIDRet().then(function (result) {
                $scope.GalleryCategorys = result.data;
            }).catch(function () {
            }).finally(function () {
            });
        };

        $scope.OpenGalleryModal = function (_IDRet, Name_Fa) {
            $scope.GalleryImages = undefined;
            $scope.GalleryCategorys = undefined;
            $scope.SelectedIDRet = _IDRet;
            $('#ModalImageGallery').modal('show');
            $scope.ImageGalleryModalTitle = 'گالری تصاویر ' + Name_Fa;
            $scope.GetGalleryCategory();
            $scope.ImageGallery();
        };

        $scope.OpenSortContentModal = function (_SubCat) {
            $('#ModalSortContent').modal('show');
            $scope.SortContentModalTitle = _SubCat.Name_Fa;
            $scope.ContentListForSort = _SubCat.Content;
        };

        $scope.SortContentsInCategory = {
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
                    var obj = {
                        JsonContentCategoryContent: JSON.stringify(evt.models)
                    };
                    ContentCategoryContentService.UpdateSortContentCategoryContent(obj).then(function (result) {
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

        //-----------------------------------------------------------------------------------------
        $scope.InitiateContentModule = function (obj) {
            obj.IDRet = obj.IDContentCategory;
            ContentModuleRetFactory.InitiateContentModuleRetModal(obj);
        };
        //-----------------------------------------------------------------------------------------
        $scope.InitiateContentModuleList = function () {
            ContentModuleRetFactory.InitiateContentModuleList();
        };
        //-----------------------------------------------------------------------------------------
        $scope.InitSentEmail = function (_Content) {
            var _obj = {
                IDXRet: _Content.IDX,
                IDRet: _Content.IDContent,
                Subject: _Content.Name_Fa,
                body: _Content.Description_Fa
            };
            SentEmailFactory.InitiateSentEmailModal(_obj);
        };
        //-----------------------------------------------------------------------------------------

        
        //-----------------------------------------------------------------------------------------
        $scope.OpenModalContentRelate = function (_Content, _ContentCategoryType) {
            $scope.GetContentRelateByIDContent(_Content);
            $scope.SelectedContentForRelation = _Content.IDContent;
            $scope.ContentRelateTitle = _Content.Name_Fa;
            $('#ModalContentRelate').modal('show');
            $scope.ClearCheckedRelatedContent($scope.ContentCategoryTypes);
        };

        $scope.GetContentRelateByIDContent = function (_Content) {
            var Relate = {
                IDContent: _Content.IDContent
            }
            ContentRelateService.GetContentRelateByIDContent(Relate).then(function (result) {
                $scope.RelatedContents = result.data;
            }).catch(function () {
            }).finally(function () {
            });
        };

        $scope.SaveToggleContentRet = function (_Content) {
            ngProgress.start();
            var Relate = {
                IDSourceContent: $scope.SelectedContentForRelation,
                IDDestinationContent: _Content.IDContent
            }
            if (!_Content.isRelated) {
                ContentRelateService.AddContentRelate(Relate).then(function (result) {
                    _Content.isRelated = true;
                    $scope.GetContentRelateByIDContent(_Content);
                }).catch(function () {
                }).finally(function () {
                    ngProgress.stop();
                    ngProgress.complete();
                });
            }
            else {
                ContentRelateService.DeleteContentRelate(Relate).then(function (result) {
                    _Content.isRelated = false;
                    $scope.GetContentRelateByIDContent(_Content);
                }).catch(function () {
                }).finally(function () {
                    ngProgress.stop();
                    ngProgress.complete();
                });
            }
        };

        $scope.CheckRelateContent = function (_Category) {
            if (_Category.Content) {
                for (i = 0; i < _Category.Content.length; i++) {
                    if ($scope.RelatedContents) {
                        for (j = 0; j < $scope.RelatedContents.length; j++) {
                            if ($scope.RelatedContents[j].IDSourceContent === _Category.Content[i].IDContent ||
                                $scope.RelatedContents[j].IDDestinationContent === _Category.Content[i].IDContent) {
                                _Category.Content[i].isRelated = true;
                            }

                        }
                    }
                }
            }
        };

        $scope.ClearCheckedRelatedContent = function (_Category) {
            for (i = 0; i < _Category.length; i++) {
                _Category[i].ShowChild = false;
                if (_Category[i].Content) {
                    for (j = 0; j < _Category[i].Content.length; j++) {
                        _Category[i].Content[j].isRelated = false;
                    }

                } else {
                    if (_Category[i].ContentCategory) {
                        $scope.ClearCheckedRelatedContent(_Category[i].ContentCategory)
                    }
                }
            }
        };

        $scope.ToggleActivation = function () {
            ngProgress.start();
            Param = {
                IDContent: $scope.SelectedContent.IDContent
            };
            ContentService.ToggleActivation(Param).then(function (result) {
                $scope.SelectedContent.Active = !$scope.SelectedContent.Active;

            }).catch(function () {
            }).finally(function () {
                ngProgress.stop();
                ngProgress.complete();
            });

        };
        //------------------------------------------------------



        $scope.FillContentCategoryType();
    }]);
