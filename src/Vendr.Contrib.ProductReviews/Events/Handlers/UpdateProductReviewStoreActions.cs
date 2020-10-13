using System;
using System.Linq;
using Vendr.Contrib.ProductReviews.Enums;
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
            var statuses = new Enum[] { ProductReviewStatus.Pending }.Cast<int>().Select(x => x.ToString()).ToArray();

            _productReviewService.SearchProductReviews(evt.StoreId, 1, 50, out long total, statuses: statuses, startDate: DateTime.UtcNow.Date);

            if (total == 0)
                return;

            evt.Actions.Add(new StoreActionDto
            {
                Icon = Constants.Trees.ProductReviews.Icon,
                Description = $"<strong>{total + " " + (total == 1 ? "review" : "reviews")}</strong> {(total == 1 ? "is" : "are")} waiting for approval",
                RoutePath = $"#/commerce/vendrproductreviews/review-list/{evt.StoreId}?statuses={string.Join(",", statuses)}"
            });
        }
    }
}