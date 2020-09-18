using System;

namespace Vendr.Contrib.ProductReviews.Dto
{
    public class ProductReviewDto
    {
        public Guid Id { get; set; }

        public DateTime CreateDate { get; set; }

        public decimal Rating { get; set; }

        public string ProductReference { get; set; }

        public string CustomerReference { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}