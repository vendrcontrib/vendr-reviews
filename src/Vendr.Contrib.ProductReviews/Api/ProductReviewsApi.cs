using System;
using Vendr.Contrib.ProductReviews.Services;

namespace Vendr.Contrib.ProductReviews.Api
{
    public class ProductReviewsApi
    {
        public static ProductReviewsApi Instance { get; internal set; }

        private Lazy<IProductReviewService> _productReviewService;
        public IProductReviewService ReviewService => _productReviewService.Value;

        public ProductReviewsApi(Lazy<IProductReviewService> productReviewService)
        {
            _productReviewService = productReviewService;
        }
    }
}