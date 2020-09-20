using Umbraco.Core.Composing;
using Umbraco.Web;

namespace Vendr.Contrib.ProductReviews.Composing
{
    public class VendrProductReviewsComponent : IComponent
    {
        private readonly IUmbracoContextFactory _umbracoContextFactory;

        public VendrProductReviewsComponent(IUmbracoContextFactory umbracoContextFactory)
        {
            _umbracoContextFactory = umbracoContextFactory;
        }

        public void Initialize()
        {
            
        }

        public void Terminate()
        { }
    }
}