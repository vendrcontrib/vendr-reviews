using System;
using System.Collections.Generic;
using Vendr.ProductReviews.Models;

namespace Vendr.ProductReviews.Services
{
    public interface IProductReviewService
    {
        /// <summary>
        /// Gets a product review.
        /// </summary>
        ProductReview GetProductReview(Guid id);

        /// <summary>
        /// Gets product reviews.
        /// </summary>
        IEnumerable<ProductReview> GetProductReviews(Guid[] ids);

        /// <summary>
        /// Gets product reviews.
        /// </summary>
        IEnumerable<ProductReview> GetProductReviews(Guid storeId, string productReference, long currentPage, long itemsPerPage, out long totalRecords);

        /// <summary>
        /// Gets product reviews for customer.
        /// </summary>
        IEnumerable<ProductReview> GetProductReviewsForCustomer(Guid storeId, string customerReference, long currentPage, long itemsPerPage, out long totalRecords, string productReference = null);

        /// <summary>
        /// Gets paged result of product reviews.
        /// </summary>
        IEnumerable<ProductReview> GetPagedResults(long currentPage, long itemsPerPage, out long totalRecords);

        /// <summary>
        /// Add product review.
        /// </summary>
        void AddProductReview(Guid storeId, string productReference, string customerReference, decimal rating, string title, string name, string description);

        /// <summary>
        /// Save product review.
        /// </summary>
        void SaveProductReview(ProductReview review);

        /// <summary>
        /// Delete product review.
        /// </summary>
        void DeleteProductReview(Guid id);
    }
}
