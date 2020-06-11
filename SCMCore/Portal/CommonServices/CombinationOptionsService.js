myApp.factory('CombinationOptionsService', function ($http) {
    return {
        GetCombinationOptionsByIDSeller: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/CombinationOptions/GetCombinationOptionsByIDSeller/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        GetCountCombinationOptions: function () {
            return $http({
                method: 'Post',
                url: '/api/CombinationOptions/GetCountCombinationOptions/',
                headers: { "Content-Type": "application/json" }
            });
        },
        GetRestOtItemsNotExistInRootByIDOptionInheritance: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/CombinationOptions/GetRestOtItemsNotExistInRootByIDOptionInheritance/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        GetRestOtItemsNotExistInRoot: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/CombinationOptions/GetRestOtItemsNotExistInRoot/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        GetRestOtItemsNotExistForOtherLevel: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/CombinationOptions/GetRestOtItemsNotExistForOtherLevel/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        AddCombinationOptions: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/CombinationOptions/AddCombinationOptions/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        InitialCombinationOptions: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/CombinationOptions/InitialCombinationOptions/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        AddCombinationOptionsForRoot: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/CombinationOptions/AddCombinationOptionsForRoot/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        AddCombinationOptionsForNextLevels: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/CombinationOptions/AddCombinationOptionsForNextLevels/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        DeleteCombinationOptionsBYIDCombinationOptions: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/CombinationOptions/DeleteCombinationOptionsBYIDCombinationOptions/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        DeleteAllCombination: function () {
            return $http({
                method: 'Post',
                url: '/api/CombinationOptions/DeleteAllCombination/',
                headers: { "Content-Type": "application/json" }
            });
        }
    }
});

