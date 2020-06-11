myApp.factory('TrainingCourseUserService', ['$http', function ($http) {
    return {
        GetTrainingCourseUser: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/TrainingCourseUser/GetTrainingCourseUser/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        GetTrainingCourseUser_ByIDTrainingCourse: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/TrainingCourseUser/GetTrainingCourseUser_ByIDTrainingCourse/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        AddTrainingCourseUser: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/TrainingCourseUser/AddTrainingCourseUser/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdateTrainingCourseUser: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/TrainingCourseUser/UpdateTrainingCourseUser/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        DeleteTrainingCourseUser: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/TrainingCourseUser/DeleteTrainingCourseUser/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    }
}]);




