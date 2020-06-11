myApp.factory('ContentCategoryContentService', function ($http) {
    return {
        AddContentCategoryContent: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/ContentCategoryContent/AddContentCategoryContent/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        DeleteContentCategoryContent: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/ContentCategoryContent/DeleteContentCategoryContent/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdateSortContentCategoryContent: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/ContentCategoryContent/UpdateSortContentCategoryContent/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    }
});




