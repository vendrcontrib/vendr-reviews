using System;

namespace Vendr.Contrib.Reviews.Models
{
    public class Comment
    {
        public Guid Id { get; internal set; }

        public Guid StoreId { get; internal set; }

        public Guid ReviewId { get; internal set; }

        public string Body { get; set; }

        public DateTime CreateDate { get; set; }

        public Comment(Guid storeId, Guid reviewId)
            : this(Guid.Empty, storeId, reviewId)
        { }

        public Comment(Guid id, Guid storeId, Guid reviewId)
        {
            Id = id;
            StoreId = storeId;
            ReviewId = reviewId;
        }
    }
}