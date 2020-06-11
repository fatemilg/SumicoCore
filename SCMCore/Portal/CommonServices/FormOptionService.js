myApp.factory('FormOptionService', function ($http) {
    return {
        GetFormOptionDataByIDFormQuestion: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/FormOption/GetFormOptionDataByIDFormQuestion/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        AddFormOption: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/FormOption/AddFormOption/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdateFormOption: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/FormOption/UpdateFormOption/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        DeleteFormOption: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/FormOption/DeleteFormOption/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        ChangeSortOptions: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/FormOption/ChangeSortOptions/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    }
});




