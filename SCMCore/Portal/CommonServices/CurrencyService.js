myApp.factory('CurrencyService', ['$http', function ($http) {
    return {
        GetCurrency: function () {
            return $http({
                method: 'Post',
                url: '/api/Currency/GetCurrency/',
                headers: { "Content-Type": "application/json" }
            });
        }
    }
}]);