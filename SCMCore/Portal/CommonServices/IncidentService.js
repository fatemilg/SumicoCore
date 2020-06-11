myApp.factory('IncidentService', function ($http) {
    return {
        GetIncident: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/Incident/GetIncident/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        GetIncidentAll: function () {
            return $http({
                method: 'Post',
                url: '/api/Incident/GetIncidentAll/',
                headers: { "Content-Type": "application/json" }
            });
        },
        AddIncident: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/Incident/AddIncident/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdateIncident: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/Incident/UpdateIncident/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        DeleteIncident: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/Incident/DeleteIncident/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    }
});




