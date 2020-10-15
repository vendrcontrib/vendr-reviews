using System;
using System.Runtime.Serialization;
using Vendr.Contrib.Reviews.Models;

namespace Vendr.Contrib.Reviews.Web.Dtos
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