myApp.controller('QuestionAndAnswerCheckController', ['$scope', 'ngProgress', '$sce', 'QuestionAndAnswerService', function ($scope, ngProgress, $sce, QuestionAndAnswerService) {


    $scope.GetNewQuestionAnswer = function () {
        ngProgress.start();
        QuestionAndAnswerService.GetNewQuestionAnswer().then(function (result) {
            $scope.QuestionAndAnswers = result.data;
            $scope.changeValueRadioButton = "New";
        }).catch(function () {
        }).finally(function () {
            ngProgress.complete();
            ngProgress.stop();
        });
    };
    $scope.GetAcceptedQuestionAnswer = function () {
        QuestionAndAnswerService.GetAcceptedQuestionAnswer().then(function (result) {
            $scope.QuestionAndAnswers = result.data;
            $scope.changeValueRadioButton = "Accepted";
        }).catch(function () {
        }).finally(function () {
            ngProgress.complete();
            ngProgress.stop();
        });
    };
    $scope.GetDeniedQuestionAnswer = function () {
        QuestionAndAnswerService.GetDeniedQuestionAnswer().then(function (result) {
            $scope.QuestionAndAnswers = result.data;
            $scope.changeValueRadioButton = "Denied";
        }).catch(function () {
        }).finally(function () {
            ngProgress.complete();
            ngProgress.stop();
        });
    };


    $scope.ShowAnswerModal = function (_item) {
        $scope.Answer = $sce.trustAsHtml(_item.Answer);
        $scope.DescriptionByManager = _item.DescriptionByManager;
        $scope.SelectedQuestionAnswer = _item;
        $('#ModalShowAnswer').modal('show');
    };
    $scope.GetNewQuestionAnswer();

    $scope.AcceptAnswer = function () {
        ngProgress.start();
        var param = {
            IDQuestionAndAnswer: $scope.SelectedQuestionAnswer.IDQuestionAndAnswer
        };
        QuestionAndAnswerService.AcceptByManager(param).then(function (result) {
            $scope.GetAcceptedQuestionAnswer();
            $('#ModalShowAnswer').modal('hide');
            AutoClosingSuccessAlert('Accept was successful !', 3000);
        }).catch(function () {
            AutoClosingErrorAlert('Error in Accept !', 5000);

        }).finally(function () {
            ngProgress.complete();
            ngProgress.stop();
        });
    };
    $scope.DenyAnswer = function (_item) {
        ngProgress.start();
        var param = {
            IDQuestionAndAnswer: $scope.SelectedQuestionAnswer.IDQuestionAndAnswer,
            DescriptionByManager: $scope.DescriptionByManager
        };
        QuestionAndAnswerService.DenyByManager(param).then(function (result) {
            $scope.GetDeniedQuestionAnswer();
            $('#ModalShowAnswer').modal('hide');
            AutoClosingSuccessAlert('Deny was successful !', 3000);

        }).catch(function () {
            AutoClosingErrorAlert('Error in Deny !', 5000);

        }).finally(function () {
            ngProgress.complete();
            ngProgress.stop();
        });
    };

}]);
