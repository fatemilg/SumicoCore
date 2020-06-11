myApp.factory('ContentModuleService', function ($http) {
    return {
        GetContentModule: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/ContentModule/GetContentModule/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        AddContentModule: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/ContentModule/AddContentModule/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdateContentModule: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/ContentModule/UpdateContentModule/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        DeleteContentModule: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/ContentModule/DeleteContentModule/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    }
});




