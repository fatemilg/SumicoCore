myApp.factory('ApplicationFormService', ['$http', '$rootScope', function ($http, $rootScope) {
    return {
        GetApplicationForm: function () {
            return $http({
                method: 'Post',
                url: '/api/ApplicationForm/GetApplicationForm/',
                headers: { "Content-Type": "application/json" }
            });
        }
    }
}]);