myApp.controller('UserSiteController', ['$scope', '$rootScope', 'ngProgress', 'UserSiteService', function ($scope, $rootScope, ngProgress, UserSiteService) {
    $scope.GetUserSite = function () {
        ngProgress.start();
        UserSiteService.GetUserSite().then(function (result) {
            $scope.UsersSite = result.data;
            var list = ($scope.UsersSite).filter(function (n) { return n.ShowDate === undefined });
            $rootScope.NewUserSiteNotShown.CountUserNotShown = list.length;

        }).catch(function () {
            AutoClosingErrorAlert('Error in catch data !', 5000);
        }).finally(function () {
            ngProgress.complete();
            ngProgress.stop();
        });
    };
    $scope.GetUserSite();

    $scope.UpdateShowDate = function (_item) {
        var param =
        {
            IDUser: _item.IDUser
        };
        UserSiteService.UpdateShowDate(param).then(function (result) {
            $scope.GetUserSite();
        }).catch(function () {
        }).finally(function () {
        });
    };

}]);