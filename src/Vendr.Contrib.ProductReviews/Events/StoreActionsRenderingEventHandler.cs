using System;
using System.Linq;
using Vendr.Contrib.ProductReviews.Enums;
using Vendr.Contrib.ProductReviews.Services;
using Vendr.Core.Events.Notification;
using Vendr.Web.Events.Notification;
using Vendr.Web.Models;

namespace Vendr.Contrib.ProductReviews.Events
{
    public class StoreActionsRenderingEventHandler : NotificationEventHandlerBase<StoreActionsRenderingNotification>
    {
        private readonly IProductReviewService _productReviewService;

        public StoreActionsRenderingEventHandler(IProductReviewService productReviewService)
        {
            _productReviewService = productReviewService;
        }

        public override void Handle(StoreActionsRenderingNotification evt)
        {
            var statuses = new Enum[] { ReviewStatus.Pending }.Cast<int>().Select(x => x.ToString()).ToArray();

            long total = 0;
            _productReviewService.SearchProductReviews(evt.StoreId, 1, 50, out total, statuses: statuses, startDate: DateTime.UtcNow.Date);

            if (total == 0)
                return;

            evt.Actions.Add(new StoreActionDto
            {
                Icon = Constants.Trees.Reviews.Icon,
                Description = $"<strong>{total + " " + (total == 1 ? "review" : "reviews")}</strong> {(total == 1 ? "is" : "are")} waiting for approval",
                RoutePath = $"#/commerce/vendrproductreviews/review-list/{evt.StoreId}?statuses={string.Join(",", statuses)}"
            });
        }
    }
}