myApp.factory('ContentDictionaryService', function ($http) {
    return {
        GetContentDictionaryByIDContent: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/ContentDictionary/GetContentDictionaryByIDContent/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        GetSelectedDataByIDContent: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/ContentDictionary/GetSelectedDataByIDContent/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        AddContentDictionary: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/ContentDictionary/AddContentDictionary/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        ChangeSortInContentDictionary: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/ContentDictionary/ChangeSortInContentDictionary/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        DeleteContentDictionary: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/ContentDictionary/DeleteContentDictionary/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    }
});




