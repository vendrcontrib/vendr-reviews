(function () {

    'use strict';

    function vendrProductReviewsDashboardController($scope, vendrProductReviewsResource) {

        var vm = this;
        vm.loading = false;
        vm.reviews = [];

        function init() {

            vendrProductReviewsResource.getPagedProductReviews().then(function (data) {
                console.log("data", data);
                vm.reviews = data;
            });
        }


        init();
    }

    angular.module('vendr').controller('Vendr.ProductReviews.Controllers.DashboardController', vendrProductReviewsDashboardController);

}());