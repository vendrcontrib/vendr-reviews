using System;
using System.Runtime.Serialization;
using Vendr.Contrib.ProductReviews.Models;

namespace Vendr.Contrib.ProductReviews.Web.Dtos
{
    [DataContract(Name = "changeReviewStatus", Namespace = "")]
    public class ChangeReviewStatusDto
    {
        [DataMember(Name = "reviewId")]
        public Guid ReviewId { get; set; }

        [DataMember(Name = "status")]
        public ReviewStatus Status { get; set; }
    }
}