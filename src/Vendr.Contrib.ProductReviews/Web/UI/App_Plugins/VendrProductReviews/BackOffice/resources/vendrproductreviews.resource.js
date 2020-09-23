(function() {

    'use strict';

    function vendrProductReviewsResource($http, umbRequestHelper) {

        return {

            getProductReview: function (id) {
                return umbRequestHelper.resourcePromise(
                    $http.get("/umbraco/backoffice/VendrProductReviews/ProductReviewApi/GetProductReview", { params: { id: id } }),
                    "Failed to get product reviews");
            },

            getProductReviews: function (ids) {
                return umbRequestHelper.resourcePromise(
                    $http.get("/umbraco/backoffice/VendrProductReviews/ProductReviewApi/GetProductReview", { params: { ids: ids } }),
                    "Failed to get product reviews");
            },

            getProductReviewsForProduct: function (storeId, productReference) {
                return umbRequestHelper.resourcePromise(
                    $http.get("/umbraco/backoffice/VendrProductReviews/ProductReviewApi/GetProductReviews", {
                        params: {
                            storeId: storeId,
                            productReference: productReference
                        }
                    }),
                    "Failed to get product reviews for product");
            },

            getProductReviewsForCustomer: function (storeId, customerReference) {
                return umbRequestHelper.resourcePromise(
                    $http.get("/umbraco/backoffice/VendrProductReviews/ProductReviewApi/GetProductReviewsForCustomer", {
                        params: {
                            storeId: storeId,
                            customerReference: customerReference
                        }
                    }),
                    "Failed to get product reviews for customer");
            },

            searchProductReviews: function (storeId, opts) {

                var params = angular.extend({}, {
                    storeId: storeId
                }, opts);

                return umbRequestHelper.resourcePromise(
                    $http.get("/umbraco/backoffice/VendrProductReviews/ProductReviewApi/SearchProductReviews", { params: params }),
                    "Failed to search reviews");
            },

            saveProductReview: function (review) {
                return umbRequestHelper.resourcePromise(
                    $http.post("/umbraco/backoffice/VendrProductReviews/ProductReviewApi/SaveReview", review),
                    "Failed to save review");
            },

            deleteProductReview: function (id) {
                return umbRequestHelper.resourcePromise(
                    $http.delete("/umbraco/backoffice/VendrProductReviews/ProductReviewApi/DeleteReview", { params: { id: id } } ),
                    "Failed to delete review");
            }
        };

    }

    angular.module('vendr.resources').factory('vendrProductReviewsResource', vendrProductReviewsResource);

}());