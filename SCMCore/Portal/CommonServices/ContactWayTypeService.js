myApp.factory('ContactWayTypeService', ['$http', function ($http) {
    return {
        GetContactWayType: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/ContactWayType/GetContactWayType/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        AddContactWayType: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/ContactWayType/AddContactWayType/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdateContactWayType: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/ContactWayType/UpdateContactWayType/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        DeleteContactWayType: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/ContactWayType/DeleteContactWayType/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    }
}]);




