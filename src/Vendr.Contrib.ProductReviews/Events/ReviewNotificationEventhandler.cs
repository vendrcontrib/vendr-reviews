using System;
using Vendr.Core.Events.Notification;
using Vendr.Core.Logging;

namespace Vendr.Contrib.ProductReviews.Events
{
    public class ProductReviewAddedHandler : NotificationEventHandlerBase<ProductReviewAddedNotification>
    {
        private readonly IActivityLogger _activityLogger;

        public ProductReviewAddedHandler(IActivityLogger activityLogger)
        {
            _activityLogger = activityLogger;
        }

        public override void Handle(ProductReviewAddedNotification evt)
        {
            _activityLogger.LogActivity(evt.Review.StoreId,
                evt.Review.Id, 
                Constants.Entities.EntityTypes.Review, 
                "New review added",
                $"vendrproductreviews/review-edit/{evt.Review.StoreId}_{evt.Review.Id}",
                $"Review submitted from {evt.Review.Name} with a rating {evt.Review.Rating}.");
        }
    }
}