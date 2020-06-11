
myApp.factory('PurchaseOrderFileService', ['$http', function ($http) {
    return {
        AddPurchaseOrderFile: function (Param) {
            return $http({
                method: 'POST',
                url: '/api/PurchaseOrderFile/AddPurchaseOrderFile/',
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
        UpdatePurchaseOrderFile: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/PurchaseOrderFile/UpdatePurchaseOrderFile/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdateShow: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/PurchaseOrderFile/UpdateShow/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        GetPurchaseOrderFileAndDetail: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/PurchaseOrderFile/GetPurchaseOrderFileAndDetail/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        GetPurchaseOrderFileInViews: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/PurchaseOrderFile/GetPurchaseOrderFileInViews/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        CheckPartNumberInPurchaseOrderFile: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/PurchaseOrderFile/CheckPartNumberInPurchaseOrderFile/',
                headers: { "Content-Type": "application/json" },
                data: JSON.stringify(Param)
            });
        },
        DeletePurchaseOrderFile: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/PurchaseOrderFile/DeletePurchaseOrderFile/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        ChangeSortInPurchaseOrderFile: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/PurchaseOrderFile/ChangeSortInPurchaseOrderFile/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },

    }
}]);

