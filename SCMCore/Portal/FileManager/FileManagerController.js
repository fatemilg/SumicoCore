myApp.controller('FileManagerController', ['$scope', 'AttachInterfaceCategoryService', function ($scope, AttachInterfaceCategoryService) {

    $scope.GetFolders = function () {
        AttachInterfaceCategoryService.GetFolders().then(function (result) {
            $scope.AttachInterfaceCategories = result.data;

        }).catch(function () {

        }).finally(function () {

        });

    }

    $scope.GetFolders();





    //$scope.ChangeAttachCrmInterfaceCategory = function (_IDParent, _IDAttachInterfaceCategory) {
    //    var param = {
    //        IDAttachInterfaceCategory: _IDAttachInterfaceCategory,
    //        IDParent: _IDParent
    //    };
    //    AttachInterfaceCategoryService.ChangeParentFolders(param).then(function (result) {


    //    }).catch(function () {
    //        AutoClosingErrorAlert('اشکال در جابجایی!', 3000);
    //        $scope.GetFolders();
    //    }).finally(function () {
    //    });

    //};

    $scope.ToggleCollpase = function (event, ID) {

        $(event.currentTarget).parent().find('#List' + ID).slideToggle();
        $(event.currentTarget).parent().find('#List' + ID).toggleClass('OpenFolder CloseFolder');


        $(event.currentTarget).parent().find('#icon' + ID).toggleClass('fa-folder fa-folder-open');;
    }

    $scope.LoadRightSide = function (_IDAttachInterfaceCategory) {
        $scope.LoadBreadcrumbs(_IDAttachInterfaceCategory);
        $scope.ListFoldersFiles(_IDAttachInterfaceCategory);
        $scope.SelectAll = false;

    }
    $scope.LoadBreadcrumbs = function (_IDAttachInterfaceCategory) {
        var param = {
            IDAttachInterfaceCategory: _IDAttachInterfaceCategory
        };
        AttachInterfaceCategoryService.LoadBreadcrumbs(param).then(function (result) {
            $scope.Breadcrumbs = result.data;

        }).catch(function () {

        }).finally(function () {

        });
    }
    $scope.ListFoldersFiles = function (_IDAttachInterfaceCategory) {
        var param = {
            IDAttachInterfaceCategory: _IDAttachInterfaceCategory
        };
        AttachInterfaceCategoryService.ListFoldersFiles(param).then(function (result) {
            $scope.FoldersFiles = result.data;

        }).catch(function () {

        }).finally(function () {

        });
    }

    $scope.SelectAllItems = function () {
        $scope.SelectOne = $scope.SelectAll;
        $('.SelectOne').prop('checked', $scope.SelectAll);
    }

    $scope.SelectOneItem = function (SelectOne) {
        if (!SelectOne) {
            $scope.SelectAll = false;
            $('.SelectAll').prop('checked', false);
          
        }
    }
}]);


