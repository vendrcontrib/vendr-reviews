using System;
using System.Collections.Generic;
using Vendr.Contrib.Reviews.Models;
using Vendr.Core.Models;

namespace Vendr.Contrib.Reviews.Services
{
    public interface IReviewService
    {
        /// <summary>
        /// Gets a review.
        /// </summary>
        Review GetReview(Guid id);

        /// <summary>
        /// Gets reviews.
        /// </summary>
        IEnumerable<Review> GetReviews(Guid[] ids);

        /// <summary>
        /// Gets reviews for product.
        /// </summary>
        PagedResult<Review> GetReviewsForProduct(Guid storeId, string productReference, long pageNumber = 1, long pageSize = 50);

        /// <summary>
        /// Gets reviews for customer.
        /// </summary>
        PagedResult<Review> GetReviewsForCustomer(Guid storeId, string customerReference, string productReference = null, long pageNumber = 1, long pageSize = 50);

        /// <summary>
        /// Search reviews.
        /// </summary>
        PagedResult<Review> SearchReviews(Guid storeId, string searchTerm = "", ReviewStatus[] statuses = null, decimal[] ratings = null, DateTime? startDate = null, DateTime? endDate = null, long pageNumber = 1, long pageSize = 50);

        /// <summary>
        /// Get the average star rating for a product.
        /// </summary>
        /// <returns></returns>
        decimal GetAverageRatingForProduct(Guid storeId, string productReference);

        /// <summary>
        /// Save review.
        /// </summary>
        Review SaveReview(Review review);

        /// <summary>
        /// Delete review.
        /// </summary>
        void DeleteReview(Guid id);

        /// <summary>
        /// Change status of review.
        /// </summary>
        Review ChangeReviewStatus(Guid id, ReviewStatus status);

        /// <summary>
        /// Save comment to review.
        /// </summary>
        Comment SaveComment(Comment comment);

        /// <summary>
        /// Delete comment for review.
        /// </summary>
        void DeleteComment(Guid comment);
    }
}
