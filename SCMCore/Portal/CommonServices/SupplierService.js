myApp.factory('SupplierService', ['$http', function ($http) {
    return {
        FillSupplier: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/Supplier/FillSupplier/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    }
}]);