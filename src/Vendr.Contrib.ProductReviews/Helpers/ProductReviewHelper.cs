using Vendr.Contrib.ProductReviews.Enums;

namespace Vendr.Contrib.ProductReviews.Helpers
{
    internal static class ProductReviewHelper
    {
        public static string GetStatusColor(ProductReviewStatus status)
        {
            var color = "black";

            switch (status)
            {
                case ProductReviewStatus.Pending:
                    color = "light-blue";
                    break;
                case ProductReviewStatus.Approved:
                    color = "green";
                    break;
                case ProductReviewStatus.Declined:
                    color = "grey";
                    break;
            }

            return color;
        }
    }
}