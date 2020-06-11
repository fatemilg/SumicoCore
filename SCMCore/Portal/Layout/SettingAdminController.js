myApp.controller('SettingAdminController', ['$scope', 'MenuService','EventUserService', function ($scope, MenuService, EventUserService) {
    $('#chkAllowAccessLevel').bootstrapToggle();
    $scope.CurrentButton = '';
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

                    ThisItem.attr('onmouseover', 'HoverAccessMenu(this)');

                }
            }
        }
        else {
            for (var item in obs) {
                var ItemsForAccess = obs[item].id;

                $("#" + ItemsForAccess).removeClass("AccessMood");
            }
        }
    }
    $scope.ToggleSettingAdmin = function () {
        $('#SettingAdmin').slideToggle();
    }
    $scope.GetPersonelList = function () {
        var Param = {
            MenuUrl: window.location.href
        };
        MenuService.GetPersonelOfMenuData(Param).then(function (result) {
            $scope.Personels = result.data;
        }).catch(function () {

        }).finally(function () {

        });
    }
    $scope.SaveEventUser = function (_Item, elem) {
        var Param = {
            IDMenu: _Item.IDMenu,
            IDUser: _Item.IDUser,
            Title_En: $scope.CurrentButton
        };
        EventUserService.AddEventUser(Param).then(function (result) {
        }).catch(function () {

        }).finally(function () {

        });
    }

}]);

function HoverAccessMenu(element) {
    angular.element(document.getElementById('HoverAccessMenu')).scope().CurrentButton = element.id;
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
    $('#HoverAccessMenu').slideDown();
}

function CloseHoverAccessMenu() {
    $('#HoverAccessMenu').slideUp();
}