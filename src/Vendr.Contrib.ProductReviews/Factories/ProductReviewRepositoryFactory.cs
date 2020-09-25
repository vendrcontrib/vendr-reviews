using Umbraco.Core.Persistence;
using Umbraco.Core.Scoping;
using Vendr.Contrib.ProductReviews.Persistence.Repositories;
using Vendr.Contrib.ProductReviews.Persistence.Repositories.Implement;
using Vendr.Core;

namespace Vendr.Contrib.ProductReviews.Factories
{
    public class ProductReviewRepositoryFactory : IProductReviewRepositoryFactory
    {
        private readonly IScopeAccessor _scopeAccessor;

        public ProductReviewRepositoryFactory(IScopeAccessor scopeAccessor)
        {
            _scopeAccessor = scopeAccessor;
        }

        public IProductReviewRepository CreateProductReviewRepository(IUnitOfWork uow)
        {
            return new ProductReviewRepository((IDatabaseUnitOfWork)uow, _scopeAccessor.AmbientScope.SqlContext);
        }
    }
}