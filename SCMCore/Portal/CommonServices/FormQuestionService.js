
myApp.factory('FormQuestionService', function ($http) {
    return {
        GetFormQuestionDataByIDDynamicForm: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/FormQuestion/GetFormQuestionDataByIDDynamicForm/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        AddFormQuestion: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/FormQuestion/AddFormQuestion/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdateFormQuestion: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/FormQuestion/UpdateFormQuestion/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        DeleteFormQuestion: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/FormQuestion/DeleteFormQuestion/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        ChangeSortQuestions: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/FormQuestion/ChangeSortQuestions/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    }
});




