namespace Vendr.ProductReviews.Models
{
    public class ProductReview
    {
        public int Id { get; set; }

        public decimal Rating { get; set; }

        public string ProductReference { get; set; }

        public string CustomerReference { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}