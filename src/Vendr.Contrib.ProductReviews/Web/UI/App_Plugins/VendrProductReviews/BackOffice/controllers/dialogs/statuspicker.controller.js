(function () {

    'use strict';

    function ReviewStatusPickerController($scope, $q,
        vendrOrderStatusResource) {
        var defaultConfig = {
            title: "Select a status",
            enableFilter: true,
            orderBy: "sortOrder"
        };

        var vm = this;

        vm.config = angular.extend({}, defaultConfig, $scope.model.config);

        vm.loadItems = function () {

            var items = [
                {
                    alias: "pending",
                    color: "light-blue",
                    icon: "icon-time",
                    id: 0, //"37cd2c8f-48d8-4416-bb37-b2c7d7bb992f",
                    name: "Pending",
                    sortOrder: 0
                    //storeId: "b1e61994-b83b-420a-903e-63a7a15942dc"
                },
                {
                    alias: "approved",
                    color: "green",
                    icon: "icon-check",
                    id: 1, //"37cd2c8f-48d8-4416-bb37-b2c7d7bb992f",
                    name: "Approved",
                    sortOrder: 1
                    //storeId: "b1e61994-b83b-420a-903e-63a7a15942dc"
                },
                {
                    alias: "declined",
                    color: "grey",
                    icon: "icon-block",
                    id: 2, //"37cd2c8f-48d8-4416-bb37-b2c7d7bb992f",
                    name: "Declined",
                    sortOrder: 2
                    //storeId: "b1e61994-b83b-420a-903e-63a7a15942dc"
                }
            ];

            return $q.resolve(items);
            //return vendrOrderStatusResource.getOrderStatuses(vm.config.storeId);
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