using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.Querying;
using Umbraco.Core.Persistence.SqlSyntax;
using Umbraco.Core.Services;
using Vendr.Contrib.ProductReviews.Factories;
using Vendr.Contrib.ProductReviews.Persistence.Dtos;
using Vendr.Core;
using Vendr.Contrib.ProductReviews.Models;
using Constants = Vendr.Contrib.ProductReviews.Constants;

namespace Vendr.Contrib.ProductReviews.Persistence.Repositories.Implement
{
    internal class ProductReviewRepository : RepositoryBase, IProductReviewRepository
    {
        private readonly IDatabaseUnitOfWork _uow;
        //private readonly ISqlSyntaxProvider _sqlSyntax;

        public ProductReviewRepository(IDatabaseUnitOfWork uow) //, ISqlSyntaxProvider sqlSyntax)
        {
            _uow = uow;
            //_sqlSyntax = sqlSyntax;
        }

        public ProductReview Get(Guid id)
        {
            //Constants.DatabaseSchema.Tables.ProductReviews

            return DoFetchInternal(_uow, "WHERE id = @0", id).SingleOrDefault();
        }

        public IEnumerable<ProductReview> Get(Guid[] ids)
        {
            return DoFetchInternal(_uow, "WHERE id IN(@0)", ids);
        }

        public IEnumerable<ProductReview> GetMany(Guid storeId, string productReference, long pageIndex, long pageSize, out long totalRecords)
        {
            var sql = new Sql()
                .Select()
                .From(Constants.DatabaseSchema.Tables.ProductReviews)
                .Where("productReference = @0", productReference);

            var page = _uow.Database.Page<ProductReviewDto>(pageIndex + 1, pageSize, sql);
            var dtos = page.Items;
            totalRecords = page.TotalItems;

            var result = dtos.Select(ProductReviewFactory.BuildProductReview).ToList();

            return result;
        }

        public IEnumerable<ProductReview> GetForCustomer(Guid storeId, string customerReference, long pageIndex, long pageSize, out long totalRecords, string productReference = null)
        {
            var sql = new Sql()
                .Select()
                .From(Constants.DatabaseSchema.Tables.ProductReviews)
                .Where("storeId = @0", storeId)
                .Where("customerReference = @0", customerReference);

            if (!string.IsNullOrWhiteSpace(productReference))
                sql.Where("productReference = @0", productReference);


            var page = _uow.Database.Page<ProductReviewDto>(pageIndex + 1, pageSize, sql);
            var dtos = page.Items;
            totalRecords = page.TotalItems;

            var result = dtos.Select(ProductReviewFactory.BuildProductReview).ToList();

            return result;
        }

        public ProductReview Save(ProductReview review)
        {
            var dto = ProductReviewFactory.BuildProductReview(review);
            _uow.Database.Save(dto);
            return review;
        }
        public void Delete(Guid id)
        {
            _uow.Database.Delete<ProductReviewDto>("WHERE id = @0", id);
        }

        protected IEnumerable<ProductReview> DoFetchInternal(IDatabaseUnitOfWork uow, string sql, params object[] args)
        {
            return uow.Database.Fetch<ProductReviewDto>(sql, args).Select(ProductReviewFactory.BuildProductReview).ToList();
        }

        public IEnumerable<ProductReview> GetPagedReviewsByQuery(Guid storeId, IQuery<ProductReview> query, long pageIndex, long pageSize, out long totalRecords) //, Ordering ordering)
        {
            var sql = new Sql()
                .Select("*")
                .From(Constants.DatabaseSchema.Tables.ProductReviews)
                .Where("storeId = @0", storeId)
                .OrderBy("createDate DESC");

            //if (ordering == null || ordering.IsEmpty)
            //    ordering = Ordering.By(_sqlSyntax.GetQuotedColumnName(Constants.DatabaseSchema.Tables.ProductReviews, "id"));

            //var translator = new SqlTranslator<IRelation>(sql, query);
            //sql = translator.Translate();

            // apply ordering
            //ApplyOrdering(ref sql, ordering);

            var page = _uow.Database.Page<ProductReviewDto>(pageIndex + 1, pageSize, sql);
            var dtos = page.Items;
            totalRecords = page.TotalItems;

            var result = dtos.Select(ProductReviewFactory.BuildProductReview).ToList();

            return result;
        }

        public IEnumerable<ProductReview> SearchReviews(Guid storeId, long pageIndex, long pageSize, out long totalRecords, string[] statuses = null, decimal[] ratings = null, string searchTerm = null)
        {
            statuses = statuses ?? new string[0];
            ratings = ratings ?? new decimal[0];

            var sql = new Sql()
                .Select("*")
                .From(Constants.DatabaseSchema.Tables.ProductReviews)
                .Where("storeId = @0", storeId);

            if (statuses.Length > 0) {
                sql.Where("status IN(@statuses)", new { statuses });
            }

            if (ratings.Length > 0) {
                sql.Where("rating IN(@ratings)", new { ratings });
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                sql.Where("title LIKE @0", $"%{searchTerm}%");
            }

            sql.OrderBy("createDate DESC");

            var page = _uow.Database.Page<ProductReviewDto>(pageIndex + 1, pageSize, sql);
            var dtos = page.Items;
            totalRecords = page.TotalItems;

            var result = dtos.Select(ProductReviewFactory.BuildProductReview).ToList();

            return result;
        }
    }
}