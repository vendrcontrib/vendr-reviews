using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.Querying;
using Vendr.Contrib.ProductReviews.Factories;
using Vendr.Contrib.ProductReviews.Persistence.Dtos;
using Vendr.Core;
using Vendr.ProductReviews.Models;
using Constants = Vendr.Contrib.ProductReviews.Constants;

namespace Vendr.Contrib.ProductReviews.Persistence.Repositories.Implement
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

        public IEnumerable<ProductReview> GetPagedReviewsByQuery(IQuery<ProductReview> query, long pageIndex, long pageSize, out long totalRecords) //, Ordering ordering)
        {
            string sql = $"SELECT * From {Constants.DatabaseSchema.Tables.ProductReviews}";

            //if (ordering == null || ordering.IsEmpty)
            //    ordering = Ordering.By(SqlSyntax.GetQuotedColumn(Constants.DatabaseSchema.Tables.ProductReviews, "id"));

            //var translator = new SqlTranslator<IRelation>(sql, query);
            //sql = translator.Translate();

            // apply ordering
            //ApplyOrdering(ref sql, ordering);

            var pageIndexToFetch = pageIndex + 1;
            var page = _uow.Database.Page<ProductReviewDto>(pageIndexToFetch, pageSize, sql);
            var dtos = page.Items;
            totalRecords = page.TotalItems;

            var result = dtos.Select(ProductReviewFactory.BuildProductReview).ToList();

            return result;
        }
    }
}