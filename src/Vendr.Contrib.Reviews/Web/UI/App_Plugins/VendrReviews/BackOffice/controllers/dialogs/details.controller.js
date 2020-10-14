(function () {

    'use strict';

    function ReviewDetailsEditController($scope, localizationService, vendrReviewsResource) {

        var defaultConfig = {};

        var vm = this;

        vm.config = angular.extend({}, defaultConfig, $scope.model.config);

        vm.content = angular.copy($scope.model.review) || {};

        function onInit() {
            
            if (!$scope.model.title) {
                localizationService.localize("general_edit").then(function (value) {
                    $scope.model.title = value;
                });
            }
        }

        vm.submit = function () {
            $scope.model.review = vm.content;

            if ($scope.model.submit) {
                $scope.model.submit($scope.model);
            }
        };

        vm.close = function () {
            if ($scope.model.close) {
                $scope.model.close();
            }
        };

        onInit();
    }

    angular.module('vendr').controller('Vendr.Reviews.Controllers.ReviewDetailsEditController', ReviewDetailsEditController);

}());