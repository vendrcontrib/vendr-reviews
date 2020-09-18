using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vendr.ProductReviews.Models;

namespace Vendr.ProductReviews.Services.Implement
{
    public sealed class ProductReviewService : IProductReviewService
    {
        public ProductReview GetProductReview(Guid ids)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductReview> GetProductReviews(Guid[] ids)
        {
            throw new NotImplementedException();
        }

        public void AddProductReview(string productReference, string customerReference, decimal rating, string name, string description)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductReview> GetProductReviews(string productReference)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductReview> GetProductReviewsForCustomer(string customerReference)
        {
            throw new NotImplementedException();
        }
    }
}