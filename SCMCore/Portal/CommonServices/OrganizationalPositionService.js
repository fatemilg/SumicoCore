myApp.factory('OrganizationalPositionService', ['$http', function ($http) {
    return {
        GetOrganizationalPosition: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/OrganizationalPosition/GetOrganizationalPosition/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        AddOrganizationalPosition: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/OrganizationalPosition/AddOrganizationalPosition/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdateOrganizationalPosition: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/OrganizationalPosition/UpdateOrganizationalPosition/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        DeleteOrganizationalPosition: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/OrganizationalPosition/DeleteOrganizationalPosition/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    }
}]);




