using LightInject;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Vendr.Contrib.ProductReviews.Factories;
using Vendr.Contrib.ProductReviews.Persistence.Repositories;
using Vendr.Contrib.ProductReviews.Persistence.Repositories.Implement;
using Vendr.ProductReviews.Services;
using Vendr.ProductReviews.Services.Implement;
using Vendr.Web.Composing;

namespace Vendr.Contrib.ProductReviews.Composing
{
    //[RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    [ComposeAfter(typeof(VendrWebComposer))]
    public class VendrProductReviewsComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.RegisterUnique<IProductReviewRepositoryFactory>();
            composition.RegisterUnique<IProductReviewRepository, ProductReviewRepository>();

            // Register services
            composition.Register<IProductReviewService, ProductReviewService>(Lifetime.Singleton);

            // Register component
            composition.Components()
                .Append<VendrProductReviewsComponent>();
        }
    }
}