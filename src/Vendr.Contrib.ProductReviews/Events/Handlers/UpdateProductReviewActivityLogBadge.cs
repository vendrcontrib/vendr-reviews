using System.Linq;
using Vendr.Core.Events.Notification;
using Vendr.Web.Events.Notification;

namespace Vendr.Contrib.ProductReviews.Events.Handlers
{
    public class UpdateProductReviewActivityLogBadge : NotificationEventHandlerBase<ActivityLogEntriesRenderingNotification>
    {
        public override void Handle(ActivityLogEntriesRenderingNotification evt)
        {
            foreach (var entry in evt.LogEntries.Where(x => 
                x.StoreId == evt.StoreId && 
                x.EntityType == Constants.Entities.EntityTypes.Review))
            {
                entry.BadgeLabel = "Review";
                entry.BadgeColorClass = "vendr-bg--orange";
                entry.RoutePath = $"#/commerce/vendrproductreviews/review-edit/{evt.StoreId}_{entry.EntityId}";
            }
        }
    }
}