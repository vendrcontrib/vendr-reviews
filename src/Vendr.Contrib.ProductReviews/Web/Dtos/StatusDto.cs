using System;
using Vendr.Contrib.ProductReviews.Enums;

namespace Vendr.Contrib.ProductReviews.Web.Dtos
{
    public class StatusDto
    {
        public Guid ReviewId { get; set; }

        public ProductReviewStatus Status { get; set; }
    }
}