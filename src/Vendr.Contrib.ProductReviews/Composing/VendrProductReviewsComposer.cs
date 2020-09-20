using Umbraco.Core;
using Umbraco.Core.Composing;
using Vendr.ProductReviews.Services;
using Vendr.ProductReviews.Services.Implement;
using Vendr.Web.Composing;

namespace Vendr.Contrib.ProductReviews.Composing
{
    [ComposeAfter(typeof(VendrWebComposer))]
    public class VendrProductReviewsComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            // Register services
            composition.Register<IProductReviewService, ProductReviewService>(Lifetime.Singleton);

            // Register component
            //composition.Components()
            //    .Append<VendrProductReviewsComponent>();
        }
    }
}