using Vendr.Contrib.ProductReviews.Persistence.Repositories;
using Vendr.Contrib.ProductReviews.Persistence.Repositories.Implement;
using Vendr.Core;

namespace Vendr.Contrib.ProductReviews.Factories
{
    public class TestRepositoryFactory : IProductReviewRepositoryFactory
    {
        public IProductReviewRepository CreateProductReviewRepository(IUnitOfWork uow)
        {
            return new ProductReviewRepository((IDatabaseUnitOfWork)uow);
        }
    }
}