using System;
using System.Collections.Generic;
using Umbraco.Core.Models;
using Vendr.Contrib.ProductReviews.Factories;
using Vendr.Contrib.ProductReviews.Models;
using Vendr.Core;
using Vendr.Core.Models;
using Vendr.Core.Persistence;

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

        public void AddProductReview(Guid storeId, string productReference, string customerReference, decimal rating, string title, string name, string description)
        {
            using (var uow = _uowProvider.Create())
            using (var repo = _repositoryFactory.CreateProductReviewRepository(uow))
            {
                var now = DateTime.Now;

                var review = new ProductReview
                {
                    StoreId = storeId,
                    ProductReference = productReference,
                    CustomerReference = customerReference,
                    CreateDate = now,
                    UpdateDate = now,
                    Rating = rating,
                    Title = title,
                    Name = name,
                    Description = description
                };

                repo.Insert(review);

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
                //var query = Query<ProductReview>().Where(x => x.Id == id);

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
                //var query = Query<ProductReview>().Where(x => x.Id == id);

                var items = repo.SearchReviews(storeId, currentPage - 1, itemsPerPage, out total, statuses: statuses, ratings: ratings, searchTerm: searchTerm);
                results.AddRange(items);
                totalRecords = total;

                uow.Complete();
            }

            return results;
        }

        public ProductReview SaveProductReview(ProductReview review)
        {
            ProductReview productReview;

            using (var uow = _uowProvider.Create())
            using (var repo = _repositoryFactory.CreateProductReviewRepository(uow))
            {
                review.UpdateDate = DateTime.Now;

                productReview = repo.Save(review);
                uow.Complete();
            }

            return productReview;
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
    }
}