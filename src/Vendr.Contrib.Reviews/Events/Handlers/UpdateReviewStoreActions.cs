using System;
using System.Linq;
using Vendr.Contrib.Reviews.Models;
using Vendr.Contrib.Reviews.Services;
using Vendr.Core.Events.Notification;
using Vendr.Web.Events.Notification;
using Vendr.Web.Models;

namespace Vendr.Contrib.Reviews.Events.Handlers
{
    public class UpdateReviewStoreActions : NotificationEventHandlerBase<StoreActionsRenderingNotification>
    {
        private readonly IReviewService _reviewService;

        public UpdateReviewStoreActions(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public override void Handle(StoreActionsRenderingNotification evt)
        {
            var statuses = new[] { ReviewStatus.Pending };
            var result = _reviewService.SearchReviews(evt.StoreId, statuses: statuses, startDate: DateTime.UtcNow.Date);

            if (result.TotalItems == 0)
                return;

            evt.Actions.Add(new StoreActionDto
            {
                Icon = Constants.Trees.Reviews.Icon,
                Description = $"<strong>{result.TotalItems + " " + (result.TotalItems == 1 ? "review" : "reviews")}</strong> {(result.TotalItems == 1 ? "is" : "are")} awaiting approval",
                RoutePath = $"#/commerce/vendrreviews/review-list/{evt.StoreId}?statuses={string.Join(",", statuses.Select(x => ((int)x).ToString() ))}"
            });
        }
    }
}