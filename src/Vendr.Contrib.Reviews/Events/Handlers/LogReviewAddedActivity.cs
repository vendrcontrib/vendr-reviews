using Umbraco.Core.Models.PublishedContent;
using Vendr.Core.Adapters;
using Vendr.Core.Events.Notification;
using Vendr.Core.Logging;

namespace Vendr.Contrib.Reviews.Events.Handlers
{
    public class LogReviewAddedActivity : NotificationEventHandlerBase<ReviewAddedNotification>
    {
        private readonly IActivityLogger _activityLogger;
        private readonly IProductAdapter _productAdapter;
        private readonly IVariationContextAccessor _variationContextAccessor;

        public LogReviewAddedActivity(IActivityLogger activityLogger, IProductAdapter productAdapter, IVariationContextAccessor variationContextAccessor)
        {
            _activityLogger = activityLogger;
            _productAdapter = productAdapter;
            _variationContextAccessor = variationContextAccessor;
        }

        public override void Handle(ReviewAddedNotification evt)
        {
            var culture = _variationContextAccessor.VariationContext.Culture;

            var snapshot = _productAdapter.GetProductSnapshot(evt.Review.ProductReference, culture);
            if (snapshot == null)
                return;

            _activityLogger.LogActivity(evt.Review.StoreId,
                evt.Review.Id, 
                Constants.Entities.EntityTypes.Review,
                "New review added",
                $"vendrreviews/review-edit/{evt.Review.StoreId}_{evt.Review.Id}",
                $"Review submitted from {evt.Review.Name} with a rating of {evt.Review.Rating} for product {snapshot.Sku}",
                evt.Review.CreateDate);
        }
    }
}