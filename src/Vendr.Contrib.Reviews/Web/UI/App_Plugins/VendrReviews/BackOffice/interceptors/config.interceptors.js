(function () {
    'use strict';

    angular.module('vendr.interceptors')
        .config(function ($httpProvider) {
            $httpProvider.interceptors.push('vendrReviewsRouteRewritesInterceptor');
        });
})();