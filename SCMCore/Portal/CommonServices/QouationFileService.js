myApp.factory('QouationFileService', ['$http', function ($http) {
    return {
        AddQouationFile: function (Param) {
            return $http({
                method: 'POST',
                url: '/api/QouationFile/AddQouationFile/',
                headers: { "Content-Type": undefined },
                data: Param,
                eventHandlers: {
                    progress: function (c) {
                        console.log('Progress -> ' + c);
                        console.log(c);
                    }
                }
            });
        },
        UpdateQouationFile: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/QouationFile/UpdateQouationFile/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdateRatioQouationFile: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/QouationFile/UpdateRatioQouationFile/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        GetQouationFile: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/QouationFile/GetQouationFile/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        ChangeSortInQouationFile: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/QouationFile/ChangeSortInQouationFile/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },

        DeleteQouationFile: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/QouationFile/DeleteQouationFile/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }

    }
}]);