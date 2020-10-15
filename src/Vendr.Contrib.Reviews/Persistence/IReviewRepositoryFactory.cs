using Vendr.Contrib.Reviews.Persistence.Repositories;
using Vendr.Core;

namespace Vendr.Contrib.Reviews.Persistence
{
    public interface IReviewRepositoryFactory
    {
        IReviewRepository CreateReviewRepository(IUnitOfWork uow);
    }
}
