(function () {

    'use strict';

    function vendrProductReviewsDashboardController($scope, $location, appState, vendrProductReviewsResource) {

        var vm = this;

        vm.page = {};
        vm.page.loading = true;

        vm.page.menu = {};
        vm.page.menu.currentSection = appState.getSectionState("currentSection");
        vm.page.menu.currentNode = null;

        vm.page.breadcrumb = {};
        vm.page.breadcrumb.items = [];
        vm.page.breadcrumb.itemClick = function (ancestor) {
            $location.path(ancestor.routePath);
        };

        vm.options = {
            createActions: [],
            //filters: [
            //    {
            //        name: 'Order Status',
            //        alias: 'orderStatusIds',
            //        localStorageKey: 'store_' + storeId + '_orderStatusFilter',
            //        getFilterOptions: function () {
            //            return vendrRouteCache.getOrFetch("store_" + storeId + "_orderStatuses", function () {
            //                return vendrOrderStatusResource.getOrderStatuses(storeId);
            //            })
            //                .then(function (items) {
            //                    return items.map(function (itm) {
            //                        return {
            //                            id: itm.id,
            //                            name: itm.name,
            //                            color: itm.color
            //                        };
            //                    });
            //                });
            //        }
            //    },
            //    {
            //        name: 'Payment Status',
            //        alias: 'paymentStatuses',
            //        localStorageKey: 'store_' + storeId + '_paymentStatusFilter',
            //        getFilterOptions: function () {
            //            return $q.resolve([
            //                { id: 1, name: 'Authorized', color: 'light-blue' },
            //                { id: 2, name: 'Captured', color: 'green' },
            //                { id: 3, name: 'Cancelled', color: 'grey' },
            //                { id: 4, name: 'Refunded', color: 'orange' },
            //                { id: 5, name: 'Pending', color: 'deep-purple' },
            //                { id: 200, name: 'Error', color: 'red' }
            //            ]);
            //        }
            //    }
            //],
            //bulkActions: [
            //    {
            //        name: 'Delete',
            //        icon: 'icon-trash',
            //        doAction: function (bulkItem) {
            //            return vendrOrderResource.deleteOrder(bulkItem.id);
            //        },
            //        getConfirmMessage: function (total) {
            //            return $q.resolve("Are you sure you want to delete " + total + " " + (total > 1 ? "items" : "item") + "?");
            //        }
            //    }
            //],
            items: [],
            itemProperties: [
                { alias: 'name', template: '<span class="vendr-table-cell-value--multiline"><span>{{customerFullName}}</span><span class="vendr-table-cell-label">#{{orderNumber}}</span></span>' },
                { alias: 'finalizedDate', header: 'Date', template: "{{ finalizedDate  | date : 'MMMM d, yyyy h:mm a' }}" },
                { alias: 'orderStatusId', header: 'Order Status', align: 'right', template: '<span class="umb-badge umb-badge--xs vendr-bg--{{ orderStatus.color }}" title="Order Status: {{ orderStatus.name }}">{{ orderStatus.name }}</span>' },
                { alias: 'paymentStatus', header: 'Payment Status', align: 'right', template: '<span class="umb-badge umb-badge--xs vendr-badge--{{ paymentStatus.toLowerCase() }}">{{paymentStatusName}}</span>' },
                { alias: 'payment', header: 'Payment', align: 'right', template: '<span class="vendr-table-cell-value--multiline"><strong>{{totalPrice}}</strong><span>{{paymentMethod.name}}</span></span>' }
            ],
            itemClick: function (itm) {
                $location.path(itm.routePath);
            }
        };

        vm.reviews = [];

        function init() {

            // http://angular-tips.com/blog/2015/10/creating-a-rating-directive-in-angular-2/
            // https://jsfiddle.net/n2h05z7e/3/

            vendrProductReviewsResource.getPagedProductReviews().then(function (data) {
                console.log("data", data);
                vm.reviews = data.items;
            });
        }


        init();
    }

    angular.module('vendr').controller('Vendr.ProductReviews.Controllers.DashboardController', vendrProductReviewsDashboardController);

}());