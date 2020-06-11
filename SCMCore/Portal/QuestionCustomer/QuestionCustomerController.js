myApp.controller('QuestionCustomerController', ['$scope', 'ngProgress', 'QuestionCustomerService', 'OptionCustomerService', 'UserGroupService', 'CombinationOptionsService', 'OptionInheritanceService', function ($scope, ngProgress, QuestionCustomerService, OptionCustomerService, UserGroupService, CombinationOptionsService, OptionInheritanceService) {
    $scope.CurrentQuestionsCustomer = {};
    $scope.GetCountCombinationOptions = function () {
        CombinationOptionsService.GetCountCombinationOptions().then(function (result) {
            $scope.COUNTIDCombinationOptions = result.data[0].COUNTIDCombinationOptions;
            if (result.data[0].COUNTIDCombinationOptions == 0) {

                $scope.LetSorting = true;

            }
            else {

                $scope.LetSorting = false;

            }

            $scope.SortTableQuestionCustomer = {
                pull: true,
                put: true,
                sort: $scope.LetSorting,
                animation: 150,
                accept: function (sourceItemHandleScope, destSortableScope) {

                },
                onStart: function (evt) {

                },
                onEnd: function (evt) {
                    if (evt.oldIndex != evt.newIndex) {
                        var param = {
                            JsonQuestionCustomer: JSON.stringify(evt.models)
                        }

                        QuestionCustomerService.ChangeSortInQuestionCustomer(param).then(function (result) {
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
        }).catch(function () {
            AutoClosingErrorAlert('Error in catch data !', 5000);
        }).finally(function () {
        });
    }
    $scope.GetCountCombinationOptions();

    $scope.GetOptionInheritance = function () {
        OptionInheritanceService.GetOptionInheritance().then(function (result) {
            $scope.OptionInheritances = result.data;
        }).catch(function () {
        }).finally(function () {
        });
    }
    $scope.GetOptionInheritance();

    $scope.GetQuestionCustomer = function () {
        ngProgress.start();
        QuestionCustomerService.GetQuestionCustomer().then(function (result) {
            $scope.QuestionCustomers = result.data;
        }).catch(function () {
            AutoClosingErrorAlert('Error in catch data !', 5000);
        }).finally(function () {
            ngProgress.complete();
            ngProgress.stop();
        });
    };
    $scope.GetQuestionCustomer();

    $scope.NewQuestionCustomer = function () {
        $scope.CurrentQuestionsCustomer.Question_Fa = "";
        $scope.CurrentQuestionsCustomer.ShortTitle = "";
        document.getElementById("btnAddQuestionCustomer").value = "Add";
    }
    $scope.CancelQuestionCustomer = function () {
        $scope.NewQuestionCustomer();
    }

    $scope.AddQuestionCustomer = function () {
        if (document.getElementById("btnAddQuestionCustomer").value == "Add") {
            $scope.CurrentQuestionsCustomer.IDQuestionCustomer = NewGuid();
            QuestionCustomerService.AddQuestionCustomer($scope.CurrentQuestionsCustomer).then(function (result) {
                $scope.NewQuestionCustomer();
                $scope.GetQuestionCustomer();
                AutoClosingSuccessAlert('Registration was successful !', 5000);
            }).catch(function () {
                AutoClosingErrorAlert('Error in registration !', 5000);

            }).finally(function () {
                ngProgress.complete();
            });
        }
        else if (document.getElementById("btnAddQuestionCustomer").value == "Update") {
            QuestionCustomerService.UpdateQuestionCustomer($scope.CurrentQuestionsCustomer).then(function (result) {
                $scope.NewQuestionCustomer();
                $scope.GetQuestionCustomer();
                AutoClosingSuccessAlert('Update was successful !', 5000);
            }).catch(function () {
                AutoClosingErrorAlert('Error in Update !', 5000);
            }).finally(function () {
                ngProgress.complete();
            });
        }
    }

    $scope.DeleteQuestionCustomer = function (_Item) {
        var Accept = confirm("Are you sure ?");
        if (Accept == true) {
            $scope.Param = {
                IDQuestionCustomer: _Item.IDQuestionCustomer
            };
            QuestionCustomerService.DeleteQuestionCustomer($scope.Param).then(function (result) {
                $scope.NewQuestionCustomer();
                $scope.GetQuestionCustomer();
                AutoClosingSuccessAlert('Delete was successful !', 5000);
            }).catch(function () {
                AutoClosingErrorAlert('Error in Delete !', 5000);

            }).finally(function () {
                ngProgress.complete();
            });

        }
    }
    $scope.EditQuestionCustomer = function (_Item) {
        $scope.CurrentQuestionsCustomer.IDQuestionCustomer = _Item.IDQuestionCustomer;
        $scope.CurrentQuestionsCustomer.Question_Fa = _Item.Question_Fa;
        $scope.CurrentQuestionsCustomer.ShortTitle = _Item.ShortTitle;
        document.getElementById("btnAddQuestionCustomer").value = "Update";
    }
    $scope.UpdateUseInCombination = function (_UseInCombination, _IDQuestionCustomer) {
        $scope.Param = {
            UseInCombination: _UseInCombination,
            IDQuestionCustomer: _IDQuestionCustomer
        }
        QuestionCustomerService.UpdateUseInCombination($scope.Param).then(function (result) {
            $scope.GetQuestionCustomer();
        }).catch(function () {
            AutoClosingErrorAlert('Error in Update  !', 5000);

        }).finally(function () {
        });
    }

    $scope.UpdateUseInSignUp = function (_UseInSignUp, _IDQuestionCustomer) {
        $scope.Param = {
            UseInSignUp: _UseInSignUp,
            IDQuestionCustomer: _IDQuestionCustomer
        }
        QuestionCustomerService.UpdateUseInSignUp($scope.Param).then(function (result) {
        }).catch(function () {
            AutoClosingErrorAlert('Error in Update  !', 5000);

        }).finally(function () {
        });
    }

    $scope.UpdateUseInMaterialDetail = function (_UseInMaterialDetail, _IDQuestionCustomer) {
        $scope.Param = {
            UseInMaterialDetail: _UseInMaterialDetail,
            IDQuestionCustomer: _IDQuestionCustomer
        }
        QuestionCustomerService.UpdateUseInMaterialDetail($scope.Param).then(function (result) {
        }).catch(function () {
            AutoClosingErrorAlert('Error in Update  !', 5000);

        }).finally(function () {
        });
    }


    $scope.CurrentOptionCustomer = {};

    $scope.ShowInputOptionForAdd = function (_Item) {
        _Item.ShowInputOption = true;
        _Item.ShowOptionsInTree = true;
    }
    $scope.CancelOptionCustomer = function (_Item) {
        _Item.ShowInputOption = false;
        $scope.CurrentOptionCustomer.Option_Fa = "";
    }
    $scope.AddOptionCustomer = function (_Item) {
        $scope.CurrentOptionCustomer.IDOptionCustomer = NewGuid();
        $scope.CurrentOptionCustomer.IDQuestionCustomer = _Item.IDQuestionCustomer;
        OptionCustomerService.AddOptionCustomer($scope.CurrentOptionCustomer).then(function (result) {
            $scope.CopyScopePushForOptionCustomer = angular.copy($scope.CurrentOptionCustomer);
            if (_Item.OptionCustomer == undefined) {
                _Item.OptionCustomer = [];
                _Item.OptionCustomer.push($scope.CopyScopePushForOptionCustomer);

            }
            else {
                _Item.OptionCustomer.push($scope.CopyScopePushForOptionCustomer);
            }

            $scope.CurrentOptionCustomer.Option_Fa = "";
            AutoClosingSuccessAlert('Registration was successful !', 5000);
        }).catch(function () {
            AutoClosingErrorAlert('Error in registration !', 5000);

        }).finally(function () {
            ngProgress.complete();
        });
    }
    $scope.DeleteOptionCustomer = function (_Opt, _OptionIndex, _item) {
        var Accept = confirm("Are you sure ?");
        if (Accept == true) {
            $scope.Param = {
                IDOptionCustomer: _Opt.IDOptionCustomer
            };
            OptionCustomerService.DeleteOptionCustomer($scope.Param).then(function (result) {
                _item.OptionCustomer.splice(_OptionIndex, 1);

                AutoClosingSuccessAlert('Delete was successful !', 5000);
            }).catch(function () {
                AutoClosingErrorAlert('Error in Delete !', 5000);

            }).finally(function () {
                ngProgress.complete();
            });

        }
    }
    $scope.SortTableOptionCustomer = {
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
                var param = {
                    JsonOptionCustomer: JSON.stringify(evt.models)
                }

                OptionCustomerService.ChangeSortInOptionCustomer(param).then(function (result) {
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


    $scope.ShowModalCustomCompositions = function () {
        QuestionCustomerService.CheckSortSequently().then(function (result) {
            if (result.data[0].result == 'Error') {

                AutoClosingErrorAlert('Error in order of sorts.Items which selected should place at first !', 5000);
            }
            else if (result.data[0].result == 'Correct') {
                $('#ModalCustomCompositions').modal('show');
                $scope.GetUserSales();
                $('#CustomCompositionsTreeForSeller').hide();

            }
        }).catch(function () {
            AutoClosingErrorAlert('Error in Delete !', 5000);
        }).finally(function () {
        });


    }
    $scope.DeleteAllCombination = function () {
        var Accept = confirm("Are you sure to delete all combination ?");
        if (Accept == true) {

            CombinationOptionsService.DeleteAllCombination().then(function (result) {
                AutoClosingSuccessAlert('Delete all combination was successful !', 5000);
                $scope.GetCountCombinationOptions();
                $scope.GetQuestionCustomer();
            }).catch(function () {
                AutoClosingErrorAlert('Error in Delete !', 5000);

            }).finally(function () {

            });

        }

    }
    $scope.ToggleCollapse = function (_Item) {
        if (_Item.ShowSub) {
            _Item.ShowSub = false;
        }
        else {
            _Item.ShowSub = true;
        }
    }
    $scope.MouseOverNode = function (_Item) {
        _Item.ShowNavigation = true;
    };
    $scope.MouseLeaveNode = function (_Item) {
        _Item.ShowNavigation = false;
    };
    $scope.MouseOverRoot = function () {
        $scope.ShowNavigationForRoot = true;

    }
    $scope.MouseLeaveRoot = function () {
        $scope.ShowNavigationForRoot = false;
    }


    $scope.ShowModalOptionsCustomerForNextLevel = function (_Item) {
        $('#ModalOptionsCustomerForNextLevel').modal('show');
        $scope.CheckOption = false;
        $scope.CompareJsons = [];
        $scope.SelectNode = _Item;
        var param = {
            IDSeller: $scope.SellerSelect,
            IDOptionInheritance: _Item.IDOptionInheritance
        }
        CombinationOptionsService.GetRestOtItemsNotExistForOtherLevel(param).then(function (result) {
            $scope.CompareJsons = result.data;
        }).catch(function () {
        }).finally(function () {
        });

    }
    $scope.ShowModalOptionsCustomerForRoot = function () {

        $scope.CheckOption = false;
        $scope.CompareJsons = [];
        $scope.SelectNode = undefined;

        var param = {
            IDSeller: $scope.SellerSelect
        }
        CombinationOptionsService.GetRestOtItemsNotExistInRoot(param).then(function (result) {
            $scope.CompareJsons = result.data;
        }).catch(function () {
        }).finally(function () {
        });
        $('#ModalOptionsCustomerForNextLevel').modal('show');
        $scope.HeaderInModalOptionsCustomerForNextLevel = "Start";


    }


    $scope.removeFromTree = function (source, IDValue, ChildName) {

        for (key in source) {
            var item = source[key];
            if (item['IDOptionInheritance'] == IDValue) {
                source.splice(key, 1);
            }
            else {
                if (source[key] != undefined && source[key].hasOwnProperty(ChildName)) {
                    $scope.removeFromTree(source[key][ChildName], IDValue, ChildName);

                }
            }


        }
        return source;

    }

    $scope.GetUserSales = function () {
        var objGroup = {
            UnicName: 'sale'
        };
        UserGroupService.GetUserGroupByGroupType(objGroup).then(function (result) {
            $scope.SaleUsers = result.data;
        }).catch(function () {
        }).finally(function () {
        });
    }



    $scope.SelectItemSeller = function (_item) {
        $scope.SellerSelect = _item.IDUser;
        $scope.InitialOptionInheritance($scope.SellerSelect);
        $scope.GetCombinationOptionsByIDSeller($scope.SellerSelect);
        $scope.GetCountCombinationOptions();
        $scope.GetQuestionCustomer();


    }
    $scope.InitialOptionInheritance = function (_IDSeller) {
        var param = {
            IDSeller: _IDSeller
        }
        OptionInheritanceService.InitialOptionInheritance(param).then(function (result) {
            $scope.GetCombinationOptionsByIDSeller($scope.SellerSelect);
            $scope.GetCountCombinationOptions();
            ngProgress.stop();
            $scope.GetQuestionCustomer();
        }).catch(function () {
        }).finally(function () {
            ngProgress.stop();
        });

    }
    $scope.GetCombinationOptionsByIDSeller = function (_IDSeller) {

        var param = {
            IDSeller: _IDSeller
        }
        CombinationOptionsService.GetCombinationOptionsByIDSeller(param).then(function (result) {
            $scope.CombinationOptions = result.data;
            $('#CustomCompositionsTreeForSeller').show();
        }).catch(function () {
            AutoClosingErrorAlert('Error in catch data !', 5000);
        }).finally(function () {

        });

    }

    $scope.AddSubChilds = function (_Item) {

        if ($scope.SelectNode == undefined) {
            var param = {
                IDSeller: $scope.SellerSelect,
                IDOptionCustomer: _Item.IDOptionCustomer

            }
            CombinationOptionsService.AddCombinationOptionsForRoot(param).then(function (result) {
                $scope.GetCombinationOptionsByIDSeller($scope.SellerSelect);

            }).catch(function () {
                AutoClosingErrorAlert('Error in Add data !', 5000);
            }).finally(function () {

            });
        }
        else {
            var param = {
                IDSeller: $scope.SellerSelect,
                IDOptionInheritance: _Item.IDOptionInheritance
            }
            CombinationOptionsService.AddCombinationOptionsForNextLevels(param).then(function (result) {
                var param2 = {
                    IDOptionInheritance: _Item.IDOptionInheritance
                }
                CombinationOptionsService.GetRestOtItemsNotExistInRootByIDOptionInheritance(param2).then(function (result) {
                    $scope.ItmesShouldPush = result.data[0];
                    var xx = findById($scope.CombinationOptions, $scope.SelectNode.IDOptionInheritance, 'IDOptionInheritance')
                    xx.SubChild.push($scope.ItmesShouldPush);

                }).catch(function () {
                    AutoClosingErrorAlert('Error in catch data !', 5000);
                }).finally(function () {

                });

            }).catch(function () {
                AutoClosingErrorAlert('Error in Add data !', 5000);
            }).finally(function () {

            });
        }
    }
    $scope.DeleteFromCompositionTree = function (_Item) {
        var Accept = confirm("Are you sure ?");
        if (Accept == true) {
            ngProgress.start();
            var param = {
                IDCombinationOptions: _Item.IDCombinationOptions,
                IDSeller: $scope.SellerSelect
            };
            CombinationOptionsService.DeleteCombinationOptionsBYIDCombinationOptions(param).then(function (result) {
                $scope.CombinationOptions = $scope.removeFromTree($scope.CombinationOptions, _Item.IDOptionInheritance, 'SubChild');
            }).catch(function () {
                AutoClosingErrorAlert('Error in Delete !', 5000);
            }).finally(function () {
                ngProgress.complete();
                ngProgress.stop();
            });

        }

    };

    $scope.MouseOverQuestion = function (_Item) {
        angular.forEach($scope.QuestionCustomers, function (rp) {
            if (rp.IDQuestionCustomer != _Item.IDQuestionCustomer) {
                rp.ShowNavigationForQuestion = false;

            }

        });

        if (_Item.ShowNavigationForQuestion == true) {
            _Item.ShowNavigationForQuestion = false
        }
        else {
            _Item.ShowNavigationForQuestion = true;
        }

    }

    $scope.MouseOverOption = function (_Opt) {
        _Opt.ShowNavigationForOption = true;

    }
    $scope.MouseLeaveOption = function (_Opt) {
        _Opt.ShowNavigationForOption = false;
    }
    $scope.CollapseIconQuestion = function (_item) {
        if (_item.ShowOptionsInTree) {
            _item.ShowOptionsInTree = false;
        }
        else {
            _item.ShowOptionsInTree = true;
        }
    }
    $scope.ShowModalSortOptionList = function (_item) {
        $('#ModalSortOptionCustomer').modal('show');
        $scope.OptionListForSort = _item.OptionCustomer;
        $scope.QuestionTitleInModalSortOption = _item.Question_Fa;
    }
    $scope.ShowModalMoveQuestions = function (_item) {
        $('#ModalMoveQuestions').modal('show');
        $scope.CopyScopeQuestionCustomerForMove = angular.copy($scope.QuestionCustomers);
        $scope.SelectedQuestionForMove = _item;

    }
    $scope.SetQuestionAsChildOfOption = function (_opt) {

        var Accept = confirm("Are you sure to set ( " + $scope.SelectedQuestionForMove.Question_Fa + " ) As child of ( " + _opt.Option_Fa + " ) ?");
        if (Accept == true) {
            if (_opt.IDQuestionCustomer == $scope.SelectedQuestionForMove.IDQuestionCustomer) {
                AutoClosingErrorAlert('You could not set as a child of itself !', 5000);
                return;
            }

            $scope.Param = {
                IDQuestionCustomer: $scope.SelectedQuestionForMove.IDQuestionCustomer,
                IDParentOption: _opt.IDOptionCustomer

            };
            QuestionCustomerService.UpdateParentQuestionCustomer($scope.Param).then(function (result) {
                $scope.GetQuestionCustomer();
                $('#ModalMoveQuestions').modal('hide');

                AutoClosingSuccessAlert('update was successful !', 5000);
            }).catch(function () {
                AutoClosingErrorAlert('Error in update !', 5000);

            }).finally(function () {
                ngProgress.complete();
            });
        }
    }
    $scope.SetQuestionAsRoot = function (_item) {

        var Accept = confirm("Are you sure to set ( " + _item.Question_Fa + " ) As Root ?");
        if (Accept == true) {
            $scope.Param = {
                IDQuestionCustomer: _item.IDQuestionCustomer
            };
            QuestionCustomerService.UpdateQuestionCustomerAsRoot($scope.Param).then(function (result) {
                $scope.GetQuestionCustomer();

                AutoClosingSuccessAlert('update was successful !', 5000);
            }).catch(function () {
                AutoClosingErrorAlert('Error in update !', 5000);

            }).finally(function () {
                ngProgress.complete();
            });
        }
    }

}]);