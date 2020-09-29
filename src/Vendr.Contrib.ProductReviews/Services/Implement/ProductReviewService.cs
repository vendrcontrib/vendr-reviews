using System;
using System.Collections.Generic;
using Umbraco.Core.Models;
using Vendr.Contrib.ProductReviews.Enums;
using Vendr.Contrib.ProductReviews.Factories;
using Vendr.Contrib.ProductReviews.Models;
using Vendr.Core;
using Vendr.Core.Events;
using Vendr.Contrib.ProductReviews.Events;

namespace Vendr.Contrib.ProductReviews.Services.Implement
{
    public class ProductReviewService : IProductReviewService
    {
        private IUnitOfWorkProvider _uowProvider;
        private IProductReviewRepositoryFactory _repositoryFactory;

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

                repo.Insert(review);

                uow.ScheduleNotification(new ProductReviewAddedNotification(review));

                uow.Complete();
            }
        }

        public IEnumerable<ProductReview> GetProductReviews(Guid storeId, string productReference, long currentPage, long itemsPerPage, out long totalRecords)
        {
            long total;
            List<ProductReview> results = new List<ProductReview>();

            using (var uow = _uowProvider.Create())
            using (var repo = _repositoryFactory.CreateProductReviewRepository(uow))
            {
                var items = repo.GetMany(storeId, productReference, currentPage - 1, itemsPerPage, out total);
                results.AddRange(items);
                totalRecords = total;
                uow.Complete();
            }

            return results;
        }

        public IEnumerable<ProductReview> GetProductReviewsForCustomer(Guid storeId, string customerReference, long currentPage, long itemsPerPage, out long totalRecords, string productReference = null)
        {
            long total;
            List<ProductReview> results = new List<ProductReview>();

            using (var uow = _uowProvider.Create())
            using (var repo = _repositoryFactory.CreateProductReviewRepository(uow))
            {
                var items = repo.GetForCustomer(storeId, customerReference, currentPage - 1, itemsPerPage, out total, productReference: productReference);
                results.AddRange(items);
                totalRecords = total;
                uow.Complete();
            }

            return results;
        }

        public IEnumerable<ProductReview> GetPagedResults(Guid storeId, long currentPage, long itemsPerPage, out long totalRecords)
        {
            long total;
            var results = new List<ProductReview>();

            using (var uow = _uowProvider.Create())
            using (var repo = _repositoryFactory.CreateProductReviewRepository(uow))
            {
                var items = repo.GetPagedReviewsByQuery(storeId, null, currentPage - 1, itemsPerPage, out total);
                results.AddRange(items);
                totalRecords = total;

                uow.Complete();
            }

            return results;
        }

        public IEnumerable<ProductReview> SearchProductReviews(Guid storeId, long currentPage, long itemsPerPage, out long totalRecords, string[] statuses, decimal[] ratings, string searchTerm = "")
        {
            long total;
            var results = new List<ProductReview>();

            using (var uow = _uowProvider.Create())
            using (var repo = _repositoryFactory.CreateProductReviewRepository(uow))
            {
                var items = repo.SearchReviews(storeId, currentPage - 1, itemsPerPage, out total, statuses: statuses, ratings: ratings, searchTerm: searchTerm);
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

        public ProductReview ChangeStatus(Guid id, ReviewStatus status)
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
    }
}