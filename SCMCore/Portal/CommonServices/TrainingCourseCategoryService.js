
myApp.factory('TrainingCourseCategoryService', ['$http', function ($http) {
    return {
        GetTrainingCourseCategory: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/TrainingCourseCategory/GetTrainingCourseCategory/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        GetNestedTrainingCourseCategory: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/TrainingCourseCategory/GetNestedTrainingCourseCategory/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        AddTrainingCourseCategory: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/TrainingCourseCategory/AddTrainingCourseCategory/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdateTrainingCourseCategory: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/TrainingCourseCategory/UpdateTrainingCourseCategory/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        DeleteTrainingCourseCategory: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/TrainingCourseCategory/DeleteTrainingCourseCategory/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    }
}]);




