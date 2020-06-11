myApp.filter('Gregorian2Jalali', function () {
    return function (strDate) {
        var CurrentDate = strDate.split("T")[0].replace('-', '/').replace('-', '/');
        return ginj(CurrentDate, true);
    };
});

