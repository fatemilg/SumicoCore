myApp.factory('RelatePurchaseOrderFileService', ['$http', function ($http) {
    return {
        AddRelatePurchaseOrderFile: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/RelatePurchaseOrderFile/AddRelatePurchaseOrderFile/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        GetDataForPOHistory: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/RelatePurchaseOrderFile/GetDataForPOHistory/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },

    }
}]);