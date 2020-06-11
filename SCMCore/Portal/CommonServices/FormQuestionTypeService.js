
myApp.factory('FormQuestionTypeService', function ($http) {
    return {
        GetFormQuestionType: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/FormQuestionType/GetFormQuestionType/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        AddFormQuestionType: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/FormQuestionType/AddFormQuestionType/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdateFormQuestionType: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/FormQuestionType/UpdateFormQuestionType/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        DeleteFormQuestionType: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/FormQuestionType/DeleteFormQuestionType/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    }
});




