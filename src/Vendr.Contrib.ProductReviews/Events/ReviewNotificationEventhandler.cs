using System;
using Vendr.Core.Events.Notification;

namespace Vendr.Contrib.ProductReviews.Events
{
    public class ProductReviewAddedHandler : NotificationEventHandlerBase<ProductReviewAddedNotification>
    {
        public override void Handle(ProductReviewAddedNotification evt)
        {
            throw new NotImplementedException();
        }
    }
}