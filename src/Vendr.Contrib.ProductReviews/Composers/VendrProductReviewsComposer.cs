using Umbraco.Core;
using Umbraco.Core.Composing;
using Vendr.Contrib.ProductReviews.Components;
using Vendr.Contrib.ProductReviews.Events;
using Vendr.Contrib.ProductReviews.Events.Handlers;
using Vendr.Contrib.ProductReviews.Persistence;
using Vendr.Contrib.ProductReviews.Services;
using Vendr.Contrib.ProductReviews.Services.Implement;
using Vendr.Core.Composing;
using Vendr.Web.Composing;
using Vendr.Web.Events.Notification;

namespace Vendr.Contrib.ProductReviews.Composers
{
    //[RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    [ComposeAfter(typeof(VendrWebComposer))]
    public class VendrProductReviewsComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.RegisterUnique<IProductReviewRepositoryFactory, ProductReviewRepositoryFactory>();

            // Register services
            composition.Register<IProductReviewService, ProductReviewService>();

            // Register events
            composition.WithNotificationEvent<ProductReviewAddedNotification>()
                .RegisterHandler<LogProductReviewAddedActivity>();

            composition.WithNotificationEvent<StoreActionsRenderingNotification>()
                .RegisterHandler<UpdateProductReviewStoreActions>();

            composition.WithNotificationEvent<ActivityLogEntriesRenderingNotification>()
                .RegisterHandler<UpdateProductReviewActivityLogBadge>();

            // Register component
            composition.Components()
                .Append<VendrProductReviewsComponent>();
        }
    }
}