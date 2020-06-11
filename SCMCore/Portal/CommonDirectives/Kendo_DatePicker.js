myApp.directive("kendodatepicker", function () {
    return {
        restrict: 'A',
        link: function (scope, elem) {
            elem.kendoDatePicker();
            $(elem).on("input", function () {
                var strDate = $(this).val();
                if (isNaN(strDate) || strDate > 2000) {
                    $(this).val('');
                }
            });

            $(elem).change(function () {
                var strDate = $(this).val();
                var CheckDate = isDate(strDate);
                if (CheckDate == false) {
                    $(this).val('');
                }
                
            });
        }
    }
})


