myApp.directive("contentmodulelist", function () {
    return {
        restrict: 'A',
        scope: { concept: '=' },
        controller: ['$scope', 'ngProgress', 'ContentModuleRetService', 'ContentModuleRetFactory',
            function ($scope, ngProgress, ContentModuleRetService, ContentModuleRetFactory) {
                ContentModuleRetFactory.InitiateContentModuleList = function () {
                    $scope.OpenContentModuleListModal();
                };
                $scope.OpenContentModuleListModal = function () {
                    $scope.ContentModules = undefined;
                    $scope.ItemsInModule = undefined;
                    $('#ModalContentModuleList').modal('show');
                    $scope.GetContentModule();
                };
                $scope.GetContentModule = function (_IDRet) {
                    var ContentModule = {
                        IDRet: _IDRet
                    }
                    ContentModuleRetService.GetContentModuleByIDRet(ContentModule).then(function (result) {
                        $scope.ContentModules = result.data;
                    }).catch(function () {
                    }).finally(function () {
                    });
                };
                $scope.SortInModule = {
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
                                JsonContentModuleRet: JSON.stringify(evt.models)
                            };
                            ContentModuleRetService.UpdateSortContentModuleRet(obj).then(function (result) {
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
                $scope.GetContentModuleList = function () {
                    if ($scope.ContentModules) {
                        var ContentModule = {
                            IDContentModule: $scope.CurrentModule.IDContentModule
                        }
                        if ($scope.concept == 'TrainingCourseBatch') {
                            ContentModuleRetService.GetContentModuleByIDContentModule_ForTrainingCourseBatch(ContentModule).then(function (result) {
                                $scope.ItemsInModule = result.data;
                            }).catch(function () {
                            }).finally(function () {
                            });
                        }
                        else if ($scope.concept == 'ContentCategory') {
                            ContentModuleRetService.GetContentModuleByIDContentModule(ContentModule).then(function (result) {
                                $scope.ItemsInModule = result.data;
                            }).catch(function () {
                            }).finally(function () {
                            });
                        }

                    }
                };

            }],
        templateUrl: '/Portal/CommonTemplate/ContentModuleList.html'

    }
})

