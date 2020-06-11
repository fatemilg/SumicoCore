
myApp.factory('ContentModuleRetService', function ($http) {
    return {
        GetContentModuleRet: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/ContentModuleRet/GetContentModuleRet/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        GetContentModuleByIDRet: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/ContentModuleRet/GetContentModuleByIDRet/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        GetContentModuleByIDContentModule: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/ContentModuleRet/GetContentModuleByIDContentModule/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        GetContentModuleByIDContentModule_ForTrainingCourseBatch: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/ContentModuleRet/GetContentModuleByIDContentModule_ForTrainingCourseBatch/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        AddContentModuleRet: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/ContentModuleRet/AddContentModuleRet/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdateContentModuleRet: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/ContentModuleRet/UpdateContentModuleRet/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdateSortContentModuleRet: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/ContentModuleRet/UpdateSortContentModuleRet/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        DeleteContentModuleRet: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/ContentModuleRet/DeleteContentModuleRet/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    }
});




