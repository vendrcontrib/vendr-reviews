using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vendr.Contrib.ProductReviews.Web.Dtos
{
    public class ProductReviewDto
    {
        public decimal Rating { get; set; }

        public string Title { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}