using Umbraco.Core;
using Umbraco.Core.Composing;
using Vendr.Contrib.Reviews.Api;
using Vendr.Contrib.Reviews.Components;
using Vendr.Contrib.Reviews.Events;
using Vendr.Contrib.Reviews.Events.Handlers;
using Vendr.Contrib.Reviews.Persistence;
using Vendr.Contrib.Reviews.Services;
using Vendr.Contrib.Reviews.Services.Implement;
using Vendr.Core.Composing;
using Vendr.Web.Composing;
using Vendr.Web.Events.Notification;

namespace Vendr.Contrib.Reviews.Composers
{
    [ComposeAfter(typeof(VendrWebComposer))]
    public class VendrReviewsComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Register<VendrReviewsApi>(Lifetime.Singleton);

            composition.RegisterUnique<IReviewRepositoryFactory, ReviewRepositoryFactory>();

            // Register services
            composition.Register<IReviewService, ReviewService>();

            // Register events
            composition.WithNotificationEvent<ReviewAddedNotification>()
                .RegisterHandler<LogReviewAddedActivity>();

            composition.WithNotificationEvent<StoreActionsRenderingNotification>()
                .RegisterHandler<UpdateReviewStoreActions>();

            composition.WithNotificationEvent<ActivityLogEntriesRenderingNotification>()
                .RegisterHandler<UpdateReviewActivityLogBadge>();

            // Register component
            composition.Components()
                .Append<VendrReviewsComponent>();
        }
    }
}