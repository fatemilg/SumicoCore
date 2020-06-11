myApp.directive("sortablelist", function () {
    return {
        restrict: 'A',
        link: function (scope, elem) {
            elem.sortable();
        }
    }
})