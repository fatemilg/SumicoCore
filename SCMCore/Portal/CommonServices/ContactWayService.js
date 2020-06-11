myApp.factory('ContactWayService', ['$http', function ($http) {
    return {
        GetContactWay: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/ContactWay/GetContactWay/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        GetContactWay_ByIDUser: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/ContactWay/GetContactWay_ByIDUser/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        AddContactWay: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/ContactWay/AddContactWay/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdateContactWay: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/ContactWay/UpdateContactWay/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdateMainContactWayAndUser: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/ContactWay/UpdateMainContactWayAndUser/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        DeleteContactWay: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/ContactWay/DeleteContactWay/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    }
}]);




