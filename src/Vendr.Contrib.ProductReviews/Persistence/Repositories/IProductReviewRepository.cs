using System;
using System.Collections.Generic;
using Vendr.ProductReviews.Models;

namespace Vendr.Contrib.ProductReviews.Persistence.Repositories
{
    public interface IProductReviewRepository
    {
        ProductReview Get(Guid id);

        IEnumerable<ProductReview> Get(Guid[] ids);
    }
}