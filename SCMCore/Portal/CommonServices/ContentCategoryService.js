myApp.factory('ContentCategoryService', ['$http', function ($http) {
    return {
        FetchContentCategoryData: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/ContentCategory/FillContentCategory/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        DeleteContentCategory: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/ContentCategory/DeleteContentCategory/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        AddContentCategory: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/ContentCategory/AddContentCategory/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },  UpdateContentCategory: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/ContentCategory/UpdateContentCategory/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    }
}]);