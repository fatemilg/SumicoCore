myApp.factory('OptionCustomerService', ['$http', function ($http) {
    return {
        GetOptionCustomer: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/OptionCustomer/GetOptionCustomer/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        AddOptionCustomer: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/OptionCustomer/AddOptionCustomer/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdateOptionCustomer: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/OptionCustomer/UpdateOptionCustomer/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        ChangeSortInOptionCustomer: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/OptionCustomer/ChangeSortInOptionCustomer/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        DeleteOptionCustomer: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/OptionCustomer/DeleteOptionCustomer/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    }
}]);
