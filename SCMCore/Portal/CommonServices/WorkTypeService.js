myApp.factory('WorkTypeService', ['$http', function ($http) {
    return {
        GetWorkType: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/WorkType/GetWorkType/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        AddWorkType: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/WorkType/AddWorkType/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdateWorkType: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/WorkType/UpdateWorkType/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        DeleteWorkType: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/WorkType/DeleteWorkType/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    }
}]);




