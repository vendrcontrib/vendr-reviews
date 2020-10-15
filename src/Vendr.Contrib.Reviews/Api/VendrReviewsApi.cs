using System;
using Vendr.Contrib.Reviews.Services;

namespace Vendr.Contrib.Reviews.Api
{
    public class VendrReviewsApi
    {
        public static VendrReviewsApi Instance { get; internal set; }

        private Lazy<IReviewService> _reviewService;
        public IReviewService ReviewService => _reviewService.Value;

        public VendrReviewsApi(Lazy<IReviewService> reviewService)
        {
            _reviewService = reviewService;
        }
    }
}