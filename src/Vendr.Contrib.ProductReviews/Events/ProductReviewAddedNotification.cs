using Vendr.Contrib.ProductReviews.Models;
using Vendr.Core.Events.Notification;

namespace Vendr.Contrib.ProductReviews.Events
{
    public class ProductReviewNotificationBase : NotificationEventBase
    {
        public Review Review { get; }

        public ProductReviewNotificationBase(Review review)
        {
            Review = review;
        }
    }

    public class ProductReviewAddingNotification : ProductReviewNotificationBase
    {
        public ProductReviewAddingNotification(Review review)
          : base(review)
        { }
    }

    public class ProductReviewAddedNotification : ProductReviewNotificationBase
    {
        public ProductReviewAddedNotification(Review review)
          : base(review)
        { }
    }
}