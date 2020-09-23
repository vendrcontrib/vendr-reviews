using Umbraco.Core;
using Umbraco.Core.Composing;
using Vendr.Contrib.ProductReviews.Factories;
using Vendr.Contrib.ProductReviews.Services;
using Vendr.Contrib.ProductReviews.Services.Implement;
using Vendr.Web.Composing;

namespace Vendr.Contrib.ProductReviews.Composing
{
    //[RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    [ComposeAfter(typeof(VendrWebComposer))]
    public class VendrProductReviewsComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.RegisterUnique<IProductReviewRepositoryFactory, ProductReviewRepositoryFactory>();

            // Register services
            composition.Register<IProductReviewService, ProductReviewService>();

            // Register component
            composition.Components()
                .Append<VendrProductReviewsComponent>();
        }
    }
}