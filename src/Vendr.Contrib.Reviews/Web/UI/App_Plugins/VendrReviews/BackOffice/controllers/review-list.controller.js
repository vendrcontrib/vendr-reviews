(function () {

    'use strict';

    function ReviewListController($scope, $routeParams, $location, $q, appState, vendrReviewsResource, navigationService, vendrUtils, vendrRouteCache, vendrLocalStorage) {

        var compositeId = vendrUtils.parseCompositeId($routeParams.id);
        var storeId = compositeId[0];

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
                {
                    name: 'Status',
                    alias: 'statuses',
                    localStorageKey: 'store_' + storeId + '_reviewStatusFilter',
                    getFilterOptions: function () {
                        return vendrRouteCache.getOrFetch("store_" + storeId + "_reviewStatuses", function () {
                            return vendrReviewsResource.getReviewStatuses(storeId);
                        })
                        .then(function (items) {
                            return items.map(function (itm) {
                                return {
                                    id: itm.id,
                                    name: itm.name,
                                    color: itm.color
                                };
                            });
                        });
                    }
                },
                {
                    name: 'Rating',
                    alias: 'ratings',
                    localStorageKey: 'store_' + storeId + '_reviewRatingFilter',
                    getFilterOptions: function () {
                        return $q.resolve([
                            { id: 1, name: '1', color: 'light-grey' },
                            { id: 2, name: '2', color: 'light-grey' },
                            { id: 3, name: '3', color: 'light-grey' },
                            { id: 4, name: '4', color: 'light-grey' },
                            { id: 5, name: '5', color: 'light-grey' }
                        ]);
                    }
                }
            ],
            bulkActions: [
                {
                    name: 'Delete',
                    icon: 'icon-trash',
                    doAction: function (bulkItem) {
                        return vendrReviewsResource.deleteReview(bulkItem.id);
                    },
                    getConfirmMessage: function (total) {
                        return $q.resolve("Are you sure you want to delete " + total + " " + (total > 1 ? "items" : "item") + "?");
                    }
                }
            ],
            items: [],
            itemProperties: [
                { alias: 'rating', header: 'Rating', template: `<span class="vendr-table-cell-value--multiline" title="Rating: {{rating}}">
                        <span class="rating" aria-hidden="true">
                            <i class="icon-rate {{rating >= 1 ? 'active' : ''}}"></i>
                            <i class="icon-rate {{rating >= 2 ? 'active' : ''}}"></i>
                            <i class="icon-rate {{rating >= 3 ? 'active' : ''}}"></i>
                            <i class="icon-rate {{rating >= 4 ? 'active' : ''}}"></i>
                            <i class="icon-rate {{rating >= 5 ? 'active' : ''}}"></i>
                        </span><span class="sr-only">{{rating}} stars</span></span>` },
                { alias: 'review', header: 'Review', template: '<span class="db bold">{{title}}</span><span class="vendr-table-cell-label">{{body}}</span>' },
                { alias: 'createDate', header: 'Date', template: "{{ createDate | date : 'MMMM d, yyyy h:mm a' }}" },
                { alias: 'status', header: 'Status', align: 'right', template: '<span class="umb-badge umb-badge--xs vendr-bg--{{status.color}}" title="Status: {{status.name}}">{{status.name}}</span>' }
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

            // Perform search
            vendrReviewsResource.searchReviews(storeId, opts).then(function (entities) {
                entities.items.forEach(function (itm) {
                    itm.routePath = '/commerce/vendrreviews/review-edit/' + vendrUtils.createCompositeId([storeId, itm.id]);
                });
                vm.options.items = entities;
                if (callback) {
                    callback();
                }
            });
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

        var onVendrReviewEvent = function (evt, args) {
            if (args.entityType === 'Review' && args.storeId === storeId) {
                vm.page.loading = true;
                vm.loadItems({
                    pageNumber: 1
                }, function () {
                    vm.page.loading = false;
                });
            }
        };

        $scope.$on("vendrReviewDeleted", onVendrReviewEvent);
    }

    angular.module('vendr').controller('Vendr.Reviews.Controllers.ReviewListController', ReviewListController);

}());