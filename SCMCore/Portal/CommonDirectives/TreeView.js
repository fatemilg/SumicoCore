myApp.directive("svtreeview", function () {
    return {
        restrict: 'A',
        scope: { datasource: '=', selectevent: '&', ngModel: '=' },
        controller: function ($scope) {

            $scope.SelectedTextTreeView = 'Select...';
            $scope.SelectItem = function (_item, _ev) {
                $scope.selectevent()(_item);
                $('.TreeViewBody').slideUp('fast');
                $scope.SelectedTextTreeView = _item.Title;
                $scope.ngModel = _item.IDUser;
            }

            $scope.ToggleTreeViewBody = function (_ev) {
                $(_ev.currentTarget).parent().find('.TreeViewBody').slideToggle('fast');
            }
            $scope.$watch('ngModel', function () {
                if ($scope.datasource != undefined) {
                    var list = findById($scope.datasource, $scope.ngModel, 'IDUser');
                    if (list && list.Title)
                        $scope.SelectedTextTreeView = list.Title;
                    else {
                        $scope.SelectedTextTreeView = 'Select...';
                    }
                }
            }, true);

        },
            
        templateUrl: '/Portal/CommonTemplate/TreeView.html'

    }
})

