(function() {

    'use strict';

    function vendrProductReviewsResource($http, umbRequestHelper) {

        return {

            getProductReview: function (id) {

            },

            getProductReviews: function(productReference) {
                
            },

            getProductReviewsForCustomer: function (customerReference) {

            }
        };

    };

    angular.module('vendr.resources').factory('vendrProductReviewsResource', vendrProductReviewsResource);

}());