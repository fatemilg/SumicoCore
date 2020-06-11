myApp.factory('DefineDetailProductService', ['$http', function ($http) {
    return {
        ChangeSortInDefineDetailProduct: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/DefineDetailProduct/ChangeSortInCrmForDefineDetailProduct/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        GetListOfPartNumbers: function () {
            return $http({
                method: 'Post',
                url: '/api/DefineDetailProduct/GetListOfPartNumbers/',
                headers: { "Content-Type": "application/json" }
            });
        },
        GetDefineDetailProduct: function (Param) {  
            return $http({
                method: 'Post',
                url: '/api/DefineDetailProduct/GetDefineDetailProduct/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        GetAutoCompleteDefineDetailProduct: function () {  
            return $http({
                method: 'Post',
                url: '/api/DefineDetailProduct/GetAutoCompleteDefineDetailProduct/',
                headers: { "Content-Type": "application/json" }
            });
        }
       ,
        GetGeneratedJsonByIDProduct: function (Param) {  
            return $http({
                method: 'Post',
                url: '/api/DefineDetailProduct/GetGeneratedJsonByIDProduct/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    }
}]);