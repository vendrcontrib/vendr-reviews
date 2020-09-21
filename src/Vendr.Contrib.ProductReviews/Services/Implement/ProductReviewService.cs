using System;
using System.Collections.Generic;
using Umbraco.Core.Models;
using Vendr.Contrib.ProductReviews.Factories;
using Vendr.Core;
using Vendr.Core.Models;
using Vendr.Core.Persistence;
using Vendr.ProductReviews.Models;

namespace Vendr.ProductReviews.Services.Implement
{
    public class ProductReviewService : IProductReviewService
    {
        private IUnitOfWorkProvider _uowProvider;
        private IProductReviewRepositoryFactory _repositoryFactory;
        //private IRepositoryFactory _repositoryFactory;

        public ProductReviewService(IUnitOfWorkProvider uowProvider, IProductReviewRepositoryFactory repositoryFactory) //IRepositoryFactory repositoryFactory)
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
            throw new NotImplementedException();
        }

        public void AddProductReview(string productReference, string customerReference, decimal rating, string name, string description)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductReview> GetProductReviews(string productReference)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductReview> GetProductReviewsForCustomer(string customerReference)
        {
            throw new NotImplementedException();
        }

        public void AddProductReview(string productReference, string customerReference, decimal rating, string title, string name, string description)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductReview> GetPagedResults(long currentPage = 1, long itemsPerPage = 50) //, out long totalRecords)
        {
            long total;
            var results = new List<ProductReview>();

            using (var uow = _uowProvider.Create())
            using (var repo = _repositoryFactory.CreateProductReviewRepository(uow))
            {
                //var query = Query<ProductReview>().Where(x => x.Id == id);

                var items = repo.GetPagedReviewsByQuery(null, currentPage - 1, itemsPerPage, out total);
                results.AddRange(items);
                //totalRecords = total;

                uow.Complete();
            }

            return results;
        }
    }
}