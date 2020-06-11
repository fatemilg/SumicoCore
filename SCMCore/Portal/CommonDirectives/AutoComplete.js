myApp.directive("svautocomplete", function () {
    return {
        restrict: 'A',
        scope: { datasource: '=', selectedevent: '&', rownumber: '@', minlength: '@', ngModel: '=' },
        controller: function ($scope) {
            $scope.AutoSearchParam = $scope.ngModel;
            $scope.SelectedItem = function (_item, _ev) {
                $scope.selectedevent()(_item, $scope.rownumber);
                $(_ev.currentTarget).parent().fadeOut();
                $scope.AutoSearchParam = _item.Title;
            }
            $scope.ChangeAutoComplete = function () {
                $scope.ngModel = $scope.AutoSearchParam;
            }

        },
        link: function (scope, elem) {
            $(document).on('click', function () {
                $('.AutoCompleteBody').fadeOut();
            })
        },
        templateUrl: '/Portal/CommonTemplate/AutoComplete.html'

    }
})

