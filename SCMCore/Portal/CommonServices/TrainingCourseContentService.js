myApp.factory('TrainingCourseContentService', ['$http', function ($http) {
    return {
        GetTrainingCourseContent: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/TrainingCourseContent/GetTrainingCourseContent/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        GetTrainingCourseContent_ByIDTrainningCourse: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/TrainingCourseContent/GetTrainingCourseContent_ByIDTrainningCourse/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        AddTrainingCourseContent: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/TrainingCourseContent/AddTrainingCourseContent/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        SaveTrainingCourseContent: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/TrainingCourseContent/SaveTrainingCourseContent/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdateTrainingCourseContent: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/TrainingCourseContent/UpdateTrainingCourseContent/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        DeleteTrainingCourseContent: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/TrainingCourseContent/DeleteTrainingCourseContent/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    }
}]);




