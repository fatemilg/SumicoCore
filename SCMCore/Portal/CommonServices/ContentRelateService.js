
myApp.factory('ContentRelateService', function ($http) {
    return {
        GetContentRelateByIDContent: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/ContentRelate/GetContentRelateDataByIDContent/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        AddContentRelate: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/ContentRelate/AddContentRelate/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        DeleteContentRelate: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/ContentRelate/DeleteContentRelate/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    }
});




