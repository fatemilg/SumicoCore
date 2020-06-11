myApp.filter('sumByKey', function () {
    return function (collection, column) {
        var total = 0;
        if (collection != undefined) {
            collection.forEach(function (item) {
                if (item[column] == '' || item[column] == undefined) {
                    item[column] = 0;
                }
                total += parseFloat(item[column]);
            });
            var FloatPoint = total - parseInt(total)
            if (FloatPoint > 0) {

                return total.toFixed(2);

            }
            else {
                return parseInt(total);
            }
        };
    }

});
myApp.filter('sumForTotal', function () {
    return function (collection, column1, column2) {
        var total = 0;
        if (collection != undefined) {
            collection.forEach(function (item) {
                if (item[column1] == '' || item[column1] == undefined) {
                    item[column1] = 0;
                }
                if (item[column2] == '' || item[column2] == undefined) {
                    item[column2] = 0;
                }
                total += parseFloat(item[column1]) * parseFloat(item[column2]);
            });
            var FloatPoint = total - parseInt(total)
            if (FloatPoint > 0) {

                return total.toFixed(2);

            }
            else {
                return parseInt(total);
            }
        }
    };
});