(function () {

    'use strict';

    function ReviewEditController($scope, $routeParams, $location, $q, formHelper,
        appState, editorState, editorService, notificationsService, navigationService,
        memberResource, vendrUtils, vendrReviewsResource) {

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
        vm.comment = null;
        vm.product = null;
        vm.customer = null;

        vm.close = function () {
            if ($scope.model.close) {
                $scope.model.close();
            }
        };

        vm.back = function () {
            $location.path("/commerce/vendrreviews/review-list/" + vendrUtils.createCompositeId([storeId]));
        };

        vm.init = function () {
            vendrReviewsResource.getReview(id).then(function (review) {

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
                    promises.push(vendrReviewsResource.getProductData(review.productReference, null));
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
                view: '/app_plugins/vendrreviews/backoffice/views/dialogs/details.html',
                size: 'small',
                title: 'Edit review',
                review: vm.content,
                config: {},
                submit: function (model) {
                    vm.content = model.review;

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
                view: '/app_plugins/vendrreviews/backoffice/views/dialogs/statuspicker.html',
                size: 'small',
                config: {
                    storeId: storeId
                },
                submit: function (model) {
                    vm.doChangeStatus(model.id);
                    editorService.close();
                },
                close: function () {
                    editorService.close();
                }
            };

            editorService.open(dialog);
        };

        vm.approveReview = function () {
            vm.doChangeStatus({ id: 1, name: 'Approved' });
        }

        vm.declineReview = function () {
            vm.doChangeStatus({ id: 2, name: 'Declined' });
        }

        vm.unapproveReview = function () {
            vm.doChangeStatus({ id: 0, name: 'Pending' });
        }

        vm.doChangeStatus = function (status) {
            vendrReviewsResource.changeReviewStatus(id, status.id).then(function (review) {
                vm.content.status = review.status;
                notificationsService.success("Status Changed", "Status successfully changed to " + status.name + ".");
            }).catch(function (e) {
                notificationsService.error("Error Changing Status", "Unabled to change status to " + status.name + ". Please check the error log for details.");
            });
        }

        vm.ready = function (model) {
            vm.page.loading = false;
            vm.content = model;

            // currently we only use a single comment
            if (vm.content.comments && vm.content.comments.length > 0) {
                vm.comment = vm.content.comments[0].body;
            }

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
                    menuUrl: "/umbraco/backoffice/VendrReviews/ReviewTree/GetMenu?application=" + application + "&tree=" + tree + "&nodeType=Review&storeId=" + storeId + "&id=" + id,
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

                vendrReviewsResource.saveReview(vm.content).then(function (saved) {

                    if (vm.comment !== null)
                    {
                        var commentId = null;
                        var commentBody = null;

                        if (vm.content.comments && vm.content.comments[0]) {
                            commentId = vm.content.comments[0].id;
                            commentBody = vm.content.comments[0].body;
                        }

                        if (vm.comment !== commentBody)
                        {
                            if (vm.comment.trim().length > 0) {
                                // Insert or update comment
                                vendrReviewsResource.saveComment(commentId, storeId, id, vm.comment).then(function (data) {
                                    vm.comment = data.body;
                                });
                            }
                            else if (commentId !== null && commentId !== undefined) {
                                // Delete comment
                                vendrReviewsResource.deleteComment(commentId).then(function (data) {
                                    vm.comment = null; 
                                });
                            }
                        }
                    }

                    formHelper.resetForm({ scope: $scope, notifications: saved.notifications });

                    vm.page.saveButtonState = "success";

                    vm.ready(saved);

                }, function (err) {

                    if (!suppressNotification) {
                        vm.page.saveButtonState = "error";
                        notificationsService.error("Failed to save review",
                            err.data.message || err.data.Message || err.errorMsg);
                    }

                    vm.page.saveButtonState = "error";
                });
            }

        };

        vm.init();

        $scope.$on("vendrReviewDeleted", function (evt, args) {
            if (args.entityType === 'Review' && args.storeId === storeId && args.entityId === id) {
                vm.back();
            }
        });

    }

    angular.module('vendr').controller('Vendr.Reviews.Controllers.ReviewEditController', ReviewEditController);

}());