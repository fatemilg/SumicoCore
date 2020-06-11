
myApp.controller('IndexController', ['$scope', '$rootScope', '$window', '$cookies', 'MenuService', 'EventUserService', 'PersonelService', 'UserSiteService', 'PeyvastStockService', 'MaterialListPreparationService', function ($scope, $rootScope, $window, $cookies, MenuService, EventUserService, PersonelService, UserSiteService, PeyvastStockService, MaterialListPreparationService) {
    $scope.OpenIndexSetting = function () {
        $('#ulIndexSetting').slideToggle();
    }
    $scope.OpenIndexSetting = function () {
        $('#ulIndexSetting').slideToggle();

    }
    $scope.ToggleSettingAdmin = function () {
        $('#SettingAdmin').slideToggle();

    }
    $('#chkAllowAccessLevel').bootstrapToggle();
    $scope.CurrentButton_Title_En = '';
    $scope.CurrentButton_Title_Fa = '';
    $scope.initialAccessLevel = function () {
        $scope.AccessLevel = false;
    }
    $scope.AllowAccessLevel = function (check) {
        $scope.AccessLevel = !check;
        var obs = document.getElementsByClassName("obs");
        if ($scope.AccessLevel) {
            for (var item in obs) {
                var ItemsForAccess = obs[item].id;
                if (ItemsForAccess != undefined) {
                    $("#" + ItemsForAccess).addClass("AccessMood");
                    var ThisItem = $('#' + ItemsForAccess);
                    $('#' + ItemsForAccess + ':disabled').wrap(function () {
                        return '<div id="' + ItemsForAccess + '" style="display:inline-block;" onmouseover= HoverAccessMenu(this) />';
                    });
                    ThisItem.attr('onmouseover', 'HoverAccessMenu(this)');
                }
            }
        }
        else {
            for (var item in obs) {
                var ItemsForAccess = obs[item].id;
                $("#" + ItemsForAccess).removeClass("AccessMood");
                var ThisItem = $('#' + ItemsForAccess);
                ThisItem.removeAttr('onmouseover');
                $('#HoverAccessMenu').slideUp(100);
            }
        }
    }
    $scope.GetPersonelList = function () {
        var Param = {
            MenuUrl: window.location.href,
            Title_En: $scope.CurrentButton_Title_En
        };
        MenuService.GetPersonelOfMenuData(Param).then(function (result) {
            $scope.Personels = result.data;
        }).catch(function () {

        }).finally(function () {

        });
    }
    $scope.SaveEventUser = function (_Item, elem) {
        var Param = {
            IDEventUser: _Item.IDEventUser,
            IDMenu: _Item.IDMenu,
            IDUser: _Item.IDUser,
            Title_En: $scope.CurrentButton_Title_En,
            Title_Fa: $scope.CurrentButton_Title_Fa
        };
        EventUserService.AddEventUser(Param).then(function (result) {
        }).catch(function () {

        }).finally(function () {

        });
    }
    $scope.GetPersonel_ByIDUser = function () {
        var param = {
            IDLogUser: _IDLogUser
        }
        PersonelService.GetPersonel(param).then(function (result) {
            $rootScope.Personel = result.data[0];
        }).catch(function () {
            AutoClosingErrorAlert('Error in catch data !', 5000);

        }).finally(function () {

        });
    }
    $scope.LogOut = function () {
        $cookies.remove('IDLogUser', { path: '/' });
        $cookies.remove('UserName', { path: '/' });
        $window.location.href = "/Admin/Login.aspx";
    }

    $scope.GetPersonel_ByIDUser();

    $scope.GetCountNewUserSiteNotShown = function ()
    {
        UserSiteService.GetCountNewUserSiteNotShown().then(function (result) {
            $rootScope.NewUserSiteNotShown = result.data[0];
        }).catch(function () {
            AutoClosingErrorAlert('Error in catch data !', 5000);

        }).finally(function () {

        });
    }
    $scope.GetCountNewUserSiteNotShown();

    $scope.GetCountNewMaterialListCreatedNotShown = function () {
        MaterialListPreparationService.GetCountNewMaterialListCreatedNotShown().then(function (result) {
            $scope.NewMaterialListCreatedNotShown = result.data[0];
        }).catch(function () {
            AutoClosingErrorAlert('Error in catch data !', 5000);

        }).finally(function () {

        });
    }
    $scope.GetCountNewMaterialListCreatedNotShown();

    $scope.GetPeyvastStock = function ()
    {
        PeyvastStockService.GetPeyvastStock().then(function (result) {
            $scope.PeyvastStock = result.data[0];
        }).catch(function () {
            AutoClosingErrorAlert('Error in catch data !', 5000);

        }).finally(function () {

        });
    }
    $scope.GetPeyvastStock();
}]);


function HoverAccessMenu(element) {
    angular.element(document.getElementById('HoverAccessMenu')).scope().CurrentButton_Title_En = element.id;
    angular.element(document.getElementById('HoverAccessMenu')).scope().CurrentButton_Title_Fa = element.getAttribute("data-original-title");
    angular.element(document.getElementById('HoverAccessMenu')).scope().GetPersonelList();
    $('#HoverAccessMenu').hide();
    if (event.clientX > 300) {
        var left = event.clientX - 300 + "px";
    }
    else {
        var left = event.clientX + "px";
    }
    var top = event.clientY + 10 + "px";
    var div = document.getElementById('HoverAccessMenu');
    div.style.left = left;
    div.style.top = top;
    $('#HoverAccessMenu').slideDown(100);
}

function CloseHoverAccessMenu() {
    $('#HoverAccessMenu').slideUp(100);
}
