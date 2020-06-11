myApp.factory('MenuService', ['$http', function ($http) {
    return {
        FetchMenuData: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/Menu/FillMenu/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        FetchSubMenuData: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/Menu/FillSubMenu/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        GetPersonelOfMenuData: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/Menu/GetPersonelOfMenuData/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    }
}]);