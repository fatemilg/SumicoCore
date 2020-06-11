myApp.factory('DictionaryService', ['$http', function ($http) {
    return {
        GetDictionary: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/Dictionary/GetAllDictionary/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        AddDictionary: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/Dictionary/AddDictionary/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdateDictionary: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/Dictionary/UpdateDictionary/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        ToggleActivation: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/Dictionary/ToggleActivation/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        DeleteDictionary: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/Dictionary/DeleteDictionary/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    }
}]);




