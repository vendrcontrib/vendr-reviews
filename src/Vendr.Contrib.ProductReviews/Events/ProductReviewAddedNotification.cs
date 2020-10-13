using Vendr.Contrib.ProductReviews.Models;
using Vendr.Core.Events.Notification;

namespace Vendr.Contrib.ProductReviews.Events
{
    public class ProductReviewNotificationBase : NotificationEventBase
    {
        public ProductReview Review { get; }

        public ProductReviewNotificationBase(ProductReview review)
        {
            Review = review;
        }
    }

    public class ProductReviewAddingNotification : ProductReviewNotificationBase
    {
        public ProductReviewAddingNotification(ProductReview review)
          : base(review)
        { }
    }

    public class ProductReviewAddedNotification : ProductReviewNotificationBase
    {
        public ProductReviewAddedNotification(ProductReview review)
          : base(review)
        { }
    }
}