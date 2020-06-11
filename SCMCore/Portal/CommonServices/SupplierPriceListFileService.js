myApp.factory('SupplierPriceListFileService', ['$http', function ($http) {
    return {
        AddSupplierPriceListFile: function (Param) {
            return $http({
                method: 'POST',
                url: '/api/SupplierPriceListFile/AddSupplierPriceListFile/',
                headers: { "Content-Type": undefined },
                data: Param,
                eventHandlers: {
                    progress: function (c) {
                        console.log('Progress -> ' + c);
                        console.log(c);
                    }
                }
            });
        },
        UpdateRaioSupplierPriceListFile: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/SupplierPriceListFile/UpdateRaioSupplierPriceListFile/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdateSupplierPriceListFile: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/SupplierPriceListFile/UpdateSupplierPriceListFile/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        ChangeSortInSupplierPriceFile: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/SupplierPriceListFile/ChangeSortInSupplierPriceFile/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        GetSupplierPriceListFile: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/SupplierPriceListFile/GetSupplierPriceListFile/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },

        DeleteSupplierPriceListFile: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/SupplierPriceListFile/DeleteSupplierPriceListFile/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }


    }
}]);