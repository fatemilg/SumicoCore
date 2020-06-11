///Routing
var myApp = angular.module('myApp', ['ngRoute', 'ngFileUpload', 'ng-sortable', 'angular-page-loader', 'froala', 'AxelSoft', 'ngProgress', 'ngCookies', 'rzTable', 'angularUtils.directives.dirPagination', 'angular-iran-national-id']);
myApp.value('froalaConfig', {
    placeholderText: 'Edit Your Content Here!',
    heightMin: 300,

    // Set the image upload URL.
    imageUploadURL: '/api/FroalaUploader/UploadImage/',
    // Set the image upload URL.
    imageManagerLoadURL: '/api/FroalaUploader/LoadImageManagerList/',
    // Set the image delete URL.
    imageManagerDeleteURL: '/api/FroalaUploader/DeleteImage/',
    // Set request type.
    imageUploadMethod: 'POST',
    // Set request type.
    imageManagerLoadMethod: 'POST',
    // Set max image size to 5MB.
    imageMaxSize: 20 * 1024 * 1024,
    // Allow to upload PNG and JPG.
    imageAllowedTypes: ['jpeg', 'jpg', 'png', 'gif', 'bmp'],

    // Set the Video upload URL.
    videoUploadURL: '/api/FroalaUploader/UploadVideo/',
    videoAllowedTypes: ['mp4'],
    videoDefaultDisplay: 'inline-block',
    videoDefaultAlign: 'center',
    videoDefaultWidth: 500,
    videoEditButtons: ['videoDisplay', 'videoRemove'],
    videoMaxSize: 1000 * 1024 * 1024
});

myApp.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    //$locationProvider.html5Mode({
    //    enabled: true,
    //    requireBase: false
    //});
    $routeProvider
        .when('/ApplicationForm',
            {
                templateUrl: '/Portal/ApplicationForm/ApplicationForm.html?v=7.6',
                resolve: {
                    init: function ($rootScope, EventUserService) {
                        GetEventUser_ByIDUser($rootScope, EventUserService);
                    }
                }
            })
        .when('/Content',
            {
                templateUrl: '/Portal/Content/Content.html?v=7.6',
                resolve: {
                    init: function ($rootScope, EventUserService) {
                        GetEventUser_ByIDUser($rootScope, EventUserService);
                    }
                }
            })
        .when('/QuestionAndAnswer',
            {
                templateUrl: '/Portal/QuestionAndAnswer/QuestionAndAnswer.html?v=7.6',
                resolve: {
                    init: function ($rootScope, EventUserService) {
                        GetEventUser_ByIDUser($rootScope, EventUserService);
                    }
                }
            })
        .when('/QuestionAndAnswerCheck',
            {
                templateUrl: '/Portal/QuestionAndAnswerCheck/QuestionAndAnswerCheck.html?v=7.6',
                resolve: {
                    init: function ($rootScope, EventUserService) {
                        GetEventUser_ByIDUser($rootScope, EventUserService);
                    }
                }
            })
        .when('/PurchaseOrder',
            {
                templateUrl: '/Portal/PurchaseOrder/PurchaseOrder.html?v=7.6',
                resolve: {
                    init: function ($rootScope, EventUserService) {
                        GetEventUser_ByIDUser($rootScope, EventUserService);
                    }
                }
            })
        .when('/MasterTable',
            {
                templateUrl: '/Portal/MasterTable/MasterTable.html?v=7.6',
                resolve: {
                    init: function ($rootScope, EventUserService) {
                        GetEventUser_ByIDUser($rootScope, EventUserService);
                    }
                }
            })
        .when('/SellsView',
            {
                templateUrl: '/Portal/SellsView/SellsView.html?v=7.6',
                resolve: {
                    init: function ($rootScope, EventUserService) {
                        GetEventUser_ByIDUser($rootScope, EventUserService);
                    }
                }
            })
        .when('/FileManager',
            {
                templateUrl: '/Portal/FileManager/FileManager.html?v=7.6',
                resolve: {
                    init: function ($rootScope, EventUserService) {
                        GetEventUser_ByIDUser($rootScope, EventUserService);
                    }
                }
            })
        .when('/Price',
            {
                templateUrl: '/Portal/Price/Price.html?v=7.6',
                resolve: {
                    init: function ($rootScope, EventUserService) {
                        GetEventUser_ByIDUser($rootScope, EventUserService);
                    }
                }
            })
        .when('/TrainingCourse',
            {
                templateUrl: '/Portal/TrainingCourse/TrainingCourse.html?v=7.6',
                resolve: {
                    init: function ($rootScope, EventUserService) {
                        GetEventUser_ByIDUser($rootScope, EventUserService);
                    }
                }
            })
        .when('/PersonelInCompany',
            {
                templateUrl: '/Portal/PersonelInCompany/PersonelInCompany.html?v=7.6',
                resolve: {
                    init: function ($rootScope, EventUserService) {
                        GetEventUser_ByIDUser($rootScope, EventUserService);
                    }
                }
            })
        .when('/POHistory',
            {
                templateUrl: '/Portal/POHistory/POHistory.html?v=7.6',
                resolve: {
                    init: function ($rootScope, EventUserService) {
                        GetEventUser_ByIDUser($rootScope, EventUserService);
                    }
                }
            })
        .when('/UserSite',
            {
                templateUrl: '/Portal/UserSite/UserSite.html?v=7.6',
                resolve: {
                    init: function ($rootScope, EventUserService) {
                        GetEventUser_ByIDUser($rootScope, EventUserService);
                    }
                }
            })
        .when('/MaterialListPreparation',
            {
                templateUrl: '/Portal/MaterialListPreparation/MaterialListPreparation.html?v=7.6',
                resolve: {
                    init: function ($rootScope, EventUserService) {
                        GetEventUser_ByIDUser($rootScope, EventUserService);
                    }
                }
            })
        .when('/QuestionCustomer',
            {
                templateUrl: '/Portal/QuestionCustomer/QuestionCustomer.html?v=7.6',
                resolve: {
                    init: function ($rootScope, EventUserService) {
                        GetEventUser_ByIDUser($rootScope, EventUserService);
                    }
                }
            })
        .when('/SecondaryContentCategory',
            {
                templateUrl: '/Portal/SecondaryContentCategory/SecondaryContentCategory.html?v=7.6',
                resolve: {
                    init: function ($rootScope, EventUserService) {
                        GetEventUser_ByIDUser($rootScope, EventUserService);
                    }
                }
            })
        .when('/Dictionary',
            {
                templateUrl: '/Portal/Dictionary/Dictionary.html?v=7.6',
                resolve: {
                    init: function ($rootScope, EventUserService) {
                        GetEventUser_ByIDUser($rootScope, EventUserService);
                    }
                }
            })
        .when('/TreeRuleProperty',
            {
                templateUrl: '/Portal/TreeRuleProperty/TreeRuleProperty.html?v=7.6',
                resolve: {
                    init: function ($rootScope, EventUserService) {
                        GetEventUser_ByIDUser($rootScope, EventUserService);
                    }
                }
            })
        .when('/MaterialListDetail/:IDUser',
            {
                templateUrl: '/Portal/MaterialListDetail/MaterialListDetail.html?v=7.6',
                resolve: {
                    init: function ($rootScope, EventUserService) {
                        GetEventUser_ByIDUser($rootScope, EventUserService);
                    }
                }
            }).when('/DynamicForm',
            {
                templateUrl: '/Portal/DynamicForm/DynamicForm.html?v=7.6',
                resolve: {
                    init: function ($rootScope, EventUserService) {
                        GetEventUser_ByIDUser($rootScope, EventUserService);
                    }
                }
            }).when('/Incident',
            {
                templateUrl: '/Portal/Incident/Incident.html?v=7.6',
                resolve: {
                    init: function ($rootScope, EventUserService) {
                        GetEventUser_ByIDUser($rootScope, EventUserService);
                    }
                }
            });
    //.otherwise(
    //    { redirectTo: '/Content' });

}]);
myApp.run(function ($rootScope) {
    $rootScope.ClientUrl = getCookie("ClientUrl");
});
function GetEventUser_ByIDUser($rootScope, EventUserService) {
    var Param = {
        IDLogUser: _IDLogUser,
        MenuUrl: window.location.href
    };
    EventUserService.GetEventUser_ByIDUser(Param).then(function (result) {
        $rootScope.EventUser = result.data;
    }).catch(function (result) {
    }).finally(function () {
    });
}
var _IDLogUser = getCookie("IDLogUser");
if (!_IDLogUser) {
    window.location = "/Admin/Login.aspx";
}











