myApp.factory('OptionInheritanceService', function ($http) {
    return {
        GetOptionInheritance: function () {
            return $http({
                method: 'Post',
                url: '/api/OptionInheritance/GetOptionInheritance/',
                headers: { "Content-Type": "application/json" }
            });
        },
        InitialOptionInheritance: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/OptionInheritance/InitialOptionInheritance/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
    }
});
