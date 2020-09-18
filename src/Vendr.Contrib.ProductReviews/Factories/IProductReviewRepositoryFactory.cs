using System;
using Vendr.Contrib.ProductReviews.Persistence.Repositories;
using Vendr.Core;

namespace Vendr.Contrib.ProductReviews.Factories
{
    public interface IProductReviewRepositoryFactory
    {
        IProductReviewRepository CreateProductReviewRepository(IUnitOfWork uow);
    }
}
