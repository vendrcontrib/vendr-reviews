using System;
using Vendr.Core.Logging;
using Vendr.Core.Models;

namespace Vendr.Contrib.ProductReviews.Events
{
    public class ReviewActivityLogger : IActivityLogger
    {
        public PagedResult<ActivityLogEntry> GetActivityLogs(Guid storeId, long curentPage, long itemsPerPage)
        {
            throw new NotImplementedException();
        }
        public void LogActivity(Guid storeId, Guid entityId, string entityType, string entitySummary, string eventType, string eventSummary)
        {
            throw new NotImplementedException();
        }

        public void LogActivity(Guid storeId, Guid entityId, string entityType, string entitySummary, string eventType, string eventSummary, int userId)
        {
            throw new NotImplementedException();
        }

        public void LogActivity(Guid storeId, Guid entityId, string entityType, string entitySummary, string eventType, string eventSummary, DateTime eventDateUtc)
        {
            throw new NotImplementedException();
        }

        public void LogActivity(Guid storeId, Guid entityId, string entityType, string entitySummary, string eventType, string eventSummary, DateTime eventDateUtc, int userId)
        {
            throw new NotImplementedException();
        }
    }
}