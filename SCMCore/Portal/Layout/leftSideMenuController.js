
myApp.controller('Menu', ['$scope','MenuService', function ($scope, MenuService) {
    $scope.FillMenu = function () {
        $scope.authorization = {
            IDLogUser: _IDLogUser
        };
        MenuService.FetchMenuData($scope.authorization).then(function (result) {
            $scope.Menu = result.data;
        }).catch(function () {

        }).finally(function () {

        });
    };

    $scope.FillSubMenu = function (_IDParent) {
        $('#accordionLeftSideMenu').find('.collapse.in').collapse('hide');
        $('#Collapse' + _IDParent).collapse('show');

        $scope.ParentMenu = {
            IDLogUser: _IDLogUser,
            IDParent: _IDParent
        };
        MenuService.FetchSubMenuData($scope.ParentMenu).then(function (result) {
            $scope.SubMenu = result.data;
        }).catch(function () {

        }).finally(function () {

        });
    }

    $scope.FillMenu();
}]);

