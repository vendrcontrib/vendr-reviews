using System;
using System.Collections.Generic;
using Vendr.Contrib.ProductReviews.Enums;
using Vendr.Contrib.ProductReviews.Models;

namespace Vendr.Contrib.ProductReviews.Services
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
        IEnumerable<ProductReview> GetPagedResults(Guid storeId, long currentPage, long itemsPerPage, out long totalRecords);

        /// <summary>
        /// Search product reviews.
        /// </summary>
        IEnumerable<ProductReview> SearchProductReviews(Guid storeId, long currentPage, long itemsPerPage, out long totalRecords, string[] statuses = null, decimal[] ratings = null, string searchTerm = "", DateTime? startDate = null, DateTime? endDate = null);

        /// <summary>
        /// Add product review.
        /// </summary>
        void AddProductReview(ProductReview review);

        /// <summary>
        /// Save product review.
        /// </summary>
        ProductReview SaveProductReview(ProductReview review);

        /// <summary>
        /// Delete product review.
        /// </summary>
        void DeleteProductReview(Guid id);

        /// <summary>
        /// Change status of product review.
        /// </summary>
        ProductReview ChangeStatus(Guid id, ReviewStatus status);

        /// <summary>
        /// Save comment to product review.
        /// </summary>
        Comment SaveComment(Comment comment);
    }
}
