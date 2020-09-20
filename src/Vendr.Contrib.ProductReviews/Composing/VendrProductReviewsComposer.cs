using Umbraco.Core;
using Umbraco.Core.Composing;
using Vendr.ProductReviews.Services;
using Vendr.ProductReviews.Services.Implement;

namespace Vendr.Contrib.ProductReviews.Composing
{
    public class VendrProductReviewsComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            // Register services
            composition.Register<IProductReviewService, ProductReviewService>();

            // Register component
            //composition.Components()
            //    .Append<VendrProductReviewsComponent>();
        }
    }
}