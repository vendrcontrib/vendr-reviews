using System;
using System.Collections.Generic;
using System.Linq;
using Vendr.Contrib.Reviews.Events;
using Vendr.Contrib.Reviews.Persistence;
using Vendr.Contrib.Reviews.Models;
using Vendr.Core;
using Vendr.Core.Events;
using Vendr.Core.Models;

namespace Vendr.Contrib.Reviews.Services.Implement
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWorkProvider _uowProvider;
        private readonly IReviewRepositoryFactory _repositoryFactory;

        public ReviewService(IUnitOfWorkProvider uowProvider, IReviewRepositoryFactory repositoryFactory)
        {
            _uowProvider = uowProvider;
            _repositoryFactory = repositoryFactory;
        }

        public Review GetReview(Guid id)
        {
            Review review;

            using (var uow = _uowProvider.Create())
            using (var repo = _repositoryFactory.CreateReviewRepository(uow))
            {
                review = repo.GetReview(id);
                uow.Complete();
            }

            return review;
        }

        public IEnumerable<Review> GetReviews(Guid[] ids)
        {
            List<Review> reviews = new List<Review>();

            using (var uow = _uowProvider.Create())
            using (var repo = _repositoryFactory.CreateReviewRepository(uow))
            {
                reviews = repo.GetReviews(ids).ToList();
                uow.Complete();
            }

            return reviews;
        }

        public PagedResult<Review> GetReviewsForProduct(Guid storeId, string productReference, long pageNumber = 1, long pageSize = 50)
        {
            PagedResult<Review> results;

            using (var uow = _uowProvider.Create())
            using (var repo = _repositoryFactory.CreateReviewRepository(uow))
            {
                results = repo.SearchReviews(storeId, productReferences:new[] { productReference }, 
                    statuses:new[] { ReviewStatus.Approved },
                    pageNumber: pageNumber,
                    pageSize: pageSize);

                var reviewIds = results.Items.Select(x => x.Id).ToArray();
                var comments = repo.GetComments(storeId, reviewIds);

                foreach (var item in results.Items)
                {
                    item.Comments = comments.Where(x => x.ReviewId == item.Id).ToList();
                }

                uow.Complete();
            }

            return results;
        }

        public PagedResult<Review> GetReviewsForCustomer(Guid storeId, string customerReference, string productReference = null, long pageNumber = 1, long pageSize = 50)
        {
            PagedResult<Review> results;

            using (var uow = _uowProvider.Create())
            using (var repo = _repositoryFactory.CreateReviewRepository(uow))
            {
                results = repo.SearchReviews(storeId, customerReferences: new[] { customerReference }, 
                    productReferences: productReference != null ? new[] { productReference } : null,
                    statuses: new[] { ReviewStatus.Approved },
                    pageNumber: pageNumber,
                    pageSize: pageSize);

                uow.Complete();
            }

            return results;
        }

        public PagedResult<Review> SearchReviews(Guid storeId, string searchTerm = null, ReviewStatus[] statuses = null, decimal[] ratings = null, DateTime? startDate = null, DateTime? endDate = null, long pageNumber = 1, long pageSize = 50)
        {
            PagedResult<Review> results;

            using (var uow = _uowProvider.Create())
            using (var repo = _repositoryFactory.CreateReviewRepository(uow))
            {
                results = repo.SearchReviews(storeId,
                    searchTerm: searchTerm,
                    statuses: statuses,
                    ratings: ratings,
                    startDate: startDate,
                    endDate: endDate,
                    pageNumber: pageNumber,
                    pageSize: pageSize);

                uow.Complete();
            }

            return results;
        }

        public decimal GetAverageRatingForProduct(Guid storeId, string productReference)
        {
            decimal rating;

            using (var uow = _uowProvider.Create())
            using (var repo = _repositoryFactory.CreateReviewRepository(uow))
            {
                rating = repo.GetAverageRatingForProduct(storeId, productReference);
                uow.Complete();
            }

            return rating;
        }

        public Review SaveReview(Review review)
        {
            Review result;

            using (var uow = _uowProvider.Create())
            using (var repo = _repositoryFactory.CreateReviewRepository(uow))
            {
                if (review.Id == Guid.Empty)
                {
                    review.Id = Guid.NewGuid();
                    review.CreateDate = DateTime.UtcNow;

                    EventBus.Dispatch(new ReviewAddingNotification(review));
                    uow.ScheduleNotification(new ReviewAddedNotification(review));
                }

                review.UpdateDate = DateTime.UtcNow;

                result = repo.SaveReview(review);
                uow.Complete();
            }

            return result;
        }

        public void DeleteReview(Guid id)
        {
            using (var uow = _uowProvider.Create())
            using (var repo = _repositoryFactory.CreateReviewRepository(uow))
            {
                repo.DeleteReview(id);
                uow.Complete();
            }
        }

        public Review ChangeReviewStatus(Guid id, ReviewStatus status)
        {
            Review result;

            using (var uow = _uowProvider.Create())
            using (var repo = _repositoryFactory.CreateReviewRepository(uow))
            {
                result = repo.ChangeReviewStatus(id, status);
                uow.Complete();
            }

            return result;
        }

        public Comment SaveComment(Comment comment)
        {
            Comment result;

            using (var uow = _uowProvider.Create())
            using (var repo = _repositoryFactory.CreateReviewRepository(uow))
            {
                result = repo.SaveComment(comment);
                uow.Complete();
            }

            return result;
        }

        public void DeleteComment(Guid id)
        {
            using (var uow = _uowProvider.Create())
            using (var repo = _repositoryFactory.CreateReviewRepository(uow))
            {
                repo.DeleteComment(id);
                uow.Complete();
            }
        }
    }
}