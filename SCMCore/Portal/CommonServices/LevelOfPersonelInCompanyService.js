myApp.factory('LevelOfPersonelInCompanyService', ['$http', function ($http) {
    return {
        GetLevelOfPersonelInCompany: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/LevelOfPersonelInCompany/GetLevelOfPersonelInCompany/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        AddLevelOfPersonelInCompany: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/LevelOfPersonelInCompany/AddLevelOfPersonelInCompany/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdateLevelOfPersonelInCompany: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/LevelOfPersonelInCompany/UpdateLevelOfPersonelInCompany/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        DeleteLevelOfPersonelInCompany: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/LevelOfPersonelInCompany/DeleteLevelOfPersonelInCompany/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    }
}]);




