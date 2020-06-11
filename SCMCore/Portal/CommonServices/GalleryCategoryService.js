myApp.factory('GalleryCategoryService', ['$http', function ($http) {
    return {
        GetGalleryCategoryByIDRet: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/GalleryCategory/GetGalleryCategory/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        AddGalleryCategory: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/GalleryCategory/AddGalleryCategory/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        UpdateGalleryCategory: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/GalleryCategory/UpdateGalleryCategory/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        },
        DeleteGalleryCategory: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/GalleryCategory/DeleteGalleryCategory/',
                data: JSON.stringify(Param),
                headers: { "Content-Type": "application/json" }
            });
        }
    }
}]);




