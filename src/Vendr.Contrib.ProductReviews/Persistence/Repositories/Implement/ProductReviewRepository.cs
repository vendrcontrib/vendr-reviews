using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.Querying;
using Umbraco.Core.Persistence.SqlSyntax;
using Vendr.Contrib.ProductReviews.Factories;
using Vendr.Contrib.ProductReviews.Persistence.Dtos;
using Vendr.Core;
using Vendr.Contrib.ProductReviews.Models;
using Vendr.Contrib.ProductReviews.Enums;

namespace Vendr.Contrib.ProductReviews.Persistence.Repositories.Implement
{
    internal class ProductReviewRepository : RepositoryBase, IProductReviewRepository
    {
        private readonly IDatabaseUnitOfWork _uow;
        private readonly ISqlContext _sqlSyntax;

        public ProductReviewRepository(IDatabaseUnitOfWork uow, ISqlContext sqlSyntax)
        {
            _uow = uow;
            _sqlSyntax = sqlSyntax;
        }

        protected Sql<ISqlContext> Sql() => _sqlSyntax.Sql();
        protected ISqlSyntaxProvider SqlSyntax => _sqlSyntax.SqlSyntax;

        public ProductReview Get(Guid id)
        {
            return DoFetchInternal(_uow, "WHERE id = @0", id).SingleOrDefault();
        }

        public IEnumerable<ProductReview> Get(Guid[] ids)
        {
            return DoFetchInternal(_uow, "WHERE id IN(@0)", ids);
        }

        public IEnumerable<ProductReview> GetMany(Guid storeId, string productReference, long pageIndex, long pageSize, out long totalRecords)
        {
            var sql = Sql()
                .Select("*")
                .From<ProductReviewDto>()
                .Where<ProductReviewDto>(x => x.ProductReference == productReference)
                .OrderByDescending<ProductReviewDto>(x => x.CreateDate);

            var page = _uow.Database.Page<ProductReviewDto>(pageIndex + 1, pageSize, sql);
            var dtos = page.Items;
            totalRecords = page.TotalItems;

            var result = dtos.Select(ProductReviewFactory.BuildProductReview).ToList();

            return result;
        }

        public IEnumerable<ProductReview> GetForCustomer(Guid storeId, string customerReference, long pageIndex, long pageSize, out long totalRecords, string productReference = null)
        {
            var sql = Sql()
                .Select("*")
                .From<ProductReviewDto>()
                .Where<ProductReviewDto>(x => x.StoreId == storeId)
                .Where<ProductReviewDto>(x => x.CustomerReference == customerReference);

            if (!string.IsNullOrWhiteSpace(productReference))
                sql.Where<ProductReviewDto>(x => x.ProductReference == productReference);

            sql.OrderByDescending<ProductReviewDto>(x => x.CreateDate);

            var page = _uow.Database.Page<ProductReviewDto>(pageIndex + 1, pageSize, sql);
            var dtos = page.Items;
            totalRecords = page.TotalItems;

            var result = dtos.Select(ProductReviewFactory.BuildProductReview).ToList();

            return result;
        }

        public ProductReview Save(ProductReview review)
        {
            var dto = ProductReviewFactory.BuildProductReview(review);
            dto.Id = dto.Id == Guid.Empty ? Guid.NewGuid() : dto.Id;

            _uow.Database.Save(dto);

            return review;
        }

        public void Insert(ProductReview review)
        {
            var dto = ProductReviewFactory.BuildProductReview(review);
            dto.Id = dto.Id == Guid.Empty ? Guid.NewGuid() : dto.Id;

            _uow.Database.Insert(dto);
        }

        public void Delete(Guid id)
        {
            _uow.Database.Delete<ProductReviewDto>("WHERE id = @0", id);
        }

        public ProductReview ChangeStatus(Guid id, ReviewStatus status)
        {
            //var sql = Sql().Update<ProductReviewDto>(r => r.Set(x => x.Status, status))
            //     .Where<ProductReviewDto>(x => x.Id == id);

            //_uow.Database.Execute(sql);

            var review = _uow.Database.SingleById<ProductReviewDto>(id);
            review.Status = status;

            _uow.Database.Update(review);

            return ProductReviewFactory.BuildProductReview(review);
        }

        protected IEnumerable<ProductReview> DoFetchInternal(IDatabaseUnitOfWork uow, string sql, params object[] args)
        {
            return uow.Database.Fetch<ProductReviewDto>(sql, args).Select(ProductReviewFactory.BuildProductReview).ToList();
        }

        public IEnumerable<ProductReview> GetPagedReviewsByQuery(Guid storeId, IQuery<ProductReview> query, long pageIndex, long pageSize, out long totalRecords) //, Ordering ordering)
        {
            var sql = Sql()
                .Select("*")
                .From<ProductReviewDto>()
                .Where<ProductReviewDto>(x => x.StoreId == storeId)
                .OrderByDescending<ProductReviewDto>(x => x.CreateDate);

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

            var sql = Sql()
                .Select("*")
                .From<ProductReviewDto>()
                .Where<ProductReviewDto>(x => x.StoreId == storeId);

            if (statuses.Length > 0) {
                sql.WhereIn<ProductReviewDto>(x => x.Status, statuses);
            }

            if (ratings.Length > 0) {
                sql.WhereIn<ProductReviewDto>(x => x.Rating, ratings);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                sql.Where<ProductReviewDto>(x => 
                    x.Title.Contains(searchTerm) ||
                    x.Name.Contains(searchTerm) ||
                    x.Email.Contains(searchTerm) ||
                    x.Description.Contains(searchTerm)
                );
            }

            sql.OrderByDescending<ProductReviewDto>(x => x.CreateDate);

            var page = _uow.Database.Page<ProductReviewDto>(pageIndex + 1, pageSize, sql);
            var dtos = page.Items;
            totalRecords = page.TotalItems;

            var result = dtos.Select(ProductReviewFactory.BuildProductReview).ToList();

            return result;
        }
    }
}