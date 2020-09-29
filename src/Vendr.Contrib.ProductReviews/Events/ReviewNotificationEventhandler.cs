using System;
using Vendr.Core.Events.Notification;

namespace Vendr.Contrib.ProductReviews.Events
{
    public class ReviewAddedHandler : NotificationEventHandlerBase<ReviewAddedNotification>
    {
        public override void Handle(ReviewAddedNotification evt)
        {
            throw new NotImplementedException();
        }
    }
}