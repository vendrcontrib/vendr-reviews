using System;
using Vendr.Core.Events.Notification;
using Vendr.Web.Events.Notification;
using Vendr.Web.Models;

namespace Vendr.Contrib.ProductReviews.Events
{
    public class StoreActionsRenderingEventHandler : NotificationEventHandlerBase<StoreActionsRenderingNotification>
    {
        public override void Handle(StoreActionsRenderingNotification evt)
        {
            //if (review.Status == ReviewStatus.Pending)
            //{
            //    uow.ScheduleNotification(new StoreActionsRenderingNotification(review.StoreId, actions));
            //}

            evt.Actions.Add(new StoreActionDto
            {
                Icon = "icon-rate",
                Description = "A review is waiting for approval",
                RoutePath = $"commerce/vendrproductreviews/review-list/{evt.StoreId}"
            });
        }
    }
}