(function () {

    'use strict';

    function ReviewDetailsEditController($scope, localizationService, vendrProductReviewsResource) {

        var defaultConfig = {
            title: "Edit product review"
        };

        var vm = this;

        vm.config = angular.extend({}, defaultConfig, $scope.model.config);

        vm.content = {};

        function onInit() {
            
            if (!$scope.model.title) {
                localizationService.localize("general_edit").then(function (value) {
                    $scope.model.title = value;
                });
            }

            vendrProductReviewsResource.getProductReview(vm.config.reviewId).then(function (review) {
                vm.content = review;
            });
        }

        vm.select = function (item) {
            //$scope.model.value = item;
            if ($scope.model.submit) {
                $scope.model.submit($scope.model.value);
            }
        };

        vm.close = function () {
            if ($scope.model.close) {
                $scope.model.close();
            }
        };

        onInit();
    }

    angular.module('vendr').controller('Vendr.ProductReviews.Controllers.ReviewDetailsEditController', ReviewDetailsEditController);

}());