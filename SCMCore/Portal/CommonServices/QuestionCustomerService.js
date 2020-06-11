myApp.factory('QuestionCustomerService', ['$http', function ($http) {
    return {
        GetQuestionCustomer: function () {
            return $http({
                method: 'Post',
                url: '/api/QuestionCustomer/GetQuestionCustomer/',
                headers: { "Content-Type": "application/json" }
            });
        },
        CheckSortSequently: function () {
            return $http({
                method: 'Post',
                url: '/api/QuestionCustomer/CheckSortSequently/',
                headers: { "Content-Type": "application/json" }
            });
        },
        AddQuestionCustomer: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/QuestionCustomer/AddQuestionCustomer/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdateQuestionCustomer: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/QuestionCustomer/UpdateQuestionCustomer/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdateParentQuestionCustomer: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/QuestionCustomer/UpdateParentQuestionCustomer/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdateQuestionCustomerAsRoot: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/QuestionCustomer/UpdateQuestionCustomerAsRoot/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdateUseInCombination: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/QuestionCustomer/UpdateUseInCombination/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdateUseInSignUp: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/QuestionCustomer/UpdateUseInSignUp/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdateUseInMaterialDetail: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/QuestionCustomer/UpdateUseInMaterialDetail/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        ChangeSortInQuestionCustomer: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/QuestionCustomer/ChangeSortInQuestionCustomer/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        DeleteQuestionCustomer: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/QuestionCustomer/DeleteQuestionCustomer/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    };
}]);
