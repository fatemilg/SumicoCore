myApp.directive("adminsetting", ['$rootScope', function ($rootScope) {
    return {
        restrict: 'A',
        link: function (scope, elem) {
            scope.$watch(function () {
                if ($rootScope.Personel != undefined) {
                    if ($rootScope.Personel.SupperUser != true) {
                        elem.remove();
                    }
                }
            }, true);
        }
    }
}])