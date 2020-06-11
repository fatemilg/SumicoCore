myApp.controller('QuestionAndAnswerController', ['$scope', 'ngProgress', '$sce', 'QuestionAndAnswerService', 'DefineDetailProductService', '$window', function ($scope, ngProgress, $sce, QuestionAndAnswerService, DefineDetailProductService, $window) {
    $scope.CurrentQA = {};
   

    $scope.FillPartNumbers = function () {

        DefineDetailProductService.GetListOfPartNumbers().then(function (result) {
            $scope.PartNumbers = result.data;
        }).catch(function () {
        }).finally(function () {
        });
    };
    $scope.NewQuestionAndAnswer = function () {
        $('#FormDataQuestionAndAnswer').slideToggle();
        $('#btnNewQuestionAndAnswer').fadeOut();
        $('#btnSaveQuestionAndAnswer').val('Add');
        $('html, body').animate({
            scrollTop: $("#FormDataQuestionAndAnswer").offset().top
        }, 1000);
        $scope.CurrentQA = {};
    };
    $scope.CancelQuestionAndAnswer = function () {
        $('#FormDataQuestionAndAnswer').slideToggle();
        $('#btnNewQuestionAndAnswer').fadeIn();
    };


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
        });
    };
    $scope.GetDeniedQuestionAnswer = function () {
        QuestionAndAnswerService.GetDeniedQuestionAnswer().then(function (result) {
            $scope.QuestionAndAnswers = result.data;
            $scope.changeValueRadioButton = "Denied";
        }).catch(function () {
        }).finally(function () {
        });
    };





    $scope.SaveQuestionAndAnswer = function (_IDQuestionAndAnswer) {
        if ($('#btnSaveQuestionAndAnswer').val() === 'Add') {
            $scope.NewQuestionAndAnswer = {
                IDQuestionAndAnswer: NewGuid(),
                Question: $scope.CurrentQA.Question,
                IDXRet: $scope.CurrentQA.PartNumber,
                MetaTag: $scope.CurrentQA.MetaTag,
                Answer: $scope.CurrentQA.Answer,
                IDLoguser: _IDLogUser,
                Status: 1
            };
            QuestionAndAnswerService.AddQuestionAndAnswer($scope.NewQuestionAndAnswer).then(function (result) {
                $scope.GetNewQuestionAnswer();
                AutoClosingSuccessAlert('Registration was successful !', 3000);
            }).catch(function () {
                AutoClosingErrorAlert('Error in registration !', 5000);
            }).finally(function () {
                $scope.CancelQuestionAndAnswer();
                ngProgress.complete();
                ngProgress.stop();
            });
        }
        else if ($('#btnSaveQuestionAndAnswer').val() === 'Update') {
            $scope.UpdateQuestionAndAnswer = {
                IDQuestionAndAnswer: $scope.CurrentQA.IDQuestionAndAnswer,
                Question: $scope.CurrentQA.Question,
                IDXRet: $scope.CurrentQA.PartNumber,
                MetaTag: $scope.CurrentQA.MetaTag,
                Answer: $scope.CurrentQA.Answer,
                IDLoguser: _IDLogUser,
                Status: 1
            };
            QuestionAndAnswerService.UpdateQuestionAndAnswer($scope.UpdateQuestionAndAnswer).then(function (result) {
                $scope.GetNewQuestionAnswer();
                AutoClosingSuccessAlert('Update was successful !', 3000);
            }).catch(function () {
                AutoClosingErrorAlert('Error in Update !', 5000);
            }).finally(function () {
                $scope.CancelQuestionAndAnswer();
                ngProgress.complete();
                ngProgress.stop();
            });
        }
    };
    $scope.DeleteQuestionAndAnswer = function (_IDQuestionAndAnswer) {
        ngProgress.start();
        $scope.DelQuestionAndAnswerInfo = {
            IDQuestionAndAnswer: _IDQuestionAndAnswer
        };
        var ConfirmDelete = $window.confirm('Are you sure ?');
        if (ConfirmDelete) {
            QuestionAndAnswerService.DeleteQuestionAndAnswer($scope.DelQuestionAndAnswerInfo).then(function (result) {
                $scope.GetNewQuestionAnswer();
                AutoClosingSuccessAlert('Delete was successful !', 3000);
            }).catch(function () {
                AutoClosingErrorAlert('Error in Delete !', 5000);

            }).finally(function () {
                ngProgress.complete();
                ngProgress.stop();
            });
        }
    };
    $scope.EditQuestionAndAnswer = function (_item) {
        $('#FormDataQuestionAndAnswer').slideDown();
        $('#btnNewQuestionAndAnswer').fadeOut();
        $('#btnSaveQuestionAndAnswer').val('Update');
        $scope.CurrentQA.Question = _item.Question;
        $scope.CurrentQA.MetaTag = _item.MetaTag;
        $scope.CurrentQA.Answer = _item.Answer;
        $scope.CurrentQA.PartNumber = _item.IDXDefineDetailProduct;
        $scope.CurrentQA.IDQuestionAndAnswer = _item.IDQuestionAndAnswer;

        $('html, body').animate({
            scrollTop: $("#FormDataQuestionAndAnswer").offset().top
        }, 1000);
    };


    $scope.FillPartNumbers();
    $scope.GetNewQuestionAnswer();

    $scope.ShowAnswerModal = function (_item) {
        $scope.Answer = $sce.trustAsHtml(_item.Answer);
        $scope.SelectedQuestionAnswer = _item;
        $('#ModalShowAnswer').modal('show');
    };
}]);
