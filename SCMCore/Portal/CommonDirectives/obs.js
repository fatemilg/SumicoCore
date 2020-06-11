myApp.directive("obs",['$rootScope', function ($rootScope) {
    return {
        restrict: 'C',
        link: function (scope, elem) {
            scope.$watch(function () {
                if ($rootScope.Personel != undefined) {
                    if ($rootScope.Personel.SupperUser != true) {
                        if ($rootScope.EventUser != undefined) {
                            var Remove = true;
                            for (var obj in $rootScope.EventUser) {
                                if (elem.attr('id') == $rootScope.EventUser[obj].Title_En) {
                                    Remove = false;
                                }
                            }
                            if (Remove) {
                                elem.remove();
                            }
                        }
                    }
                }
            }, true);
        }
    }
}])