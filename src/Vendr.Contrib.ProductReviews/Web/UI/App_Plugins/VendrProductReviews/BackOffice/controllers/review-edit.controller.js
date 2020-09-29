(function () {

    'use strict';

    function ReviewEditController($scope, $routeParams, $location, $q, formHelper,
        appState, editorState, editorService, notificationsService, navigationService,
        memberResource, vendrUtils, vendrProductReviewsResource) {

        var infiniteMode = editorService.getNumberOfEditors() > 0 ? true : false;
        var compositeId = infiniteMode
            ? [$scope.model.config.storeId, $scope.model.config.orderId]
            : vendrUtils.parseCompositeId($routeParams.id);

        var storeId = compositeId[0];
        var id = compositeId[1];

        var vm = this;

        vm.page = {};
        vm.page.loading = true;
        vm.page.saveButtonState = 'init';
        vm.page.editView = false;
        vm.page.isInfiniteMode = infiniteMode;

        vm.page.menu = {};
        vm.page.menu.currentSection = appState.getSectionState("currentSection");
        vm.page.menu.currentNode = null;

        vm.page.breadcrumb = {};
        vm.page.breadcrumb.items = [];
        vm.page.breadcrumb.itemClick = function (ancestor) {
            $location.path(ancestor.routePath);
        };

        vm.options = {};
        vm.content = {};
        vm.product = null;
        vm.customer = null;

        vm.close = function () {
            if ($scope.model.close) {
                $scope.model.close();
            }
        };

        vm.back = function () {
            $location.path("/commerce/vendrproductreviews/review-list/" + vendrUtils.createCompositeId([storeId]));
        };

        vm.init = function () {
            vendrProductReviewsResource.getProductReview(id).then(function (review) {

                var promises = [];

                // Check to see if we have a customer ref, and if so, try and fetch a member
                if (review.customerReference) {
                    promises.push(memberResource.getByKey(review.customerReference));
                }
                else {
                    promises.push($q.resolve(null));
                }

                // Check to see if we have a product ref, and if so, try and fetch a product
                if (review.productReference) {
                    promises.push(vendrProductReviewsResource.getProductData(review.productReference, "GB"));
                }
                else {
                    promises.push($q.resolve(null));
                }

                $q.all(promises).then(function (responses) {

                    var resp1 = responses[0];
                    var resp2 = responses[1];

                    if (resp1 !== null) {
                        vm.customer = {
                            name: resp1.name
                        };
                    }
                    
                    if (resp2 !== null) {
                        vm.product = {
                            name: resp2.name,
                            sku: resp2.sku
                        };
                    }

                    vm.ready(review);
                });

            });
        };

        vm.editReview = function () {

            var dialog = {
                view: '/app_plugins/vendrproductreviews/backoffice/views/dialogs/details.html',
                size: 'small',
                title: 'Edit review',
                review: vm.content,
                config: {},
                submit: function (model) {
                    console.log("model", model);

                    console.log("vm.content 1", vm.content);
                    vm.content = model.review;
                    console.log("vm.content 2", vm.content);

                    editorService.close();
                },
                close: function () {
                    editorService.close();
                }
            };

            editorService.open(dialog);
        };

        vm.changeStatus = function () {

            var dialog = {
                view: '/app_plugins/vendrproductreviews/backoffice/views/dialogs/statuspicker.html',
                size: 'small',
                config: {
                    storeId: storeId
                },
                submit: function (model) {
                    vendrProductReviewsResource.changeProductReviewStatus(id, model.id).then(function (review) {
                        vm.content.status = review.status;
                        notificationsService.success("Status Changed", "Status successfully changed to " + model.name + ".");
                        editorService.close();
                    }).catch(function (e) {
                        notificationsService.error("Error Changing Status", "Unabled to change status to " + model.name + ". Please check the error log for details.");
                    });
                },
                close: function () {
                    editorService.close();
                }
            };

            editorService.open(dialog);
        };

        vm.ready = function (model) {
            vm.page.loading = false;
            vm.content = model;

            // sync state
            editorState.set(vm.content);

            if (infiniteMode)
                return;

            var pathToSync = vm.content.path.slice(0, -1);

            navigationService.syncTree({ tree: "vendr", path: pathToSync, forceReload: true }).then(function (syncArgs) {

                var name = vm.content.name;

                // Fake a current node
                // This is used in the header to generate the actions menu
                var application = syncArgs.node.metaData.application;
                var tree = syncArgs.node.metaData.tree;

                vm.page.menu.currentNode = {
                    id: id,
                    name: name,
                    nodeType: "Review",
                    menuUrl: "/umbraco/backoffice/VendrProductReviews/ReviewTree/GetMenu?application=" + application + "&tree=" + tree + "&nodeType=Review&storeId=" + storeId + "&id=" + id,
                    metaData: {
                        treeAlias: tree,
                        storeId: storeId
                    }
                };

                // Build breadcrumb for parent then append current node
                var breadcrumb = vendrUtils.createBreadcrumbFromTreeNode(syncArgs.node);
                breadcrumb.push({ name: name, routePath: "" });
                vm.page.breadcrumb.items = breadcrumb;

            });
        };

        vm.save = function (suppressNotification) {

            if (formHelper.submitForm({ scope: $scope, statusMessage: "Saving..." })) {

                vm.page.saveButtonState = "busy";

                vendrProductReviewsResource.saveProductReview(vm.content).then(function (saved) {

                    formHelper.resetForm({ scope: $scope, notifications: saved.notifications });

                    vm.page.saveButtonState = "success";

                    vm.ready(saved);

                }, function (err) {

                    if (!suppressNotification) {
                        vm.page.saveButtonState = "error";
                        notificationsService.error("Failed to save product review",
                            err.data.message || err.data.Message || err.errorMsg);
                    }

                    vm.page.saveButtonState = "error";
                });
            }

        };

        vm.init();

        $scope.$on("vendrProductReviewDeleted", function (evt, args) {
            if (args.entityType === 'Review' && args.storeId === storeId && args.entityId === id) {
                vm.back();
            }
        });

    }

    angular.module('vendr').controller('Vendr.ProductReviews.Controllers.ReviewEditController', ReviewEditController);

}());