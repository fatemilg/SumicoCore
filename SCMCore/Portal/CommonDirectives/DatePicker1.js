myApp.directive("datepicker", function () {
    return {
        restrict: 'A',
        scope: {ngModel: '=' },
        link: function (scope, elem, ngModel) {
            
            if (!ngModel)
                return; // do nothing if no ng-model

            elem.datetimepicker({
                format: 'YYYY/MM/DD hh:mm:ss a '
            })

            // Listen for change events to enable binding
            elem.on('dp.change', function (e) {
                
                scope.$apply(function () {
                    scope.ngModel = e.target.value;
                });

            });



        }
    }
})