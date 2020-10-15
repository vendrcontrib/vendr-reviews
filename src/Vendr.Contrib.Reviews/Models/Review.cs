using System;
using System.Collections.Generic;

namespace Vendr.Contrib.Reviews.Models
{
    public class Review
    {
        public Guid Id { get; internal set; }

        public Guid StoreId { get; internal set; }

        public string ProductReference { get; internal set; }

        public string CustomerReference { get; internal set; }

        public decimal Rating { get; set; }

        public string Title { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Body { get; set; }

        public bool VerifiedBuyer { get; set; }

        public bool? RecommendProduct { get; set; }

        public ReviewStatus Status { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public IReadOnlyCollection<Comment> Comments { get; internal set; }

        public Review(Guid storeId, string productReference)
            : this(Guid.Empty, storeId, productReference, null)
        { }

        public Review(Guid storeId, string productReference, string customerReference)
            : this(Guid.Empty, storeId, productReference, customerReference)
        { }

        public Review(Guid id, Guid storeId, string productReference, string customerReference)
        {
            Id = id;
            StoreId = storeId;
            ProductReference = productReference;
            CustomerReference = customerReference;
        }
    }
}