myApp.factory('GalleryService', ['$http', function ($http) {
    return {
        FillGalleryDataByIDRet: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/Gallery/FillGalleryByIDRet/',
                data: JSON.stringify(Param),
                headers: { "Gallery-Type": "application/json" }
            });
        },
        FillGalleryByIDGalleryCategory: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/Gallery/FillGalleryByIDGalleryCategory/',
                data: JSON.stringify(Param),
                headers: { "Gallery-Type": "application/json" }
            });
        },
        DeleteGallery: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/Gallery/DeleteGallery/',
                data: JSON.stringify(Param),
                headers: { "Gallery-Type": "application/json" }
            });
        },
        AddGallery: function (Param) {
            return $http({
                method: 'POST',
                url: '/api/Gallery/AddGallery/',
                headers: { "Gallery-Type": "application/json" },
                data: JSON.stringify(Param),
                eventHandlers: {
                    progress: function (c) {
                        console.log('Progress -> ' + c);
                        console.log(c);
                    }
                }
            });
        },
        AddFileToGallery: function (Param) {
            return $http({
                method: 'POST',
                url: '/api/Gallery/AddFileToGallery/',
                headers: { "Gallery-Type": "application/json" },
                data: JSON.stringify(Param),
                eventHandlers: {
                    progress: function (c) {
                        console.log('Progress -> ' + c);
                        console.log(c);
                    }
                }
            });
        },
        UpdateGallery: function (Param) {
            return $http({
                method: 'Post',
                url: '/api/Gallery/UpdateGallery/',
                data: JSON.stringify(Param),
                headers: { "Gallery-Type": "application/json" }
            });
        }
    }
}]);