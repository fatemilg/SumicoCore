myApp.factory('PreparationPriceListDetailService', ['$http', function ($http) {
    return {
        GetPreparationPriceListDetailWithOutCategory: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/PreparationPriceListDetail/GetPreparationPriceListDetailWithOutCategory/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        GetPreparationPriceListDetailByCategory: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/PreparationPriceListDetail/GetPreparationPriceListDetailByCategory/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdateEndUserPriceByIDPreparationPriceListDetail: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/PreparationPriceListDetail/UpdateEndUserPriceByIDPreparationPriceListDetail/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdateQoutationDetainInPreparationPriceListDetail: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/PreparationPriceListDetail/UpdateQoutationDetainInPreparationPriceListDetail/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        IncreaseAllEndUserPriceForWithOutCategory: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/PreparationPriceListDetail/IncreaseAllEndUserPriceForWithOutCategory/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        DecreaseAllEndUserPriceForWithOutCategory: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/PreparationPriceListDetail/DecreaseAllEndUserPriceForWithOutCategory/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },

        IncreaseAllEndUserPriceForByCategory: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/PreparationPriceListDetail/IncreaseAllEndUserPriceForByCategory/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        DecreaseAllEndUserPriceForByCategory: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/PreparationPriceListDetail/DecreaseAllEndUserPriceForByCategory/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
    }
}]);