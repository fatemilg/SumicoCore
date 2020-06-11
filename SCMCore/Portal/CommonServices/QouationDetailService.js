myApp.factory('QouationDetailService', ['$http', function ($http) {
    return {
        GetQouationDetail: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/QouationDetail/GetQouationDetail/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        GetSumsColumns: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/QouationDetail/GetSumsColumnsOfQouationDetail/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        AddAllQouationDetail: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/QouationDetail/AddAllQouationDetail/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        DeleteSelectedQouationDetail: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/QouationDetail/DeleteSelectedQouationDetail/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        GetQouationDetailByPartNumber: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/QouationDetail/GetQouationDetailByPartNumber/',
                headers: { "Content-Type": "application/json" },
                data: JSON.stringify(Param)
            });
        },
    }
}]);