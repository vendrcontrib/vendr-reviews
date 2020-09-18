using Vendr.Core.Persistence.Repositories;

namespace Vendr.Contrib.ProductReviews.Persistence.Repositories
{
    internal abstract class RepositoryBase : IRepository
    {
        public virtual void Dispose()
        {
            // Dispose of any resources
        }
    }
}