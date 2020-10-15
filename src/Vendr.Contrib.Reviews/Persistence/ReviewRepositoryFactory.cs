using Umbraco.Core.Persistence;
using Umbraco.Core.Scoping;
using Vendr.Contrib.Reviews.Persistence.Repositories;
using Vendr.Contrib.Reviews.Persistence.Repositories.Implement;
using Vendr.Core;

namespace Vendr.Contrib.Reviews.Persistence
{
    public class ReviewRepositoryFactory : IReviewRepositoryFactory
    {
        private readonly IScopeAccessor _scopeAccessor;

        public ReviewRepositoryFactory(IScopeAccessor scopeAccessor)
        {
            _scopeAccessor = scopeAccessor;
        }

        public IReviewRepository CreateReviewRepository(IUnitOfWork uow)
        {
            return new ReviewRepository((IDatabaseUnitOfWork)uow, _scopeAccessor.AmbientScope.SqlContext);
        }
    }
}