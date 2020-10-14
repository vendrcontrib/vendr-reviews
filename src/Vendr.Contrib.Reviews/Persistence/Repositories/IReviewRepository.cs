using System;
using System.Collections.Generic;
using Vendr.Contrib.Reviews.Models;
using Vendr.Core.Models;

namespace Vendr.Contrib.Reviews.Persistence.Repositories
{
    public interface IReviewRepository : IDisposable
    {
        // Reviews 

        Review GetReview(Guid id);

        IEnumerable<Review> GetReviews(Guid[] ids);

        PagedResult<Review> SearchReviews(Guid storeId, string searchTerm = null, string[] productReferences = null, string[] customerReferences = null, ReviewStatus[] statuses = null, decimal[] ratings = null, DateTime? startDate = null, DateTime? endDate = null, long pageNumber = 1, long pageSize = 50);

        decimal GetAverageRatingForProduct(Guid storeId, string productReference);

        Review SaveReview(Review review);

        void DeleteReview(Guid id);

        Review ChangeReviewStatus(Guid id, ReviewStatus status);

        // Comments

        Comment SaveComment(Comment comment);

        void DeleteComment(Guid id);

        IEnumerable<Comment> GetComments(Guid storeId, Guid reviewId);

        IEnumerable<Comment> GetComments(Guid storeId, Guid[] reviewIds);
    }
}