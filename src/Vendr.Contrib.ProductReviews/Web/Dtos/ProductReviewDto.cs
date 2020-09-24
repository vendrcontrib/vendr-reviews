using System;

namespace Vendr.Contrib.ProductReviews.Web.Dtos
{
    public class ProductReviewDto
    {
        public Guid StoreId { get; set; }

        public string ProductReference { get; set; }

        public string CustomerReference { get; set; }

        public decimal Rating { get; set; }

        public string Title { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}