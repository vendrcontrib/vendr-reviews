using System;
using System.Linq;
using Vendr.Contrib.ProductReviews.Models;
using Vendr.Contrib.ProductReviews.Services;
using Vendr.Core.Events.Notification;
using Vendr.Web.Events.Notification;
using Vendr.Web.Models;

namespace Vendr.Contrib.ProductReviews.Events.Handlers
{
    public class UpdateProductReviewStoreActions : NotificationEventHandlerBase<StoreActionsRenderingNotification>
    {
        private readonly IProductReviewService _productReviewService;

        public UpdateProductReviewStoreActions(IProductReviewService productReviewService)
        {
            _productReviewService = productReviewService;
        }

        public override void Handle(StoreActionsRenderingNotification evt)
        {
            var statuses = new[] { ReviewStatus.Pending };
            var result = _productReviewService.SearchReviews(evt.StoreId, statuses: statuses, startDate: DateTime.UtcNow.Date);

            if (result.TotalItems == 0)
                return;

            evt.Actions.Add(new StoreActionDto
            {
                Icon = Constants.Trees.ProductReviews.Icon,
                Description = $"<strong>{result.TotalItems + " " + (result.TotalItems == 1 ? "review" : "reviews")}</strong> {(result.TotalItems == 1 ? "is" : "are")} awaiting approval",
                RoutePath = $"#/commerce/vendrproductreviews/review-list/{evt.StoreId}?statuses={string.Join(",", statuses.Select(x => ((int)x).ToString() ))}"
            });
        }
    }
}