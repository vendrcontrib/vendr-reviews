using System.Collections.Generic;
using Vendr.ProductReviews.Models;

namespace Vendr.ProductReviews.Services
{
    public interface IProductReviewService
    {
        /// <summary>
        /// Gets a product review.
        /// </summary>
        ProductReview GetProductReview(int id);

        /// <summary>
        /// Gets product reviews.
        /// </summary>
        IEnumerable<ProductReview> GetProductReviews(string productReference);

        /// <summary>
        /// Gets product reviews for customer.
        /// </summary>
        IEnumerable<ProductReview> GetProductReviewsForCustomer(string customerReference);

        /// <summary>
        /// Add product review.
        /// </summary>
        void AddProductReview(string productReference, string customerReference, decimal rating, string name, string description);
    }
}
