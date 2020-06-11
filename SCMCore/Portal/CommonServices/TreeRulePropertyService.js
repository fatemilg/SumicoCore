myApp.factory('TreeRulePropertyService', function ($http) {
    return {
        GetTreeRuleProperty: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/TreeRuleProperty/GetTreeRulePropertyByIDProduct/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        AddTreeRuleProperty: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/TreeRuleProperty/AddTreeRuleProperty/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    }
});




