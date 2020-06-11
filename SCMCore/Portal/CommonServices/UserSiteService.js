myApp.factory('UserSiteService', ['$http', function ($http) {
    return {
        GetUserSite: function () {
            return $http({
                method: 'Post',
                url: '/api/UserSite/GetUserSite/',
                headers: { "Content-Type": "application/json" }
            });
        },
        GetIntegratedUserSiteByID: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/UserSite/GetIntegratedUserSiteByID/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        GetCountNewUserSiteNotShown: function () {
            return $http({
                method: 'Post',
                url: '/api/UserSite/GetCountNewUserSiteNotShown/',
                headers: { "Content-Type": "application/json" }
            });
        },
        CheckPersonelInCompanyExistInUserSite: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/UserSite/CheckPersonelInCompanyExistInUserSite/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        AddUserSiteIntoPersonelInCompany: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/UserSite/AddUserSiteIntoPersonelInCompany/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdateShowDate: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/UserSite/UpdateShowDate/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdaIDPersonelInCompany: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/UserSite/UpdaIDPersonelInCompany/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    }
}]);