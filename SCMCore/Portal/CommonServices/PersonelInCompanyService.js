myApp.factory('PersonelInCompanyService', ['$http', function ($http) {
    return {
        GetPersonelInCompany: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/PersonelInCompany/GetPersonelInCompany/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },

        GetPersonelInCompanyByNationalCodeAndFullName: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/PersonelInCompany/GetPersonelInCompanyByNationalCodeAndFullName/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        AddPersonelInCompany: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/PersonelInCompany/AddPersonelInCompany/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdatePersonelInCompany: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/PersonelInCompany/UpdatePersonelInCompany/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        DeletePersonelInCompany: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/PersonelInCompany/DeletePersonelInCompany/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    }
}]);