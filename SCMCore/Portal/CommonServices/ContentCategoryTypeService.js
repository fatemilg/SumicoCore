myApp.factory('ContentCategoryTypeService', ['$http', function ($http) {
    return {
        FillContentCategoryTypeComplete: function () {
            return $http({
                method: 'Post',
                url: '/api/ContentCategoryType/FillContentCategoryTypeComplete/',
                headers: { "Content-Type": "application/json" }
            });
        }
    }
}]);