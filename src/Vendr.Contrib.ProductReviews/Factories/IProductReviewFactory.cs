using System;
using Vendr.Core;

namespace Vendr.Contrib.ProductReviews.Factories
{
    public interface IProductReviewFactory
    {
        IDisposable CreateProductReviewRepository(IUnitOfWork uow);
    }
}
