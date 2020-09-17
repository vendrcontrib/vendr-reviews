namespace Vendr.ProductReviews.Services
{
    interface IProductReviewService
    {
        /// <summary>
        /// Gets a product review.
        /// </summary>
        void GetProductReview(int id);

        /// <summary>
        /// Gets product reviews.
        /// </summary>
        void GetProductReviews(string productReference);

        /// <summary>
        /// Gets product reviews for customer.
        /// </summary>
        void GetProductReviewsForCustomer(string customerReference);

        /// <summary>
        /// Add product review.
        /// </summary>
        void AddProductReview(string productReference, string customerReference, decimal rating, string name, string description);
    }
}
