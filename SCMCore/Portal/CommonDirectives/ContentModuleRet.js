myApp.directive("contentmoduleret", function () {
    return {
        restrict: 'A',
        controller: ['$scope', 'ngProgress', 'ContentModuleRetService', 'ContentModuleRetFactory',
            function ($scope, ngProgress, ContentModuleRetService, ContentModuleRetFactory) {
                ContentModuleRetFactory.InitiateContentModuleRetModal = function (obj) {
                    $scope.objSelected = obj;
                    $scope.OpenContentModuleModal(obj.Name_Fa, obj.IDRet);
                    $scope.GetContentModule(obj.IDRet);
                };
                $scope.OpenContentModuleModal = function (Name_Fa, _IDRet) {
                    $scope.SelectedIDRet = _IDRet;
                    $('#ModalContentModule').modal('show');
                    $scope.ContentModuleModalTitle = Name_Fa;
                    $scope.GetContentModule(_IDRet);
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
                $scope.ToggleContentModule = function (_item) {
                    ngProgress.start();
                    var objContentModule = {
                        IDRet: $scope.SelectedIDRet,
                        IDContentModule: _item.IDContentModule
                    };
                    if (_item.Exist) {
                        ContentModuleRetService.DeleteContentModuleRet(objContentModule).then(function (result) {
                            delete _item.Exist;
                        }).catch(function () {
                        }).finally(function () {
                            ngProgress.stop();
                            ngProgress.complete();
                        });
                    }
                    else {
                        ContentModuleRetService.AddContentModuleRet(objContentModule).then(function (result) {
                            _item.Exist = true;
                        }).catch(function () {
                        }).finally(function () {
                            ngProgress.stop();
                            ngProgress.complete();
                        });
                    }
                }
            }],
        templateUrl: '/Portal/CommonTemplate/ContentModuleRet.html'

    }
})

