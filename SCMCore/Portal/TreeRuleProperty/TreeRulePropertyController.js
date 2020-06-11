myApp.controller('TreeRulePropertyController', ['$scope', 'ngProgress', 'TreeRulePropertyService', 'DefineDetailProductService', function ($scope, ngProgress, TreeRulePropertyService, DefineDetailProductService) {

    $scope.CurrentProduct = {};
    $scope.AddTreeRuleProperty = function (_IDProduct, _GeneratedTree) {
        var Param = {
            IDTreeRuleProperty: NewGuid(),
            IDProduct: _IDProduct,
            GeneratedTree: _GeneratedTree
        }
        TreeRulePropertyService.AddTreeRuleProperty(Param).then(function (result) {
            AutoClosingSuccessAlert("Save Successfuly",5000);
        }).catch(function () {
        }).finally(function () {
        });
    };
    $scope.SelectProduct = function (_IDProduct) {
        ngProgress.start();
        var Param = {
            IDProduct: _IDProduct
        }
        DefineDetailProductService.GetGeneratedJsonByIDProduct(Param).then(function (result) {
            $scope.Defines = result.data;
            $scope.FinalTree = $scope.Defines[0].ChildProperty;
            for (i = 0; i < $scope.Defines.length; i++) {
                $scope.GenerateTree($scope.FinalTree, $scope.Defines[i].ChildProperty[0]);
            }
            for (i = 0; i < $scope.Defines.length; i++) {
                $scope.GenerateTree($scope.FinalTree, $scope.Defines[i].ChildProperty2[0]);
            }
            $scope.AddTreeRuleProperty(_IDProduct, JSON.stringify($scope.FinalTree));
        }).catch(function () {
        }).finally(function () {
            ngProgress.complete();
            ngProgress.stop();
        });
    }
    $scope.GenerateTree = function (_Source, _Model) {
        if (_Source.some(item => item.IDX === _Model.IDX)) {
            var index;
            for (j = 0; j < _Source.length; j++) {
                if (_Source[j].IDX == _Model.IDX) {
                    index = j;
                }
            }
            if (_Model.ChildProperty != undefined) {
                var eq = $scope.GenerateTree(_Source[index].ChildProperty, _Model.ChildProperty[0]);
                if (eq == false) {
                    _Source[index].ChildProperty.push(_Model.ChildProperty[0]);
                }
            }

        }
        else {
            return false;
        }
    }

}]);
