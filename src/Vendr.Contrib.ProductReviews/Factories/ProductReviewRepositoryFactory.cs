using Vendr.Contrib.ProductReviews.Persistence.Repositories;
using Vendr.Contrib.ProductReviews.Persistence.Repositories.Implement;
using Vendr.Core;

namespace Vendr.Contrib.ProductReviews.Factories
{
    public class ProductReviewRepositoryFactory : IProductReviewRepositoryFactory
    {
        public IProductReviewRepository CreateProductReviewRepository(IUnitOfWork uow)
        {
            return new ProductReviewRepository((IDatabaseUnitOfWork)uow);
        }
    }
}