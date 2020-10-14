using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Vendr.Contrib.Reviews.Web
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString VendrReviews(this HtmlHelper html, Guid storeId, string productReference)
            => VendrReviews(html, storeId, productReference, "~/App_Plugins/VendrReviews/Views/Partials/VendrReviews.cshtml");

        public static MvcHtmlString VendrReviews(this HtmlHelper html, Guid storeId, string productReference, string partialViewPath)
        {
            return html.Partial(partialViewPath, new ViewDataDictionary
            {
                { "storeId", storeId },
                { "productReference", productReference }
            });
        }
    }
}