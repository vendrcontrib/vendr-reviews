using System;
using System.Collections.Generic;
using Vendr.ProductReviews.Models;

namespace Vendr.Contrib.ProductReviews.Repositories
{
    public interface IProductReviewRepository
    {
        IEnumerable<ProductReview> Get(Guid id);
    }
}