(function () {

    'use strict';

    function vendrProductReviewListController($scope, $routeParams, $location, $q, appState, vendrProductReviewsResource, navigationService, vendrUtils, vendrLocalStorage) {

        var compositeId = vendrUtils.parseCompositeId($routeParams.id);
        var storeId = compositeId[0];
        console.log("store id", storeId);
        console.log("$routeParams", $routeParams);

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
            filters: [
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
                {
                    name: 'Status',
                    alias: 'reviewStatuses',
                    localStorageKey: 'store_' + storeId + '_reviewStatusFilter',
                    getFilterOptions: function () {
                        return $q.resolve([
                            { id: 1, name: 'Approved', color: 'green' },
                            { id: 2, name: 'Declined', color: 'grey' },
                            { id: 3, name: 'Pending', color: 'light-blue' }
                        ]);
                    }
                }
            ],
            bulkActions: [
                {
                    name: 'Delete',
                    icon: 'icon-trash',
                    doAction: function (bulkItem) {
                        //return vendrProductReviewsResource.deleteReview(bulkItem.id);
                    },
                    getConfirmMessage: function (total) {
                        return $q.resolve("Are you sure you want to delete " + total + " " + (total > 1 ? "items" : "item") + "?");
                    }
                }
            ],
            items: [],
            itemProperties: [
                {
                    alias: 'rating', header: 'Rating', template: `<span class="vendr-table-cell-value--multiline" title="Rating: {{rating}}">
                        <span class="rating" aria-hidden="true">
                            <i class="icon-rate {{rating < 1 ? 'dn' : ''}}"></i>
                            <i class="icon-rate {{rating < 2 ? 'dn' : ''}}"></i>
                            <i class="icon-rate {{rating < 3 ? 'dn' : ''}}"></i>
                            <i class="icon-rate {{rating < 4 ? 'dn' : ''}}"></i>
                            <i class="icon-rate {{rating < 5 ? 'dn' : ''}}"></i>
                        </span><span>{{rating}}</span></span>` },
                { alias: 'review', header: 'Review', template: '<span class="db bold">{{title}}</span><span class="vendr-table-cell-label">{{description}}</span>' },
                { alias: 'createDate', header: 'Date', template: "{{ createDate | date : 'MMMM d, yyyy h:mm a' }}" },
                { alias: 'status', header: 'Status', align: 'right', template: '<span class="umb-badge umb-badge--xs vendr-bg--blue" title="Status: {{ status }}">{{ status }}</span>' }
            ],
            itemClick: function (itm) {
                $location.path(itm.routePath);
            }
        };

        var hasFilterRouteParams = false;

        vm.options.filters.forEach(fltr => {
            Object.defineProperty(fltr, "value", {
                get: function () {
                    return vendrLocalStorage.get(fltr.localStorageKey) || [];
                },
                set: function (value) {
                    vendrLocalStorage.set(fltr.localStorageKey, value);
                }
            });

            // Initially just check to see if any of the filter are in the route params
            // as if they are, we will reset filters accordingly in a moment, but we
            // need to know if any params exist as we'll wipe out anything that isn't
            // in the querystring
            if ($routeParams[fltr.alias])
                hasFilterRouteParams = true;
        });

        // If we have some filters in the querystring then
        // set the filter values by default, wiping out any
        // cached value they previously had
        if (hasFilterRouteParams) {
            vm.options.filters.forEach(fltr => {
                if ($routeParams[fltr.alias]) {
                    fltr.value = $routeParams[fltr.alias].split(",");
                    $location.search(fltr.alias, null);
                } else {
                    fltr.value = [];
                }
            });
        }

        vm.loadItems = function (opts, callback) {

            if (typeof opts === "function") {
                callback = opts;
                opts = undefined;
            }

            if (!opts) {
                opts = {
                    pageNumber: 1
                };
            }

            // Apply filters
            vm.options.filters.forEach(fltr => {
                if (fltr.value && fltr.value.length > 0) {
                    opts[fltr.alias] = fltr.value;
                } else {
                    delete opts[fltr.alias];
                }
            });

            vendrProductReviewsResource.getPagedProductReviews().then(function (entities) {
                //entities.forEach(function (itm) {
                //    itm.routePath = '/commerce/vendr/review-edit/' + vendrUtils.createCompositeId([storeId, itm.id]);
                //});
                vm.options.items = entities;
                console.log("vm.options.items", vm.options.items);
                if (callback) {
                    callback();
                }
            });

            // Perform search
            //vendrOrderResource.searchOrders(storeId, opts).then(function (entities) {
            //    entities.items.forEach(function (itm) {
            //        itm.routePath = '/commerce/vendr/order-edit/' + vendrUtils.createCompositeId([storeId, itm.id]);
            //    });
            //    vm.options.items = entities;
            //    if (callback) {
            //        callback();
            //    }
            //});
        };

        vm.init = function () {

            navigationService.syncTree({ tree: "vendr", path: "-1," + storeId + ",100", forceReload: true }).then(function (syncArgs) {
                vm.page.menu.currentNode = syncArgs.node;
                vm.page.breadcrumb.items = vendrUtils.createBreadcrumbFromTreeNode(syncArgs.node);
                vm.loadItems({
                    pageNumber: 1
                }, function () {
                    vm.page.loading = false;
                });
            });

        };

        vm.init();

        //var onVendrEvent = function (evt, args) {
        //    if (args.entityType === 'Order' && args.storeId === storeId) {
        //        vm.page.loading = true;
        //        vm.loadItems({
        //            pageNumber: 1
        //        }, function () {
        //            vm.page.loading = false;
        //        });
        //    }
        //};

        //$scope.$on("vendrEntitiesSorted", onVendrEvent);
        //$scope.$on("vendrEntityDelete", onVendrEvent);

        //vm.reviews = [];

        //function init() {

        //    // http://angular-tips.com/blog/2015/10/creating-a-rating-directive-in-angular-2/
        //    // https://jsfiddle.net/n2h05z7e/3/

        //    vendrProductReviewsResource.getPagedProductReviews().then(function (data) {
        //        console.log("data", data);
        //        vm.reviews = data.items;
        //    });
        //}


        //init();
    }

    angular.module('vendr').controller('Vendr.ProductReviews.Controllers.ReviewListController', vendrProductReviewListController);

}());