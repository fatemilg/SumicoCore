myApp.factory('PeyvastStockService', ['$http', function ($http) {
    return {
        GetPeyvastStock: function () {
            return $http({
                method: 'Post',
                url: '/api/PeyvastStock/GetPeyvastStock/',
                headers: { "Content-Type": "application/json" }
            });
        }
    }
}]);