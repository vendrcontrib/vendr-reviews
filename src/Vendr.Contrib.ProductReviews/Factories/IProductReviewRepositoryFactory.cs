using System;
using Vendr.Core;

namespace Vendr.Contrib.ProductReviews.Factories
{
    public interface IProductReviewRepositoryFactory
    {
        IDisposable CreateProductReviewRepository(IUnitOfWork uow);
    }
}
