myApp.factory('TrainingCourseBatchTrainingCourseService', ['$http', function ($http) {
    return {
        GetDataByIDTrainingCourseBatchByIDTrainingCourseBatch: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/TrainingCourseBatchTrainingCourse/GetDataByIDTrainingCourseBatchByIDTrainingCourseBatch/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }, GetTrainingCourseBatchTainingCourseByIDTrainingCourse: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/TrainingCourseBatchTrainingCourse/GetTrainingCourseBatchTainingCourseByIDTrainingCourse/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        AddTrainingCourseBatchTrainingCourse: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/TrainingCourseBatchTrainingCourse/AddTrainingCourseBatchTrainingCourse/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdateTrainingCourseBatchTrainingCourse: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/TrainingCourseBatchTrainingCourse/UpdateTrainingCourseBatchTrainingCourse/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        DeleteTrainingCourseBatchTrainingCourse: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/TrainingCourseBatchTrainingCourse/DeleteTrainingCourseBatchTrainingCourse/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        ChangeSortTrainingCourse: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/TrainingCourseBatchTrainingCourse/ChangeSortTrainingCourse/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        ToggleSelectTrainingCourseBatch: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/TrainingCourseBatchTrainingCourse/ToggleSelectTrainingCourseBatch/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    }
}]);




