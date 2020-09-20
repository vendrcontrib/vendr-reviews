using System;
using System.Collections.Generic;
using Umbraco.Core.Persistence.Querying;
using Vendr.ProductReviews.Models;

namespace Vendr.Contrib.ProductReviews.Persistence.Repositories
{
    public interface IProductReviewRepository : IDisposable
    {
        ProductReview Get(Guid id);

        IEnumerable<ProductReview> Get(Guid[] ids);

        IEnumerable<ProductReview> GetPagedReviewsByQuery(IQuery<ProductReview> query, long pageIndex, long pageSize, out long totalRecords);
    }
}