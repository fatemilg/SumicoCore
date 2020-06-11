
myApp.factory('DynamicFormRetService', function ($http) {
    return {
        GetDynamicFormRet: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/DynamicFormRet/GetDynamicFormRet/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        AddDynamicFormRet: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/DynamicFormRet/AddDynamicFormRet/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdateDynamicFormRet: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/DynamicFormRet/UpdateDynamicFormRet/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        DeleteDynamicFormRet: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/DynamicFormRet/DeleteDynamicFormRet/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    }
});




