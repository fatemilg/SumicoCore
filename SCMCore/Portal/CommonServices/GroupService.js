myApp.factory('UserGroupService', function ($http) {
    return {
        GetUserGroupByGroupType: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/UserGroup/GetUserGroupByGroupType/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    }
});




