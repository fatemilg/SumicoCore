myApp.factory('TrainingCourseService', ['$http', function ($http) {
    return {
        GetTrainingCourse: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/TrainingCourse/GetTrainingCourse/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        AddTrainingCourse: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/TrainingCourse/AddTrainingCourse/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdateTrainingCourse: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/TrainingCourse/UpdateTrainingCourse/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        DeleteTrainingCourse: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/TrainingCourse/DeleteTrainingCourse/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    }
}]);




