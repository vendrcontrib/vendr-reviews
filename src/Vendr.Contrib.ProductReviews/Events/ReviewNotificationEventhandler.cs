using Umbraco.Core.Models.PublishedContent;
using Vendr.Core.Adapters;
using Vendr.Core.Events.Notification;
using Vendr.Core.Logging;

namespace Vendr.Contrib.ProductReviews.Events
{
    public class ProductReviewAddedHandler : NotificationEventHandlerBase<ProductReviewAddedNotification>
    {
        private readonly IActivityLogger _activityLogger;
        private readonly IProductAdapter _productAdapter;
        private readonly IVariationContextAccessor _variationContextAccessor;

        public ProductReviewAddedHandler(IActivityLogger activityLogger, IProductAdapter productAdapter, IVariationContextAccessor variationContextAccessor)
        {
            _activityLogger = activityLogger;
            _productAdapter = productAdapter;
            _variationContextAccessor = variationContextAccessor;
        }

        public override void Handle(ProductReviewAddedNotification evt)
        {
            var culture = _variationContextAccessor.VariationContext.Culture;

            var snapshot = _productAdapter.GetProductSnapshot(evt.Review.ProductReference, culture);
            if (snapshot == null)
                return;

            _activityLogger.LogActivity(evt.Review.StoreId,
                evt.Review.Id, 
                Constants.Entities.EntityTypes.Review,
                "New review added",
                $"vendrproductreviews/review-edit/{evt.Review.StoreId}_{evt.Review.Id}",
                $"Review submitted from {evt.Review.Name} with a rating of {evt.Review.Rating} for product {snapshot.Sku}",
                evt.Review.CreateDate);
        }
    }
}