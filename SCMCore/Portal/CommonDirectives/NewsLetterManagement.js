myApp.directive("newslettermanagement", function () {
    return {
        restrict: 'A',
        controller: ['$scope', 'ngProgress', 'SentEmailService', 'SentEmailFactory',
            function ($scope, ngProgress, SentEmailService, SentEmailFactory) {
                SentEmailFactory.InitiateNewsLetterManagement = function (_objRet) {
                    $scope.objRet = _objRet;
                    $('#ModalNewsLetterManagement').modal('show');
                    $scope.GetSentEmailByIDRet();
                };
                $scope.GetSentEmailByIDRet = function () {
                    var obj = {
                        IDRet: $scope.objRet.IDRet
                    };
                    SentEmailService.GetSentEmailByIDRet(obj).then(function (result) {
                        $scope.SentEmails = result.data;
                    }).catch(function () {
                        AutoClosingErrorAlert('Error !', 3000);
                    }).finally(function () {
                    });
                };
                $scope.ExportExcel = function (_json) {
                    JSONToCSVConvertor(_json,'emails',false);
                };

                $scope.StartSendingNewsLetter = function () {
                    var obj = {
                        IDLogUser: _IDLogUser,
                        IDRet: $scope.objRet.IDRet,
                        IDXRet: $scope.objRet.IDXRet,
                        Subject: $scope.objRet.Subject,
                        body: $scope.objRet.body
                    };
                    SentEmailService.PreparationForNewsLetter(obj).then(function (result) {
                        AutoClosingSuccessAlert('Email has been sent!', 3000);
                        $scope.GetSentEmailByIDRet();
                    }).catch(function () {
                        AutoClosingErrorAlert('Error in sending email !', 3000);
                    }).finally(function () {
                    });
                };
            }],
        templateUrl: '/Portal/CommonTemplate/NewsLetterManagement.html'

    };
});

