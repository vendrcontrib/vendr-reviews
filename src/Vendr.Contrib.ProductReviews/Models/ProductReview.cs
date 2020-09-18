using System;
using Vendr.ProductReviews.Enums;

namespace Vendr.ProductReviews.Models
{
    public class ProductReview
    {
        public ProductReview(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public ReviewStatus Status { get; set; }

        public decimal Rating { get; set; }

        public string ProductReference { get; set; }

        public string CustomerReference { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}