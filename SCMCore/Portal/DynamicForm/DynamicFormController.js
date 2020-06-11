myApp.controller('DynamicFormController', ['$scope', '$compile', '$window', 'ngProgress', 'DynamicFormService', 'FormQuestionTypeService',
    'FormQuestionService', 'FormOptionService', 'IncidentService',
    function ($scope, $compile, $window, ngProgress, DynamicFormService, FormQuestionTypeService, FormQuestionService, FormOptionService, IncidentService) {
        $scope.CurrentDynamicForm = {};
        $scope.CurrentFormQuestion = {};
        $scope.CurrentFormOption = {};
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
        //---------------------------------------------------------------------------------

        //---------------------------------------------------------------------------------
        $scope.NewDynamicForm = function () {
            $('#DivInfoDynamicForm').slideToggle();
            $('#btnNewDynamicForm').fadeOut();
            $('#btnSaveDynamicForm').val('Add');
            $('html, body').animate({
                scrollTop: $("#DivInfoDynamicForm").offset().top
            }, 1000);
            $scope.CurrentDynamicForm = {
            };
        }
        $scope.CancelDynamicForm = function () {
            $('#DivInfoDynamicForm').slideToggle();
            $('#btnNewDynamicForm').fadeIn();
        }
        $scope.GetDynamicForm = function () {
            DynamicFormService.GetDynamicForm().then(function (result) {
                $scope.DynamicForms = result.data;
            }).catch(function () {
            }).finally(function () {
            });
        };
        $scope.SaveDynamicForm = function () {
            if ($('#btnSaveDynamicForm').val() == 'Add') {
                $scope.NewDynamicForm = {
                    Name_Fa: $scope.CurrentDynamicForm.Name_Fa,
                    Description: $scope.CurrentDynamicForm.Description
                };
                DynamicFormService.AddDynamicForm($scope.NewDynamicForm).then(function (result) {
                    $scope.CurrentDynamicForm = {}
                    $scope.GetDynamicForm();
                    AutoClosingSuccessAlert('Registration has been succeeded!', 3000);
                }).catch(function () {
                    AutoClosingErrorAlert('Registration has been failed!', 3000);
                }).finally(function () {
                    $scope.CancelDynamicForm();
                });
            }
            else if ($('#btnSaveDynamicForm').val() == 'Update') {
                $scope.UpdateDynamicForm = {
                    IDDynamicForm: $scope.CurrentDynamicForm.IDDynamicForm,
                    Name_Fa: $scope.CurrentDynamicForm.Name_Fa,
                    Description: $scope.CurrentDynamicForm.Description
                };
                DynamicFormService.UpdateDynamicForm($scope.UpdateDynamicForm).then(function (result) {
                    $scope.GetDynamicForm();
                    AutoClosingSuccessAlert('Update has been succeeded!', 3000);
                }).catch(function () {
                    AutoClosingErrorAlert('Update has been failed!', 3000);
                }).finally(function () {
                    $scope.CancelDynamicForm();
                });
            }
        };
        $scope.DeleteDynamicForm = function (_IDDynamicForm) {
            $scope.DelDynamicFormInfo = {
                IDDynamicForm: _IDDynamicForm
            };
            var ConfirmDelete = $window.confirm('Are you sure?');
            if (ConfirmDelete) {
                DynamicFormService.DeleteDynamicForm($scope.DelDynamicFormInfo).then(function (result) {
                    $scope.GetDynamicForm();
                    AutoClosingSuccessAlert('Delete has been succeeded!', 3000);
                }).catch(function () {
                    AutoClosingErrorAlert('Delete has been failed!', 3000);
                }).finally(function () {
                });
            }
        };
        $scope.EditDynamicForm = function (_item) {
            $('#DivInfoDynamicForm').slideDown();
            $('#btnNewDynamicForm').fadeOut();
            $('#btnSaveDynamicForm').val('Update');
            $scope.CurrentDynamicForm.IDDynamicForm = _item.IDDynamicForm;
            $scope.CurrentDynamicForm.Name_Fa = _item.Name_Fa;
            $scope.CurrentDynamicForm.Description = _item.Description;
            $('html, body').animate({
                scrollTop: $("#DivInfoDynamicForm").offset().top
            }, 1000);
        }
        //---------------------------------------------------------------------------------

        //---------------------------------------------------------------------------------
        $scope.OpenFormQuestionModal = function (_item) {
            $('#ModalFormQuestion').modal('show');
            $scope.GetFormQuestionDataByIDDynamicForm(_item.IDDynamicForm);
            $scope.SelectedIDDynamicForm = _item.IDDynamicForm;
            $scope.GetFormQuestionType();
        }
        $scope.NewFormQuestion = function () {
            $('#DivInfoFormQuestion').slideToggle();
            $('#btnNewFormQuestion').fadeOut();
            $('#btnSaveFormQuestion').val('Add');
            $scope.CurrentFormQuestion = {};
        }
        $scope.CancelFormQuestion = function () {
            $('#DivInfoFormQuestion').slideToggle();
            $('#btnNewFormQuestion').fadeIn();
        }
        $scope.GetFormQuestionDataByIDDynamicForm = function (_IDDynamicForm) {
            var Param = {
                IDDynamicForm: _IDDynamicForm
            }
            FormQuestionService.GetFormQuestionDataByIDDynamicForm(Param).then(function (result) {
                $scope.FormQuestions = result.data;
            }).catch(function () {
            }).finally(function () {
            });
        };
        $scope.SaveFormQuestion = function () {
            if ($('#btnSaveFormQuestion').val() == 'Add') {
                $scope.ObjNewFormQuestion = {
                    IDDynamicForm: $scope.SelectedIDDynamicForm,
                    IDFormQuestionType: $scope.CurrentFormQuestion.IDFormQuestionType,
                    QuestionText: $scope.CurrentFormQuestion.QuestionText,
                    Optional: $scope.CurrentFormQuestion.Optional
                };
                FormQuestionService.AddFormQuestion($scope.ObjNewFormQuestion).then(function (result) {
                    $scope.CurrentFormQuestion = {}
                    $scope.GetFormQuestionDataByIDDynamicForm($scope.SelectedIDDynamicForm);
                    AutoClosingSuccessAlert('Registration has been succeeded!', 3000);
                }).catch(function () {
                    AutoClosingErrorAlert('Registration has been failed!', 3000);
                }).finally(function () {
                    $scope.CancelFormQuestion();
                });
            }
            else if ($('#btnSaveFormQuestion').val() == 'Update') {
                $scope.UpdateFormQuestion = {
                    IDFormQuestion: $scope.CurrentFormQuestion.IDFormQuestion,
                    IDDynamicForm: $scope.CurrentFormQuestion.IDDynamicForm,
                    IDFormQuestionType: $scope.CurrentFormQuestion.IDFormQuestionType,
                    QuestionText: $scope.CurrentFormQuestion.QuestionText,
                    Optional: $scope.CurrentFormQuestion.Optional
                };
                FormQuestionService.UpdateFormQuestion($scope.UpdateFormQuestion).then(function (result) {
                    $scope.GetFormQuestionDataByIDDynamicForm($scope.SelectedIDDynamicForm);
                    AutoClosingSuccessAlert('Update has been succeeded!', 3000);
                }).catch(function () {
                    AutoClosingErrorAlert('Update has been failed!', 3000);
                }).finally(function () {
                    $scope.CancelFormQuestion();
                });
            }
        };
        $scope.DeleteFormQuestion = function (_IDFormQuestion) {
            $scope.DelFormQuestionInfo = {
                IDFormQuestion: _IDFormQuestion
            };
            var ConfirmDelete = $window.confirm('Are you sure?');
            if (ConfirmDelete) {
                FormQuestionService.DeleteFormQuestion($scope.DelFormQuestionInfo).then(function (result) {
                    $scope.GetFormQuestionDataByIDDynamicForm($scope.SelectedIDDynamicForm);
                    AutoClosingSuccessAlert('Delete has been succeeded!', 3000);
                }).catch(function () {
                    AutoClosingErrorAlert('Delete has been failed!', 3000);
                }).finally(function () {
                });
            }
        };
        $scope.EditFormQuestion = function (_item) {
            $('#DivInfoFormQuestion').slideDown();
            $('#btnNewFormQuestion').fadeOut();
            $('#btnSaveFormQuestion').val('Update');
            $scope.CurrentFormQuestion.IDFormQuestion = _item.IDFormQuestion;
            $scope.CurrentFormQuestion.IDDynamicForm = _item.IDDynamicForm;
            $scope.CurrentFormQuestion.IDFormQuestionType = _item.IDFormQuestionType;
            $scope.CurrentFormQuestion.QuestionText = _item.QuestionText;
            $scope.CurrentFormQuestion.Optional = _item.Optional;
        }
        $scope.GetFormQuestionType = function () {
            FormQuestionTypeService.GetFormQuestionType().then(function (result) {
                $scope.FormQuestionTypes = result.data;
            }).catch(function () {
            }).finally(function () {
            });
        };
        $scope.SortTableSelectedFormQuestion = {
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
                    var objFormQuestion = {
                        JsonFormQuestion: myJsonString
                    }
                    FormQuestionService.ChangeSortQuestions(objFormQuestion).then(function (result) {

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
        //--------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------
        $scope.OpenFormOptionModal = function (_item) {
            $('#ModalFormOption').modal('show');
            $scope.GetFormOptionDataByIDFormQuestion(_item.IDFormQuestion);
            $scope.SelectedIDFormQuestion = _item.IDFormQuestion;
            $scope.SelectedFormQuestionType = _item.UniqueName;
        }
        $scope.NewFormOption = function () {
            $('#DivInfoFormOption').slideToggle();
            $('#btnNewFormOption').fadeOut();
            $('#btnSaveFormOption').val('Add');
            $('#PreviewFormOptionImage').attr({ 'src': '' });
            $scope.CurrentFormOption = {
            };
            document.getElementById("urlNameFormOptionPic").value = "";
        }
        $scope.CancelFormOption = function () {
            $('#DivInfoFormOption').slideToggle();
            $('#btnNewFormOption').fadeIn();
        }
        $scope.GetFormOptionDataByIDFormQuestion = function (_IDFormQuestion) {
            var Param = {
                IDFormQuestion: _IDFormQuestion
            }
            FormOptionService.GetFormOptionDataByIDFormQuestion(Param).then(function (result) {
                $scope.FormOptions = result.data;
            }).catch(function () {
            }).finally(function () {
            });
        };
        $scope.SaveFormOption = function () {
            if ($('#btnSaveFormOption').val() == 'Add') {
                $scope.ObjNewFormOption = {
                    IDFormQuestion: $scope.SelectedIDFormQuestion,
                    OptionText: $scope.CurrentFormOption.OptionText,
                    PicFile: $scope.CurrentFormOption.PicFile
                };
                FormOptionService.AddFormOption($scope.ObjNewFormOption).then(function (result) {
                    $scope.CurrentFormOption = {}
                    $scope.GetFormOptionDataByIDFormQuestion($scope.SelectedIDFormQuestion);
                    AutoClosingSuccessAlert('Registration has been succeeded!', 3000);
                }).catch(function () {
                    AutoClosingErrorAlert('Registration has been failed!', 3000);
                }).finally(function () {
                    $scope.CancelFormOption();
                });
            }
            else if ($('#btnSaveFormOption').val() == 'Update') {
                $scope.UpdateFormOption = {
                    IDFormOption: $scope.CurrentFormOption.IDFormOption,
                    IDFormQuestion: $scope.SelectedIDFormQuestion,
                    OptionText: $scope.CurrentFormOption.OptionText,
                    PicFile: $scope.CurrentFormOption.PicFile
                };
                FormOptionService.UpdateFormOption($scope.UpdateFormOption).then(function (result) {
                    $scope.GetFormOptionDataByIDFormQuestion($scope.SelectedIDFormQuestion);
                    AutoClosingSuccessAlert('Update has been succeeded!', 3000);
                }).catch(function () {
                    AutoClosingErrorAlert('Update has been failed!', 3000);
                }).finally(function () {
                    $scope.CancelFormOption();
                });
            }
        };
        $scope.DeleteFormOption = function (_IDFormOption) {
            $scope.DelFormOptionInfo = {
                IDFormOption: _IDFormOption
            };
            var ConfirmDelete = $window.confirm('Are you sure?');
            if (ConfirmDelete) {
                FormOptionService.DeleteFormOption($scope.DelFormOptionInfo).then(function (result) {
                    $scope.GetFormOptionDataByIDFormQuestion($scope.SelectedIDFormQuestion);
                    AutoClosingSuccessAlert('Delete has been succeeded!', 3000);
                }).catch(function () {
                    AutoClosingErrorAlert('Delete has been failed!', 3000);
                }).finally(function () {
                });
            }
        };
        $scope.EditFormOption = function (_item) {
            $('#DivInfoFormOption').slideDown();
            $('#btnNewFormOption').fadeOut();
            $('#btnSaveFormOption').val('Update');
            $scope.CurrentFormOption.IDFormOption = _item.IDFormOption;
            $scope.CurrentFormOption.IDFormQuestion = _item.IDFormQuestion;
            $scope.CurrentFormOption.OptionText = _item.OptionText;
            $scope.CurrentFormOption.PicUrl = _item.PicUrl;
            $scope.CurrentFormOption.Sort = _item.Sort;
            document.getElementById("urlNameFormOptionPic").value = $scope.CurrentFormOption.PicUrl;
            $('#PreviewFormOptionImage').attr({
                'src': '\\' + $scope.CurrentFormOption.PicUrl
            });
            $scope.CurrentFormOption.PicFile = {};
        }
        $scope.ChangeFormOptionFile = function (element) {
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
            document.getElementById("urlNameFormOptionPic").value = element.files[0].name;

            var preview = document.querySelector('#PreviewFormOptionImage');
            var file = document.querySelector('#FormOptionPicFileUpload').files[0];
            var reader = new FileReader();

            reader.addEventListener("load", function () {
                preview.src = reader.result;
                $scope.CurrentFormOption.PicFile = reader.result;
            }, false);

            if (file) {
                reader.readAsDataURL(file);
            }
        }

        $scope.SortTableSelectedFormOption = {
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
                    var objFormOption = {
                        JsonFormOption: myJsonString
                    }
                    FormOptionService.ChangeSortOptions(objFormOption).then(function (result) {

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
        //--------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------
        $scope.OpenRelations = function (_item) {
            $scope.RelationModalTitle = _item.Name_Fa;
            $('#ModalDynamicFormRelation').modal('show');
        };
        $scope.LoadRelations = function (_item) {
            //switch (_item) {
            //    case 'Incident': {
            //        IncidentService.GetIncidentAll().then(function (result) {
            //            $scope.Incidets = result.data;
            //        }).catch(function () {
            //        }).finally(function () {
            //        });

            //       // IncidentService.GetIncidentAll
            //    }
            //        break;
            //    case 'TrainingCourse': {
            //        alert();
            //    }
            //        break;
            //    case 'Content': {
            //        alert();
            //    }
            //        break;
            //    default:
            //}
            IncidentService.GetIncidentAll().then(function (result) {
                $scope.Incidents = result.data;
            }).catch(function () {
            }).finally(function () {
            });
        };

        //--------------------------------------------------------------------------------

        $scope.GetDynamicForm();
    }]);




