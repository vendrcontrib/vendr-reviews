using System;
using System.Collections.Generic;
using Umbraco.Core.Persistence.Querying;
using Vendr.Contrib.ProductReviews.Enums;
using Vendr.Contrib.ProductReviews.Models;

namespace Vendr.Contrib.ProductReviews.Persistence.Repositories
{
    public interface IProductReviewRepository : IDisposable
    {
        ProductReview Get(Guid id);

        IEnumerable<ProductReview> Get(Guid[] ids);

        IEnumerable<ProductReview> GetMany(Guid storeId, string productReference, long pageIndex, long pageSize, out long totalRecords);

        IEnumerable<ProductReview> GetForCustomer(Guid storeId, string customerReference, long pageIndex, long pageSize, out long totalRecords, string productReference = null);

        IEnumerable<ProductReview> GetPagedReviewsByQuery(Guid storeId, IQuery<ProductReview> query, long pageIndex, long pageSize, out long totalRecords);

        IEnumerable<ProductReview> SearchReviews(Guid storeId, long pageIndex, long pageSize, out long totalRecords, string[] statuses, decimal[] ratings, string searchTerm = "");

        ProductReview Save(ProductReview review);

        ProductReview Insert(ProductReview review);

        void Delete(Guid id);

        ProductReview ChangeStatus(Guid id, ReviewStatus status);

        Comment SaveComment(Comment comment);
    }
}