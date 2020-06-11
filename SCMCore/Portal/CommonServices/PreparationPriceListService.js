myApp.factory('PreparationPriceListService', ['$http', function ($http) {
    return {

        GetPreparationPriceList: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/PreparationPriceList/GetPreparationPriceList/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        AddPreparationPriceList: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/PreparationPriceList/AddPreparationPriceList/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdatePreparationPriceList: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/PreparationPriceList/UpdatePreparationPriceList/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdatePreparationPriceListByShow: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/PreparationPriceList/UpdatePreparationPriceListByShow/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        DeletePreparationPriceList: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/PreparationPriceList/DeletePreparationPriceList/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },

    }
}]);