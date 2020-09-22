(function () {

    'use strict';

    var routeMap = [
        {
            pattern: /^views\/vendrproductreviews\/(.*)-(.*).html$/gi,
            map: '/app_plugins/vendrproductreviews/views/$1/$2.html'
        },
        {
            pattern: /^views\/vendrproductreviews\/(.*).html$/gi,
            map: '/app_plugins/vendrproductreviews/views/$1/edit.html'
        }
    ];

    function vendrProductReviewRouteRewritesInterceptor($q) {
        return {
            'request': function (config) {
                console.log("config", config);

                routeMap.forEach(function (m) {
                    config.url = config.url.replace(m.pattern, m.map);
                });
                return config || $q.when(config);
            }
        };
    }

    angular.module('vendr.interceptors').factory('vendrProductReviewRouteRewritesInterceptor', vendrProductReviewRouteRewritesInterceptor);

}());