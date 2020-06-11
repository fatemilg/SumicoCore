myApp.controller('MaterialListPreparationController', ['$scope', 'ngProgress', 'MaterialListPreparationService', 'UserSiteService', 'PersonelInCompanyService', function ($scope, ngProgress, MaterialListPreparationService, UserSiteService, PersonelInCompanyService) {
    $scope.GetNewMaterialListCreatedByCustomer = function () {
        ngProgress.start();
        MaterialListPreparationService.GetNewMaterialListCreatedByCustomer().then(function (result) {
            $scope.NewMaterialListCreatedByCustomer = result.data;

        }).catch(function () {
            AutoClosingErrorAlert('Error in catch data !', 5000);
        }).finally(function () {
            ngProgress.complete();
            ngProgress.stop();
        });
    };
    $scope.GetNewMaterialListCreatedByCustomer();

    $scope.ShowListOfMaterialsCreatedByCustomer = function (_Item) {
        $scope.CustomerSelected = _Item;
        var param = {
            IDUser: _Item.IDUser
        };
        UserSiteService.CheckPersonelInCompanyExistInUserSite(param).then(function (result) {
            if (result.data[0].IDPersonelInCompany === undefined) {
                var param = {
                    FName_Fa: _Item.FName,
                    LName_Fa: _Item.LName,
                    NationalCode: _Item.NationalCode
                };
                PersonelInCompanyService.GetPersonelInCompanyByNationalCodeAndFullName(param).then(function (result) {
                    $scope.PersonelInCompanyFounds = result.data;

                    var list = ($scope.PersonelInCompanyFounds).filter(function (n) { return n.NationalCode === _Item.NationalCode });
                    if (list[0] === undefined) {//National Code not exist in List
                        $scope.ShowBtnAddIntoCrm = true; // should Add into Crm
                    }
                    else {
                        $scope.ShowBtnAddIntoCrm = false; // should integrate 
                    }

                }).catch(function () {
                    AutoClosingErrorAlert('Error in catch data !', 5000);
                }).finally(function () {
                    ngProgress.complete();
                    ngProgress.stop();
                });
                $('#ModalIntegrateUser').modal('show');

            }
            else {
                window.location = "/Portal/Layout/#!/MaterialListDetail/" + $scope.CustomerSelected.IDUser;
            }

        }).catch(function () {
            AutoClosingErrorAlert('Error in catch data !', 5000);
        }).finally(function () {
        });
    };
    $scope.AddIntoPersonelInCompanyAsNew = function () {
        UserSiteService.AddUserSiteIntoPersonelInCompany($scope.CustomerSelected).then(function (result) {
            AutoClosingSuccessAlert('Add into CRM was successful !', 5000);
            $('#ModalIntegrateUser').modal('hide');
        }).catch(function () {
            AutoClosingErrorAlert('Error in Add into CRM!', 5000);
        }).finally(function () {

        });
    };
    $scope.IntegratePersonelInComanyAndUserSite = function (_IDPersonelInCompany) {
        var param = {
            IDPersonelInCompany: _IDPersonelInCompany,
            IDUser: $scope.CustomerSelected.IDUser
        };
        UserSiteService.UpdaIDPersonelInCompany(param).then(function (result) {
            AutoClosingSuccessAlert('Integration done!', 5000);
            $('#ModalIntegrateUser').modal('hide');
            window.location = "/Portal/Layout/#!/MaterialListDetail/" + $scope.CustomerSelected.IDUser;
        }).catch(function () {
            AutoClosingErrorAlert('Error in Integrate !', 5000);
        }).finally(function () {

        });
    };

}]);