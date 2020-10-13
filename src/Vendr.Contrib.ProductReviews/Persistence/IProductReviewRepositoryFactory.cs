using Vendr.Contrib.ProductReviews.Persistence.Repositories;
using Vendr.Core;

namespace Vendr.Contrib.ProductReviews.Persistence
{
    public interface IProductReviewRepositoryFactory
    {
        IProductReviewRepository CreateProductReviewRepository(IUnitOfWork uow);
    }
}
