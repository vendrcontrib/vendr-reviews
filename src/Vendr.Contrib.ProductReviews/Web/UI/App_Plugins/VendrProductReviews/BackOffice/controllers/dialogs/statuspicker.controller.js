(function () {

    'use strict';

    function ReviewStatusPickerController($scope, vendrProductReviewsResource) {

        var defaultConfig = {
            title: "Select a status",
            enableFilter: true,
            orderBy: "sortOrder"
        };

        var vm = this;

        vm.config = angular.extend({}, defaultConfig, $scope.model.config);

        vm.loadItems = function () {
            return vendrProductReviewsResource.getStatuses(vm.config.storeId);
        };

        vm.select = function (item) {
            $scope.model.value = item;
            if ($scope.model.submit) {
                $scope.model.submit($scope.model.value);
            }
        };

        vm.close = function () {
            if ($scope.model.close) {
                $scope.model.close();
            }
        };
    }

    angular.module('vendr').controller('Vendr.ProductReviews.Controllers.ReviewStatusPickerController', ReviewStatusPickerController);

}());