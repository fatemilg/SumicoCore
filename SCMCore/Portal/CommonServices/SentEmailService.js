myApp.factory('SentEmailService', ['$http', function ($http) {
    return {
        SentTestEmail: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/SentEmail/SentTestEmail/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        PreparationForNewsLetter: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/SentEmail/PreparationForNewsLetter/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        GetSentEmailByIDRet: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/SentEmail/GetSentEmailByIDRet/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    }
}]);