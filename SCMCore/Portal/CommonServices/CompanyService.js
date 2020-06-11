myApp.factory('CompanyService', ['$http', function ($http) {
    return {
        GetCompany: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/Company/GetCompany/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        AddCompany: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/Company/AddCompany/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdateCompany: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/Company/UpdateCompany/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        DeleteCompany: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/Company/DeleteCompany/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    }
}]);




