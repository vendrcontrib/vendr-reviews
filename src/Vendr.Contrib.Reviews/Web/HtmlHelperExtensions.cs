using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Vendr.Contrib.Reviews.Web
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString RenderVendrReviews(this HtmlHelper html, Guid storeId, string productReference)
            => RenderVendrReviews(html, "~/App_Plugins/VendrReviews/Views/Partials/VendrReviews.cshtml", storeId, productReference);

        public static MvcHtmlString RenderVendrReviews(this HtmlHelper html, string partialViewPath, Guid storeId, string productReference)
        {
            return html.Partial(partialViewPath, new ViewDataDictionary
            {
                { "storeId", storeId },
                { "productReference", productReference }
            });
        }
    }
}