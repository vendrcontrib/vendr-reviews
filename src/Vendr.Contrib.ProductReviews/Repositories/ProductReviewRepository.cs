using System;
using System.Collections.Generic;
using System.Linq;
using Vendr.Contrib.ProductReviews.Dto;
using Vendr.Contrib.ProductReviews.Factories;
using Vendr.Core;
using Vendr.ProductReviews.Models;
using Constants = Vendr.Contrib.ProductReviews.Constants;

namespace Vendr.Contrib.ProductReviews.Repositories
{
    internal class ProductReviewRepository : RepositoryBase, IProductReviewRepository
    {
        private readonly IDatabaseUnitOfWork _uow;

        public ProductReviewRepository(IDatabaseUnitOfWork uow)
        {
            _uow = uow;
        }

        public ProductReview Get(Guid id)
        {
            //Constants.DatabaseSchema.Tables.ProductReviews

            return DoFetchInternal(_uow, "WHERE id = @0", id).SingleOrDefault();
        }

        public IEnumerable<ProductReview> Get(Guid[] ids)
        {
            return DoFetchInternal(_uow, "WHERE id = IN(@0)", ids);
        }

        protected IEnumerable<ProductReview> DoFetchInternal(IDatabaseUnitOfWork uow, string sql, params object[] args)
        {
            return uow.Database.Fetch<ProductReviewDto>(sql, args).Select(ProductReviewFactory.BuildProductReview).ToList();
        }
    }
}