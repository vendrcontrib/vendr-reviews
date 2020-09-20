(function() {

    'use strict';

    function vendrProductReviewsResource($http, umbRequestHelper) {

        return {

            getProductReview: function (id) {
                
            },

            getProductReviews: function (ids) {
                
            },

            getPagedProductReviews: function () {
                return umbRequestHelper.resourcePromise(
                    $http.get("/umbraco/backoffice/VendrProductReviews/ProductReviewApi/GetPagedProductReviews"),
                    "Failed to get product reviews");
            },

            getProductReviewsForProduct: function (productReference) {

            },

            getProductReviewsForCustomer: function (customerReference) {

            }
        };

    }

    angular.module('vendr.resources').factory('vendrProductReviewsResource', vendrProductReviewsResource);

}());