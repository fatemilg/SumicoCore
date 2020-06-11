
myApp.factory('PurchaseOrderService', ['$http', function ($http) {
    return {
      
        GetPurchaseOrderInProgress: function () {
            return $http({
                method: 'POST',
                url: '/api/PurchaseOrder/GetPurchaseOrderInProgress/',
                headers: { "Content-Type": "application/json" }
            });
        },
        GetPurchaseOrderByIDPurchaseOrderFile: function (Param) {
            return $http({
                method: 'POST',
                url: '/api/PurchaseOrder/GetPurchaseOrderByIDPurchaseOrderFile/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
     
            });
        },
        GetPurchaseOrderByPartNumber: function (Param) {
            return $http({
                method: 'POST',
                url: '/api/PurchaseOrder/GetPurchaseOrderByPartNumber/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }

            });
        }
    }
}]);