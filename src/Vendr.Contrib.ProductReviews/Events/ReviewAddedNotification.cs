using Vendr.Contrib.ProductReviews.Models;
using Vendr.Core.Events.Notification;

namespace Vendr.Contrib.ProductReviews.Events
{
    public class ProductReviewNotificationBase : NotificationEventBase
    {
        public ProductReview Entity { get; }

        public ProductReviewNotificationBase(ProductReview entity)
        {
            Entity = entity;
        }
    }

    public class ProductReviewAddingNotification : ProductReviewNotificationBase
    {
        public ProductReviewAddingNotification(ProductReview entity)
          : base(entity)
        { }
    }

    public class ProductReviewAddedNotification : ProductReviewNotificationBase
    {
        public ProductReviewAddedNotification(ProductReview entity)
          : base(entity)
        { }
    }
}