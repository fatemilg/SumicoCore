myApp.factory('TrainingCourseBatchService', ['$http', function ($http) {
    return {
        GetTrainingCourseBatch: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/TrainingCourseBatch/GetTrainingCourseBatch/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        AddTrainingCourseBatch: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/TrainingCourseBatch/AddTrainingCourseBatch/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdateTrainingCourseBatch: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/TrainingCourseBatch/UpdateTrainingCourseBatch/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        DeleteTrainingCourseBatch: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/TrainingCourseBatch/DeleteTrainingCourseBatch/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    }
}]);




