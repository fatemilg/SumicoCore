myApp.controller('ApplicationFormController', ['$scope', 'ApplicationFormService', function ($scope, ApplicationFormService) {


    $scope.GenerateExcelApplicationForm = function () {

        ApplicationFormService.GetApplicationForm().then(function (result) {
            $scope.ApplicationForms = result.data;
            var col_list = ['CreateDate', 'ApplicationFormType', 'FName', 'LName', 'BirthCity', 'BirthYear', 'FatherJob',
                'Grade', 'FieldStudy', 'University', 'GraduationYear', 'GPA', 'DiplomFieldStudy', 'Residence',
                'TotalExperience', 'SoftwareExperience', 'EmploymentStatus', 'RequestedSalary', 'Mobile'];
                
                
            JSONToCSVConvertor($scope.ApplicationForms, 'HR-Form', true, col_list);

        }).catch(function () {
            AutoClosingErrorAlert('Error in Fetch  !', 5000);

        }).finally(function () {
        });

    }
}]);