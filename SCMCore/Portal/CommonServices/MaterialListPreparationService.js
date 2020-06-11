myApp.factory('MaterialListPreparationService', ['$http', function ($http) {
    return {
        GetCountNewMaterialListCreatedNotShown: function () {
            return $http({
                method: 'Post',
                url: '/api/MaterialListPreparation/GetCountNewMaterialListCreatedNotShown/',
                headers: { "Content-Type": "application/json" }
            });
        },
        GetNewMaterialListCreatedByCustomer: function () {
            return $http({
                method: 'Post',
                url: '/api/MaterialListPreparation/GetNewMaterialListCreatedByCustomer/',
                headers: { "Content-Type": "application/json" }
            });
        }
    }
}]);