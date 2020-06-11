myApp.factory('QuestionAndAnswerService', ['$http', function ($http) {
    return {
        GetNewQuestionAnswer: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/QuestionAndAnswer/GetNewQuestionAnswer/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        GetDeniedQuestionAnswer: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/QuestionAndAnswer/GetDeniedQuestionAnswer/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        GetAcceptedQuestionAnswer: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/QuestionAndAnswer/GetAcceptedQuestionAnswer/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        AddQuestionAndAnswer: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/QuestionAndAnswer/AddQuestionAndAnswer/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdateQuestionAndAnswer: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/QuestionAndAnswer/UpdateQuestionAndAnswer/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        DeleteQuestionAndAnswer: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/QuestionAndAnswer/DeleteQuestionAndAnswer/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        AcceptByManager: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/QuestionAndAnswer/AcceptByManager/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        DenyByManager: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/QuestionAndAnswer/DenyByManager/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    };
}]);