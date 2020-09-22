(function () {
    'use strict';

    var routeMap = [
        {
            pattern: /^views\/vendrproductreviews\/(.*)-(.*).html$/gi,
            map: '/app_plugins/vendrproductreviews/backoffice/views/$1/$2.html'
        },
        {
            pattern: /^views\/vendrproductreviews\/(.*).html$/gi,
            map: '/app_plugins/vendrproductreviews/backoffice/views/$1/edit.html'
        }
    ];

    function vendrProductReviewRouteRewritesInterceptor($q) {
        console.log("vendrProductReviewRouteRewritesInterceptor");

        return {
            'request': function (config) {
                console.log("config", config);

                if (config.url.toLowerCase().includes('views/vendrproductreviews/review-list.html')) {
                    config.url = '/app_plugins/vendrproductreviews/backoffice/views/review/list.html';
                }

                //routeMap.forEach(function (m) {
                //    console.log("config url 1", config.url, m.pattern, m.map);
                //    config.url = config.url.replace(m.pattern, m.map);
                //    console.log("config url 2", config.url);
                //});

                return config || $q.when(config);
            }
        };
    }

    angular.module('umbraco.interceptors').factory('vendrProductReviewRouteRewritesInterceptor', vendrProductReviewRouteRewritesInterceptor);

}());