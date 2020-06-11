myApp.factory('AttachInterfaceCategoryService', ['$http', function ($http) {
    return {
        GetFolders: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/AttachInterfaceCategory/GetFolders/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        ChangeParentFolders: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/AttachInterfaceCategory/ChangeParentFolders/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        LoadBreadcrumbs: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/AttachInterfaceCategory/LoadBreadcrumbs/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        ListFoldersFiles: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/AttachInterfaceCategory/ListFoldersFiles/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    }
}]);