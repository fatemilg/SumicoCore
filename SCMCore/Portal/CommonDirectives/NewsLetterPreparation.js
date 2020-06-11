myApp.directive("newsletterpreparation", function () {
    return {
        restrict: 'A',
        controller: ['$scope', 'ngProgress', 'SentEmailService', 'SentEmailFactory',
            function ($scope, ngProgress, SentEmailService, SentEmailFactory) {
                SentEmailFactory.InitiateSentEmailModal = function (_objRet) {
                    $scope.objRet = _objRet;
                    $('#ModalNewsLetterPreparation').modal('show');
                };
                $scope.SendTestNewsLetter = function () {
                    var obj = {
                        IDLogUser: _IDLogUser,
                        IDRet: $scope.objRet.IDRet,
                        IDXRet: $scope.objRet.IDXRet,
                        EmailTo: $scope.CurrentTestNewsLetter.EmailTo,
                        Subject: $scope.objRet.Subject,
                        body: $scope.objRet.body
                    };
                    SentEmailService.SentTestEmail(obj).then(function (result) {
                        AutoClosingSuccessAlert('ارسال ایمیل تست با موفقیت انجام شد!', 3000);
                        $scope.CurrentTestNewsLetter.EmailTo = '';
                        $scope.GetSentEmailByIDRet();
                    }).catch(function () {
                        AutoClosingErrorAlert('خطا در ارسال ایمیل تست !', 3000);
                    }).finally(function () {
                    });
                };
               
                $scope.OpenNewsLetterManagement = function () {
                    SentEmailFactory.InitiateNewsLetterManagement($scope.objRet);
                };
            }],
        templateUrl: '/Portal/CommonTemplate/NewsLetterPreparation.html'

    };
});

