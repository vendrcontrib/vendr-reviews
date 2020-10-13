using System;
using System.Collections.Generic;
using System.Linq;
using Vendr.Contrib.ProductReviews.Enums;
using Vendr.Contrib.ProductReviews.Events;
using Vendr.Contrib.ProductReviews.Persistence;
using Vendr.Contrib.ProductReviews.Models;
using Vendr.Core;
using Vendr.Core.Events;

namespace Vendr.Contrib.ProductReviews.Services.Implement
{
    public class ProductReviewService : IProductReviewService
    {
        private readonly IUnitOfWorkProvider _uowProvider;
        private readonly IProductReviewRepositoryFactory _repositoryFactory;

        public ProductReviewService(IUnitOfWorkProvider uowProvider, IProductReviewRepositoryFactory repositoryFactory)
        {
            _uowProvider = uowProvider;
            _repositoryFactory = repositoryFactory;
        }

        public ProductReview GetProductReview(Guid id)
        {
            ProductReview productReview;

            using (var uow = _uowProvider.Create())
            using (var repo = _repositoryFactory.CreateProductReviewRepository(uow))
            {
                productReview = repo.Get(id);
                uow.Complete();
            }

            return productReview;
        }

        public IEnumerable<ProductReview> GetProductReviews(Guid[] ids)
        {
            List<ProductReview> productReviews = new List<ProductReview>();

            using (var uow = _uowProvider.Create())
            using (var repo = _repositoryFactory.CreateProductReviewRepository(uow))
            {
                var reviews = repo.Get(ids);
                productReviews.AddRange(reviews);
                uow.Complete();
            }

            return productReviews;
        }

        public void AddProductReview(ProductReview review)
        {
            using (var uow = _uowProvider.Create())
            using (var repo = _repositoryFactory.CreateProductReviewRepository(uow))
            {
                var now = DateTime.UtcNow;

                review.CreateDate = now;
                review.UpdateDate = now;

                EventBus.Dispatch(new ProductReviewAddingNotification(review));

                review = repo.Save(review);

                uow.ScheduleNotification(new ProductReviewAddedNotification(review));

                uow.Complete();
            }
        }

        public IEnumerable<ProductReview> GetProductReviews(Guid storeId, string productReference, long currentPage, long itemsPerPage, out long totalRecords)
        {
            List<ProductReview> results = new List<ProductReview>();

            using (var uow = _uowProvider.Create())
            using (var repo = _repositoryFactory.CreateProductReviewRepository(uow))
            {
                var items = repo.GetMany(storeId, productReference, currentPage - 1, itemsPerPage, out long total, ProductReviewStatus.Approved);
                var reviewIds = items.Select(x => x.Id).ToArray();

                var comments = repo.GetComments(storeId, reviewIds);

                foreach (var item in items)
                {
                    item.Comments = comments.Where(x => x.ReviewId == item.Id).ToList();
                }

                results.AddRange(items);
                totalRecords = total;
                uow.Complete();
            }

            return results;
        }

        public IEnumerable<ProductReview> GetProductReviewsForCustomer(Guid storeId, string customerReference, long currentPage, long itemsPerPage, out long totalRecords, string productReference = null)
        {
            List<ProductReview> results = new List<ProductReview>();

            using (var uow = _uowProvider.Create())
            using (var repo = _repositoryFactory.CreateProductReviewRepository(uow))
            {
                var items = repo.GetForCustomer(storeId, customerReference, currentPage - 1, itemsPerPage, out long total, productReference: productReference, status: ProductReviewStatus.Approved);
                results.AddRange(items);
                totalRecords = total;
                uow.Complete();
            }

            return results;
        }

        public IEnumerable<ProductReview> GetPagedResults(Guid storeId, long currentPage, long itemsPerPage, out long totalRecords)
        {
            var results = new List<ProductReview>();

            using (var uow = _uowProvider.Create())
            using (var repo = _repositoryFactory.CreateProductReviewRepository(uow))
            {
                var items = repo.GetPagedReviewsByQuery(storeId, null, currentPage - 1, itemsPerPage, out long total);
                results.AddRange(items);
                totalRecords = total;

                uow.Complete();
            }

            return results;
        }

        public IEnumerable<ProductReview> SearchProductReviews(Guid storeId, long currentPage, long itemsPerPage, out long totalRecords, string[] statuses, decimal[] ratings, string searchTerm = "", DateTime? startDate = null, DateTime? endDate = null)
        {
            var results = new List<ProductReview>();

            using (var uow = _uowProvider.Create())
            using (var repo = _repositoryFactory.CreateProductReviewRepository(uow))
            {
                var items = repo.SearchReviews(storeId, currentPage - 1, itemsPerPage, out long total, statuses: statuses, ratings: ratings, searchTerm: searchTerm, startDate: startDate, endDate: endDate);
                results.AddRange(items);
                totalRecords = total;

                uow.Complete();
            }

            return results;
        }

        public ProductReview SaveProductReview(ProductReview review)
        {
            ProductReview result;

            using (var uow = _uowProvider.Create())
            using (var repo = _repositoryFactory.CreateProductReviewRepository(uow))
            {
                review.UpdateDate = DateTime.UtcNow;

                result = repo.Save(review);
                uow.Complete();
            }

            return result;
        }

        public void DeleteProductReview(Guid id)
        {
            using (var uow = _uowProvider.Create())
            using (var repo = _repositoryFactory.CreateProductReviewRepository(uow))
            {
                repo.Delete(id);
                uow.Complete();
            }
        }

        public ProductReview ChangeStatus(Guid id, ProductReviewStatus status)
        {
            ProductReview result;

            using (var uow = _uowProvider.Create())
            using (var repo = _repositoryFactory.CreateProductReviewRepository(uow))
            {
                result = repo.ChangeStatus(id, status);
                uow.Complete();
            }

            return result;
        }

        public Comment SaveComment(Comment comment)
        {
            Comment result;

            using (var uow = _uowProvider.Create())
            using (var repo = _repositoryFactory.CreateProductReviewRepository(uow))
            {
                result = repo.SaveComment(comment);
                uow.Complete();
            }

            return result;
        }

        public void DeleteComment(Guid id)
        {
            using (var uow = _uowProvider.Create())
            using (var repo = _repositoryFactory.CreateProductReviewRepository(uow))
            {
                repo.DeleteComment(id);
                uow.Complete();
            }
        }
    }
}