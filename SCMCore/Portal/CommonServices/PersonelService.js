myApp.factory('PersonelService', ['$http', function ($http) {
    return {
        GetPersonel: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/Personel/GetPersonel/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    }
}]);