(function () {
    'use strict';

    var routeMap = [
        {
            pattern: /^views\/vendrproductreviews\/(.*)-(.*).html(.*)$/gi,
            map: '/app_plugins/vendrproductreviews/backoffice/views/$1/$2.html$3'
        },
        {
            pattern: /^views\/vendrproductreviews\/(.*).html(.*)$/gi,
            map: '/app_plugins/vendrproductreviews/backoffice/views/$1/edit.html$3'
        }
    ];

    function vendrProductReviewRouteRewritesInterceptor($q) {
        console.log("vendrProductReviewRouteRewritesInterceptor");

        return {
            'request': function (config) {
                
                routeMap.forEach(function (m) {
                    config.url = config.url.replace(m.pattern, m.map);
                });

                return config || $q.when(config);
            }
        };
    }

    angular.module('umbraco.interceptors').factory('vendrProductReviewRouteRewritesInterceptor', vendrProductReviewRouteRewritesInterceptor);

}());