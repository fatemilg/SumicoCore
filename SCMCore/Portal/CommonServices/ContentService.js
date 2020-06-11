myApp.factory('ContentService', ['$http', function ($http) {
    return {
        FillContentDataByID: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/Content/FillContentByID/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        DeleteContent: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/Content/DeleteContent/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        AddContent: function (Param) {
            return $http({
                method: 'POST',
                url: '/api/Content/AddContent/',
                headers: { "Content-Type": "application/json" },
                data: JSON.stringify(Param)
            });
        },
        LikeContent: function (Param) {
            return $http({
                method: 'POST',
                url: '/api/Content/LikeContent/',
                headers: { "Content-Type": "application/json" },
                data: JSON.stringify(Param)
            });
        },
        UnLikeContent: function (Param) {
            return $http({
                method: 'POST',
                url: '/api/Content/UnLikeContent/',
                headers: { "Content-Type": "application/json" },
                data: JSON.stringify(Param)
            });
        },
        UpdateContent: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/Content/UpdateContent/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        ToggleActivation: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/Content/ToggleActivation/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    }
}]);