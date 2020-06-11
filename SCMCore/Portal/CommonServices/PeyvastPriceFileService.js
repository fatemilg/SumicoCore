myApp.factory('PeyvastPriceFileService', ['$http', function ($http) {
    return {
        AddPeyvastPriceFile: function (Param) {
            return $http({
                method: 'POST',
                url: '/api/PeyvastPriceFile/AddPeyvastPriceFile/',
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
        UpdatePeyvastPriceFile: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/PeyvastPriceFile/UpdatePeyvastPriceFile/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdateRaioPeyvastPriceFile: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/PeyvastPriceFile/UpdateRaioPeyvastPriceFile/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        ChangeSortInPeyvastFile: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/PeyvastPriceFile/ChangeSortInPeyvastFile/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        GetPeyvastPriceFile: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/PeyvastPriceFile/GetPeyvastPriceFile/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        GetStockValuePeyvast: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/PeyvastPriceFile/GetStockValuePeyvast/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },

        DeletePeyvastPriceFile: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/PeyvastPriceFile/DeletePeyvastPriceFile/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        CheckPartNumberInPeyvastPriceFile: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/PeyvastPriceFile/CheckPartNumberInPeyvastPriceFile/',
                headers: { "Content-Type": "application/json" },
                data: JSON.stringify(Param)
            });
        },

    }
}]);