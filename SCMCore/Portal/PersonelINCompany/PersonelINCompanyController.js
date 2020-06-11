myApp.controller('PersonelInCompanyController', ['$scope', '$compile', 'ContactWayService', 'ContactWayTypeService', 'PersonelInCompanyService', 'CompanyService', 'LevelOfPersonelInCompanyService', 'OrganizationalPositionService', 'UserGroupService', '$window',
    function ($scope, $compile, ContactWayService, ContactWayTypeService, PersonelInCompanyService, CompanyService, LevelOfPersonelInCompanyService, OrganizationalPositionService, UserGroupService, $window) {
        $scope.CurrentPersonelInCompany = {};
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



        //---------------- Personel In Company--------------------------
        $scope.ClearPersonelInCompany = function () {
            $('#DivInfoPersonelInCompany').slideToggle();
            $('#btnNewPersonelInCompany').fadeOut();
            $('#btnSavePersonelInCompany').val('Add');
            document.querySelector('#PreviewPersonelImage').src = '';
            $('html, body').animate({
                scrollTop: $("#DivInfoPersonelInCompany").offset().top
            }, 1000);
            $scope.CurrentPersonelInCompany = {
            };
            $scope.CurrentPersonelInCompany.NationalCode = '';
        };
        $scope.CancelPersonelInCompany = function () {
            $('#DivInfoPersonelInCompany').slideToggle();
            $('#btnNewPersonelInCompany').fadeIn();
        }
        $scope.GetPersonelInCompany = function () {
            PersonelInCompanyService.GetPersonelInCompany().then(function (result) {
                $scope.PersonelInCompanys = result.data;
            }).catch(function () {
            }).finally(function () {
            });
        };
        $scope.SavePersonelInCompany = function () {
            if ($('#btnSavePersonelInCompany').val() == 'Add') {
                $scope.NewPersonelInCompany = {
                    IDPersonelInCompany: NewGuid(),
                    FName_En: $scope.CurrentPersonelInCompany.FName_En,
                    LName_En: $scope.CurrentPersonelInCompany.LName_En,
                    FName_Fa: $scope.CurrentPersonelInCompany.FName_Fa,
                    LName_Fa: $scope.CurrentPersonelInCompany.LName_Fa,
                    NationalCode: $scope.CurrentPersonelInCompany.NationalCode,
                    BirthDate: $scope.CurrentPersonelInCompany.BirthDate != undefined ? jing($scope.CurrentPersonelInCompany.BirthDate, true) : '',
                    IDCompany: $scope.CurrentPersonelInCompany.IDCompany,
                    IDOrganizationalPosition: $scope.CurrentPersonelInCompany.IDOrganizationalPosition,
                    IDIntroducer: $scope.CurrentPersonelInCompany.IDIntroducer,
                    IDLevelOfPersonelInCompany: $scope.CurrentPersonelInCompany.IDLevelOfPersonelInCompany,
                    IDInterfaceSale: $scope.CurrentPersonelInCompany.IDInterfaceSale,
                    PicFile: $scope.CurrentPersonelInCompany.PicFile,
                    Status: 1
                };
                PersonelInCompanyService.AddPersonelInCompany($scope.NewPersonelInCompany).then(function (result) {
                    $scope.CurrentPersonelInCompany = {}
                    $scope.GetPersonelInCompany();
                    AutoClosingSuccessAlert('Registration has been succeeded!', 3000);
                }).catch(function () {
                }).finally(function () {
                    $scope.CancelPersonelInCompany();
                });
            }
            else if ($('#btnSavePersonelInCompany').val() == 'Update') {
                $scope.UpdatePersonelInCompany = {
                    IDPersonelInCompany: $scope.CurrentPersonelInCompany.IDPersonelInCompany,
                    FName_En: $scope.CurrentPersonelInCompany.FName_En,
                    LName_En: $scope.CurrentPersonelInCompany.LName_En,
                    FName_Fa: $scope.CurrentPersonelInCompany.FName_Fa,
                    LName_Fa: $scope.CurrentPersonelInCompany.LName_Fa,
                    NationalCode: $scope.CurrentPersonelInCompany.NationalCode,
                    IDCompany: $scope.CurrentPersonelInCompany.IDCompany,
                    BirthDate: $scope.CurrentPersonelInCompany.BirthDate != undefined && $scope.CurrentPersonelInCompany.BirthDate != '' ? jing($scope.CurrentPersonelInCompany.BirthDate, true) : '',
                    IDOrganizationalPosition: $scope.CurrentPersonelInCompany.IDOrganizationalPosition,
                    IDIntroducer: $scope.CurrentPersonelInCompany.IDIntroducer,
                    IDLevelOfPersonelInCompany: $scope.CurrentPersonelInCompany.IDLevelOfPersonelInCompany,
                    PicFile: $scope.CurrentPersonelInCompany.PicFile,
                    IDInterfaceSale: $scope.CurrentPersonelInCompany.IDInterfaceSale

                };
                PersonelInCompanyService.UpdatePersonelInCompany($scope.UpdatePersonelInCompany).then(function (result) {
                    $scope.GetPersonelInCompany();
                    AutoClosingSuccessAlert('Update has been succeeded!', 3000);
                }).catch(function () {
                    AutoClosingErrorAlert('Update has been faield!', 3000);
                }).finally(function () {
                    $scope.CancelPersonelInCompany();
                });
            }
        };
        $scope.DeletePersonelInCompany = function (_IDPersonelInCompany) {
            $scope.DelPersonelInCompanyInfo = {
                IDPersonelInCompany: _IDPersonelInCompany
            };
            var ConfirmDelete = $window.confirm('Are you sure?');
            if (ConfirmDelete) {
                PersonelInCompanyService.DeletePersonelInCompany($scope.DelPersonelInCompanyInfo).then(function (result) {
                    $scope.GetPersonelInCompany();
                    AutoClosingSuccessAlert('Delete has been succeeded!', 3000);
                }).catch(function () {
                    AutoClosingErrorAlert('Delete has been failed!', 3000);
                }).finally(function () {
                });
            }
        };
        $scope.EditPersonelInCompany = function (_item) {
            $('#DivInfoPersonelInCompany').slideDown();
            $('#btnNewPersonelInCompany').fadeOut();
            $('#btnSavePersonelInCompany').val('Update');
            $scope.CurrentPersonelInCompany.IDPersonelInCompany = _item.IDPersonelInCompany,
                $scope.CurrentPersonelInCompany.FName_En = _item.FName_En;
            $scope.CurrentPersonelInCompany.LName_En = _item.LName_En;
            $scope.CurrentPersonelInCompany.FName_Fa = _item.FName_Fa;
            $scope.CurrentPersonelInCompany.LName_Fa = _item.LName_Fa;
            $scope.CurrentPersonelInCompany.NationalCode = _item.NationalCode;
            $scope.CurrentPersonelInCompany.BirthDate = _item.BirthDate != undefined && _item.BirthDate != '' ? ginj(_item.BirthDate.split("T")[0].replace('-', '/').replace('-', '/'), true) : '';
            $scope.CurrentPersonelInCompany.IDCompany = _item.IDCompany;
            $scope.CurrentPersonelInCompany.IDLevelOfPersonelInCompany = _item.IDLevelOfPersonelInCompany;
            $scope.CurrentPersonelInCompany.IDOrganizationalPosition = _item.IDOrganizationalPosition;
            $scope.CurrentPersonelInCompany.IDIntroducer = _item.IDIntroducer;
            $scope.CurrentPersonelInCompany.IDInterfaceSale = _item.IDInterfaceSale;
            $scope.CurrentPersonelInCompany.Status = _item.Status;
            document.querySelector('#PreviewPersonelImage').src = '/' + _item.PicUrl;
            $scope.CurrentPersonelInCompany.PicFile = {};
            $('html, body').animate({
                scrollTop: $("#DivInfoPersonelInCompany").offset().top
            }, 1000);
        };
        $scope.ChangePersonelFile = function (element) {
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
            document.getElementById("urlNamePersonelPic").value = element.files[0].name;

            var preview = document.querySelector('#PreviewPersonelImage');
            var file = document.querySelector('#PersonelPicFileUpload').files[0];
            var reader = new FileReader();

            reader.addEventListener("load", function () {
                preview.src = reader.result;
                $scope.CurrentPersonelInCompany.PicFile = reader.result;
            }, false);

            if (file) {
                reader.readAsDataURL(file);
            }
        }
        $scope.ClearPersonelPicFileUpload = function () {
            document.getElementById("urlNamePersonelPic").value = "";
            $('#PersonelPicFileUpload').val('');
            $('#PreviewPersonelImage').attr({ 'src': '' });
        }
        //-------------------------------------------------------------------



        //----------------Modal Contact Way ---------------------------------
        $scope.AddNewContactWay = function (_IDPersonelInCompany) {
            $('#ModalContactWay').modal('show');
            $scope.CurrentPersonelInCompany.IDPersonelInCompany = _IDPersonelInCompany;
            $scope.GetContactWay_ByIDUser(_IDPersonelInCompany);
            $scope.GetContactWayType();
        };
        $scope.GetCompany = function () {
            CompanyService.GetCompany().then(function (result) {
                $scope.Companies = result.data;
            }).catch(function () {
            }).finally(function () {
            });
        };
        $scope.GetLevelOfPersonelInCompany = function () {
            LevelOfPersonelInCompanyService.GetLevelOfPersonelInCompany().then(function (result) {
                $scope.LevelOfPersonelInCompanies = result.data;
            }).catch(function () {
            }).finally(function () {
            });
        };
        $scope.GetOrganizationalPosition = function () {
            OrganizationalPositionService.GetOrganizationalPosition().then(function (result) {
                $scope.OrganizationalPositions = result.data;
            }).catch(function () {
            }).finally(function () {
            });
        };
        $scope.AddNewCompany = function () {
            $('#ModalCompaniesManagement').modal('show');
            $scope.GetCompany();
        };
        $scope.AddLevelOfPersonelInCompany = function () {
            $('#ModalLevelOfPersonelInCompany').modal('show');
            $scope.GetLevelOfPersonelInCompany();
        };
        $scope.AddOrganizationalPosition = function () {
            $('#ModalOrganizationalPosition').modal('show');
            $scope.GetOrganizationalPosition();
        };
        $scope.setSortColumn = function (propertyName) {
            if ($scope.orderProperty === propertyName) {
                $scope.orderProperty = '-' + propertyName;
            } else if ($scope.orderProperty === '-' + propertyName) {
                $scope.orderProperty = propertyName;
            } else {
                $scope.orderProperty = propertyName;
            }
        };
        $scope.UserGroupForSale = function () {
            var objGroup = {
                UnicName: 'sale'
            };
            UserGroupService.GetUserGroupByGroupType(objGroup).then(function (result) {
                $scope.SaleUsers = result.data;
            }).catch(function () {
            }).finally(function () {
            });
        };
        $scope.SelectItem = function (_item) {

        }
        //------------------------------------------------------------------


        //----------------Modal Company ------------------------------------
        $scope.CurrentCompany = {};
        $scope.CreateNewCompany = function () {
            $('#DivInfoCompany').slideToggle();
            $('#btnNewCompany').fadeOut();
            $('#btnSaveCompany').val('Add');
            $('html, body').animate({
                scrollTop: $("#DivInfoCompany").offset().top
            }, 1000);
            $scope.CurrentCompany = {
            };
        }
        $scope.CancelCompany = function () {
            $('#DivInfoCompany').slideToggle();
            $('#btnNewCompany').fadeIn();
        }
        $scope.SaveCompany = function () {
            if ($('#btnSaveCompany').val() == 'Add') {
                $scope.NewCompany = {
                    IDCompany: NewGuid(),
                    Name_Fa: $scope.CurrentCompany.Name_Fa,
                    Status: 1
                };
                CompanyService.AddCompany($scope.NewCompany).then(function (result) {
                    $scope.CurrentCompany = {}
                    $scope.GetCompany();
                    AutoClosingSuccessAlert('Registration has been succeeded!', 3000);
                }).catch(function () {
                }).finally(function () {
                    $scope.CancelCompany();
                });
            }
            else if ($('#btnSaveCompany').val() == 'Update') {
                $scope.UpdateCompany = {
                    IDCompany: $scope.CurrentCompany.IDCompany,
                    Name_Fa: $scope.CurrentCompany.Name_Fa
                };
                CompanyService.UpdateCompany($scope.UpdateCompany).then(function (result) {
                    $scope.GetCompany();
                    AutoClosingSuccessAlert('Update has been succeeded!', 3000);
                }).catch(function () {
                }).finally(function () {
                    $scope.CancelCompany();
                });
            }
        };
        $scope.DeleteCompany = function (_IDCompany) {
            $scope.DelCompanyInfo = {
                IDCompany: _IDCompany
            };
            var ConfirmDelete = $window.confirm('Are you sure?');
            if (ConfirmDelete) {
                CompanyService.DeleteCompany($scope.DelCompanyInfo).then(function (result) {
                    $scope.GetCompany();
                    AutoClosingSuccessAlert('Delete has been succeeded!', 3000);
                }).catch(function () {
                    AutoClosingErrorAlert('Delete has been failed!', 3000);
                }).finally(function () {
                });
            }
        };
        $scope.EditCompany = function (_item) {
            $('#DivInfoCompany').slideDown();
            $('#btnNewCompany').fadeOut();
            $('#btnSaveCompany').val('Update');
            $scope.CurrentCompany.IDCompany = _item.IDCompany,
                $scope.CurrentCompany.IDParent = _item.IDParent,
                $scope.CurrentCompany.Name_Fa = _item.Name_Fa,
                $scope.CurrentCompany.Status = _item.Status,
                $('html, body').animate({
                    scrollTop: $("#DivInfoCompany").offset().top
                }, 1000);
        }
        //------------------------------------------------------------------


        //----------------Modal LevelOfPersonelInCompany --------------------
        $scope.CurrentLevelOfPersonelInCompany = {};
        $scope.AddNewLevelOfPersonelInCompany = function () {
            $('#DivInfoLevelOfPersonelInCompany').slideToggle();
            $('#btnNewLevelOfPersonelInCompany').fadeOut();
            $('#btnSaveLevelOfPersonelInCompany').val('Add');
            $('html, body').animate({
                scrollTop: $("#DivInfoLevelOfPersonelInCompany").offset().top
            }, 1000);
            $scope.CurrentLevelOfPersonelInCompany = {
            };
        }
        $scope.CancelLevelOfPersonelInCompany = function () {
            $('#DivInfoLevelOfPersonelInCompany').slideToggle();
            $('#btnNewLevelOfPersonelInCompany').fadeIn();
        }
        $scope.SaveLevelOfPersonelInCompany = function () {
            if ($('#btnSaveLevelOfPersonelInCompany').val() == 'Add') {
                $scope.NewLevelOfPersonelInCompany = {
                    IDLevelOfPersonelInCompany: NewGuid(),
                    Title: $scope.CurrentLevelOfPersonelInCompany.Title,
                    Sort: $scope.CurrentLevelOfPersonelInCompany.Sort,
                    Status: 1
                };
                LevelOfPersonelInCompanyService.AddLevelOfPersonelInCompany($scope.NewLevelOfPersonelInCompany).then(function (result) {
                    $scope.CurrentLevelOfPersonelInCompany = {}
                    $scope.GetLevelOfPersonelInCompany();
                    AutoClosingSuccessAlert('Registration has been succeeded!', 3000);
                }).catch(function () {
                }).finally(function () {
                    $scope.CancelLevelOfPersonelInCompany();
                });
            }
            else if ($('#btnSaveLevelOfPersonelInCompany').val() == 'Update') {
                $scope.UpdateLevelOfPersonelInCompany = {
                    IDLevelOfPersonelInCompany: $scope.CurrentLevelOfPersonelInCompany.IDLevelOfPersonelInCompany,
                    Title: $scope.CurrentLevelOfPersonelInCompany.Title,
                    Sort: $scope.CurrentLevelOfPersonelInCompany.Sort
                };
                LevelOfPersonelInCompanyService.UpdateLevelOfPersonelInCompany($scope.UpdateLevelOfPersonelInCompany).then(function (result) {
                    $scope.GetLevelOfPersonelInCompany();
                    AutoClosingSuccessAlert('Update has been succeeded!', 3000);
                }).catch(function () {
                }).finally(function () {
                    $scope.CancelLevelOfPersonelInCompany();
                });
            }
        };
        $scope.DeleteLevelOfPersonelInCompany = function (_IDLevelOfPersonelInCompany) {
            $scope.DelLevelOfPersonelInCompanyInfo = {
                IDLevelOfPersonelInCompany: _IDLevelOfPersonelInCompany
            };
            var ConfirmDelete = $window.confirm('Are you sure?');
            if (ConfirmDelete) {
                LevelOfPersonelInCompanyService.DeleteLevelOfPersonelInCompany($scope.DelLevelOfPersonelInCompanyInfo).then(function (result) {
                    $scope.GetLevelOfPersonelInCompany();
                    AutoClosingSuccessAlert('Delete has been succeeded!', 3000);
                }).catch(function () {
                    AutoClosingErrorAlert('Delete has been failed!', 3000);
                }).finally(function () {
                });
            }
        };
        $scope.EditLevelOfPersonelInCompany = function (_item) {
            $('#DivInfoLevelOfPersonelInCompany').slideDown();
            $('#btnNewLevelOfPersonelInCompany').fadeOut();
            $('#btnSaveLevelOfPersonelInCompany').val('Update');
            $scope.CurrentLevelOfPersonelInCompany.IDLevelOfPersonelInCompany = _item.IDLevelOfPersonelInCompany,
                $scope.CurrentLevelOfPersonelInCompany.Title = _item.Title,
                $scope.CurrentLevelOfPersonelInCompany.Sort = _item.Sort,
                $scope.CurrentLevelOfPersonelInCompany.Status = _item.Status,
                $('html, body').animate({
                    scrollTop: $("#DivInfoLevelOfPersonelInCompany").offset().top
                }, 1000);
        }
        //-------------------------------------------------------------------



        //----------------Modal OrganizationalPosition ----------------------
        $scope.CurrentOrganizationalPosition = {};
        $scope.AddNewOrganizationalPosition = function () {
            $('#DivInfoOrganizationalPosition').slideToggle();
            $('#btnNewOrganizationalPosition').fadeOut();
            $('#btnSaveOrganizationalPosition').val('Add');
            $('html, body').animate({
                scrollTop: $("#DivInfoOrganizationalPosition").offset().top
            }, 1000);
            $scope.CurrentOrganizationalPosition = {
            };
        }
        $scope.CancelOrganizationalPosition = function () {
            $('#DivInfoOrganizationalPosition').slideToggle();
            $('#btnNewOrganizationalPosition').fadeIn();
        }
        $scope.GetOrganizationalPosition = function () {
            OrganizationalPositionService.GetOrganizationalPosition().then(function (result) {
                $scope.OrganizationalPositions = result.data;
            }).catch(function () {
            }).finally(function () {
            });
        };
        $scope.SaveOrganizationalPosition = function () {
            if ($('#btnSaveOrganizationalPosition').val() == 'Add') {
                $scope.NewOrganizationalPosition = {
                    IDOrganizationPosition: NewGuid(),
                    Name_Fa: $scope.CurrentOrganizationalPosition.Name_Fa,
                    Status: 1
                };
                OrganizationalPositionService.AddOrganizationalPosition($scope.NewOrganizationalPosition).then(function (result) {
                    $scope.CurrentOrganizationalPosition = {}
                    $scope.GetOrganizationalPosition();
                    AutoClosingSuccessAlert('Registration has been succeeded!', 3000);
                }).catch(function () {
                }).finally(function () {
                    $scope.CancelOrganizationalPosition();
                });
            }
            else if ($('#btnSaveOrganizationalPosition').val() == 'Update') {
                $scope.UpdateOrganizationalPosition = {
                    IDOrganizationPosition: $scope.CurrentOrganizationalPosition.IDOrganizationPosition,
                    Name_Fa: $scope.CurrentOrganizationalPosition.Name_Fa
                };
                OrganizationalPositionService.UpdateOrganizationalPosition($scope.UpdateOrganizationalPosition).then(function (result) {
                    $scope.GetOrganizationalPosition();
                    AutoClosingSuccessAlert('Update has been succeeded!', 3000);
                }).catch(function () {
                }).finally(function () {
                    $scope.CancelOrganizationalPosition();
                });
            }
        };
        $scope.DeleteOrganizationalPosition = function (_IDOrganizationalPosition) {
            $scope.DelOrganizationalPositionInfo = {
                IDOrganizationalPosition: _IDOrganizationalPosition
            };
            var ConfirmDelete = $window.confirm('Are you sure?');
            if (ConfirmDelete) {
                OrganizationalPositionService.DeleteOrganizationalPosition($scope.DelOrganizationalPositionInfo).then(function (result) {
                    $scope.GetOrganizationalPosition();
                    AutoClosingSuccessAlert('Delete has been succeeded!', 3000);
                }).catch(function () {
                    AutoClosingErrorAlert('Delete has been failed!', 3000);
                }).finally(function () {
                });
            }
        };
        $scope.EditOrganizationalPosition = function (_item) {
            $('#DivInfoOrganizationalPosition').slideDown();
            $('#btnNewOrganizationalPosition').fadeOut();
            $('#btnSaveOrganizationalPosition').val('Update');
            $scope.CurrentOrganizationalPosition.IDOrganizationPosition = _item.IDOrganizationPosition,
                $scope.CurrentOrganizationalPosition.Name_Fa = _item.Name_Fa,
                $scope.CurrentOrganizationalPosition.Status = _item.Status,
                $scope.CurrentOrganizationalPosition.ParentID = _item.ParentID,
                $('html, body').animate({
                    scrollTop: $("#DivInfoOrganizationalPosition").offset().top
                }, 1000);
        }
        //-----------------------------------------------------------------

        //------------------------Modal Contact Way-------------------------
        $scope.CurrentContactWay = {};
        $scope.CreateNewContactWay = function () {
            $('#DivInfoContactWay').slideToggle();
            $('#btnNewContactWay').fadeOut();
            $('#btnSaveContactWay').val('Add');
            $('html, body').animate({
                scrollTop: $("#DivInfoContactWay").offset().top
            }, 1000);
            $scope.CurrentContactWay = {
            };
        }
        $scope.CancelContactWay = function () {
            $('#DivInfoContactWay').slideToggle();
            $('#btnNewContactWay').fadeIn();
        }
        $scope.GetContactWay_ByIDUser = function (_IDUser) {
            $scope.objContactWay = {
                IDUser: _IDUser
            };
            ContactWayService.GetContactWay_ByIDUser($scope.objContactWay).then(function (result) {
                $scope.ContactWays = result.data;
            }).catch(function () {
            }).finally(function () {
            });
        };
        $scope.GetContactWayType = function () {
            ContactWayTypeService.GetContactWayType().then(function (result) {
                $scope.ContactWayTypes = result.data;
            }).catch(function () {
            }).finally(function () {
            });
        };
        $scope.SaveContactWay = function () {
            if ($('#btnSaveContactWay').val() == 'Add') {
                $scope.NewContactWay = {
                    IDContactWay: NewGuid(),
                    IDContactWayType: $scope.CurrentContactWay.IDContactWayType,
                    IDUser: $scope.CurrentPersonelInCompany.IDPersonelInCompany,
                    Input: $scope.CurrentContactWay.Input,
                    InternalInput: $scope.CurrentContactWay.InternalInput,
                    Status: 1,
                };
                ContactWayService.AddContactWay($scope.NewContactWay).then(function (result) {
                    $scope.CurrentContactWay = {}
                    $scope.GetContactWay_ByIDUser($scope.CurrentPersonelInCompany.IDPersonelInCompany);
                    AutoClosingSuccessAlert('Registration has been succeeded!', 3000);
                }).catch(function () {
                }).finally(function () {
                    $scope.CancelContactWay();
                });
            }
            else if ($('#btnSaveContactWay').val() == 'Update') {
                $scope.UpdateContactWay = {
                    IDContactWay: $scope.CurrentContactWay.IDContactWay,
                    IDContactWayType: $scope.CurrentContactWay.IDContactWayType,
                    IDUser: $scope.CurrentContactWay.IDUser,
                    Input: $scope.CurrentContactWay.Input,
                    InternalInput: $scope.CurrentContactWay.InternalInput
                };
                ContactWayService.UpdateContactWay($scope.UpdateContactWay).then(function (result) {
                    $scope.GetContactWay_ByIDUser($scope.CurrentPersonelInCompany.IDPersonelInCompany);
                    AutoClosingSuccessAlert('Update has been succeeded!', 3000);
                }).catch(function () {
                    AutoClosingErrorAlert('Update has been failed!', 3000);
                }).finally(function () {
                    $scope.CancelContactWay();
                });
            }
        };
        $scope.DeleteContactWay = function (_IDContactWay, _IDUser) {
            $scope.DelContactWayInfo = {
                IDContactWay: _IDContactWay
            };
            var ConfirmDelete = $window.confirm('Are you sure?');
            if (ConfirmDelete) {
                ContactWayService.DeleteContactWay($scope.DelContactWayInfo).then(function (result) {
                    $scope.GetContactWay_ByIDUser($scope.CurrentPersonelInCompany.IDPersonelInCompany);
                    AutoClosingSuccessAlert('Delete has been succeeded!', 3000);
                }).catch(function () {
                    AutoClosingErrorAlert('Delete has been failed!', 3000);
                }).finally(function () {
                });
            }
        };
        $scope.EditContactWay = function (_item) {
            $('#DivInfoContactWay').slideDown();
            $('#btnNewContactWay').fadeOut();
            $('#btnSaveContactWay').val('Update');
            $scope.CurrentContactWay.IDContactWay = _item.IDContactWay,
                $scope.CurrentContactWay.IDContactWayType = _item.IDContactWayType,
                $scope.CurrentContactWay.IDUser = _item.IDUser,
                $scope.CurrentContactWay.Input = _item.Input,
                $scope.CurrentContactWay.InternalInput = _item.InternalInput,
                $scope.CurrentContactWay.Status = _item.Status,
                $('html, body').animate({
                    scrollTop: $("#DivInfoContactWay").offset().top
                }, 1000);
        }
        $scope.SelectMainItemByType = function (_item, xx) {
            angular.forEach($scope.ContactWays, function (row, index) {
                if (row.ContactWayTypeName_Fa == _item.ContactWayTypeName_Fa && row.IDContactWay == _item.IDContactWay) {
                    $("#chkSelectMainContactWay" + index).prop('checked', true);
                    $scope.param = {
                        IDContactWay: _item.IDContactWay,
                        IDContactWayType: _item.IDContactWayType,
                        IDUser: _item.IDUser,
                        ContactWayTypeName_Fa: _item.ContactWayTypeName_Fa,
                        Input: _item.Input
                    }
                    ContactWayService.UpdateMainContactWayAndUser($scope.param).then(function (result) {
                    }).catch(function () {
                        AutoClosingErrorAlert('update has been failed!', 3000);
                    }).finally(function () {
                    });

                }
                else if (row.ContactWayTypeName_Fa == _item.ContactWayTypeName_Fa && row.IDContactWay != _item.IDContactWay) {
                    $("#chkSelectMainContactWay" + index).prop('checked', false);
                }

            });

        }

        //----------------------------------------------------------------------        

        $scope.GetCompany();
        $scope.GetOrganizationalPosition();
        $scope.GetLevelOfPersonelInCompany();
        $scope.GetPersonelInCompany();
        $scope.UserGroupForSale();

    }]);


