using System;
using Vendr.Core;

namespace Vendr.Contrib.ProductReviews.Factories
{
    internal interface IProductReviewFactory
    {
        IDisposable CreateProductReviewRepository(IUnitOfWork uow);
    }
}
