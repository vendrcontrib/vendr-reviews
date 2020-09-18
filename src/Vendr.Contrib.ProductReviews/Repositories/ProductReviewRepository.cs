using System;
using System.Collections.Generic;
using System.Linq;
using Vendr.Contrib.ProductReviews.Dto;
using Vendr.Contrib.ProductReviews.Factories;
using Vendr.Core;
using Vendr.ProductReviews.Models;

namespace Vendr.Contrib.ProductReviews.Repositories
{
    public class ProductReviewRepository : RepositoryBase, IProductReviewRepository
    {
        private readonly IDatabaseUnitOfWork _uow;

        public ProductReviewRepository(IDatabaseUnitOfWork uow)
        {
            _uow = uow;
        }

        public ProductReview Get(Guid id)
        {
            return DoFetchInternal(_uow, "WHERE id = @0", id).SingleOrDefault();
        }

        protected IEnumerable<ProductReview> DoFetchInternal(IDatabaseUnitOfWork uow, string sql, params object[] args)
        {
            return uow.Database.Fetch<ProductReviewDto>(sql, args).Select(ProductReviewFactory.BuildProductReview).ToList();
        }
    }
}