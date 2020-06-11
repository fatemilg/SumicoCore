myApp.directive("tooltip", function () {
    return {
        restrict: 'A',
        link: function (scope, elem) {
            elem.tooltip();
        }
    }
})