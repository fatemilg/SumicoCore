myApp.factory('ProductCategoryService', ['$http', function ($http) {
    return {
        FetchProductCategoryDataFromParentToMaster: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/ProductCategory/FillProductCategoryFromParentToMaster/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    }
}]);