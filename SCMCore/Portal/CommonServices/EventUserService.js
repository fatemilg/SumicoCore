myApp.factory('EventUserService', ['$http', function ($http) {
    return {
        GetEventUser: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/EventUser/GetEventUser/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        GetEventUser_ByIDUser: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/EventUser/GetEventUser_ByIDUser/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        AddEventUser: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/EventUser/AddEventUser/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdateEventUser: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/EventUser/UpdateEventUser/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        DeleteEventUser: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/EventUser/DeleteEventUser/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    }
}]);