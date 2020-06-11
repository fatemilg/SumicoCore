myApp.factory('SupplierPriceListDetailService', ['$http', function ($http) {
    return {
        GetSupplierPriceListDetail: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/SupplierPriceListDetail/GetSupplierPriceListDetail/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        GetSumsColumns: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/SupplierPriceListDetail/GetSumsColumnsOfSupplierPriceListDetail/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    }
}]);