(function () {

    'use strict';

    function vendrProductReviewsDashboardController($scope, vendrProductReviewsResource) {

        var vm = this;
        vm.loading = false;
        vm.reviews = [];

        function init() {

            // http://angular-tips.com/blog/2015/10/creating-a-rating-directive-in-angular-2/
            // https://jsfiddle.net/n2h05z7e/3/

            vendrProductReviewsResource.getPagedProductReviews().then(function (data) {
                console.log("data", data);
                vm.reviews = data;
            });
        }


        init();
    }

    angular.module('vendr').controller('Vendr.ProductReviews.Controllers.DashboardController', vendrProductReviewsDashboardController);

}());