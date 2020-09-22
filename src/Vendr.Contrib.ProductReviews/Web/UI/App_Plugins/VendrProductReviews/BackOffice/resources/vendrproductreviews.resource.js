(function() {

    'use strict';

    function vendrProductReviewsResource($http, umbRequestHelper) {

        return {

            getProductReview: function (storeId, id) {
                return umbRequestHelper.resourcePromise(
                    $http.get("/umbraco/backoffice/VendrProductReviews/ProductReviewApi/GetProductReview", { params: { id: id } }),
                    "Failed to get product reviews");
            },

            getProductReviews: function (storeId, ids) {
                return umbRequestHelper.resourcePromise(
                    $http.get("/umbraco/backoffice/VendrProductReviews/ProductReviewApi/GetProductReview", { params: { ids: ids } }),
                    "Failed to get product reviews");
            },

            getPagedProductReviews: function () {
                return umbRequestHelper.resourcePromise(
                    $http.get("/umbraco/backoffice/VendrProductReviews/ProductReviewApi/GetPagedProductReviews"),
                    "Failed to get product reviews");
            },

            getProductReviewsForProduct: function (productReference) {

            },

            getProductReviewsForCustomer: function (customerReference) {

            },

            saveProductReview: function (review) {
                return umbRequestHelper.resourcePromise(
                    $http.post("/umbraco/backoffice/VendrProductReviews/SaveReview", review),
                    "Failed to save review");
            },

            deleteProductReview: function (id) {
                return umbRequestHelper.resourcePromise(
                    $http.delete("/umbraco/backoffice/VendrProductReviews/DeleteReview", { params: { id: id } } ),
                    "Failed to delete review");
            }
        };

    }

    angular.module('vendr.resources').factory('vendrProductReviewsResource', vendrProductReviewsResource);

}());