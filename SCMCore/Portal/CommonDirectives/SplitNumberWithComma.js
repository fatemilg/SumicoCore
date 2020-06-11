myApp.directive("splitwithcomma", function () {
    return {
        restrict: 'A',
        scope: { ngModel: '=' },
        link: function (scope, elem) {
            $(elem).keyup(function (e) {
                var x = splitComma($(this).val());
                $('#' + e.currentTarget.id).val(x);
            });
            $(elem).change(function (e) {
                var y = splitComma($(this).val());
                $('#' + e.currentTarget.id).val(y);
            });
            if (scope.ngModel != undefined) {
                scope.ngModel = splitComma(scope.ngModel.toString());
            }
            function splitComma(_inputValue) {
                var thousand_separator = ',',
                    decimal_separator = '.',
                    regex = new RegExp('[^' + decimal_separator + '\\d]', 'g'),
                    number_string = _inputValue.replace(regex, '').toString(),
                    split = number_string.split(decimal_separator),
                    rest = split[0].length % 3,
                    result = split[0].substr(0, rest),
                    thousands = split[0].substr(rest).match(/\d{3}/g);

                if (thousands) {
                    separator = rest ? thousand_separator : '';
                    result += separator + thousands.join(thousand_separator);
                }
                result = split[1] != undefined ? result + decimal_separator + split[1] : result;
                return result;
            }
        }
    }
})