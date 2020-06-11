
myApp.factory('IncidentCategoryService', function ($http) {
    return {
        GetIncidentCategory: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/IncidentCategory/GetIncidentCategory/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        GetDataWithIncidents: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/IncidentCategory/GetDataWithIncidents/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        AddIncidentCategory: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/IncidentCategory/AddIncidentCategory/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdateIncidentCategory: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/IncidentCategory/UpdateIncidentCategory/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        DeleteIncidentCategory: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/IncidentCategory/DeleteIncidentCategory/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    }
});




