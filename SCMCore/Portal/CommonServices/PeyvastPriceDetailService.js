myApp.factory('PeyvastPriceDetailService', ['$http', function ($http) {
    return {

        GetPeyvastPriceDetail: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/PeyvastPriceDetail/GetPeyvastPriceDetail/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        GetSumsColumns: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/PeyvastPriceDetail/GetSumsColumnsOfPeyvastPriceDetail/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }

    }
}]);