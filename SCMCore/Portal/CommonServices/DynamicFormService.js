myApp.factory('DynamicFormService', function ($http) {
    return {
        GetDynamicForm: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/DynamicForm/GetDynamicForm/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        AddDynamicForm: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/DynamicForm/AddDynamicForm/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdateDynamicForm: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/DynamicForm/UpdateDynamicForm/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        DeleteDynamicForm: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/DynamicForm/DeleteDynamicForm/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    }
});




