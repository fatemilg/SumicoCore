myApp.factory('MaterialListDetailService', ['$http', function ($http) {
    return {
        FetchMenuData: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/Menu/FillMenu/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    };
}]);