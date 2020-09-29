using Vendr.Contrib.ProductReviews.Models;
using Vendr.Core.Events.Notification;

namespace Vendr.Contrib.ProductReviews.Events
{
    public class ReviewAddedNotification : NotificationEventBase
    {
        public ReviewAddedNotification(ProductReview review)
        {
            
        }
    }
}