using System;
using System.Runtime.Serialization;

namespace Vendr.Contrib.Reviews.Web.Dtos
{
    [DataContract(Name = "addReview", Namespace = "")]
    public class AddReviewDto
    {
        [DataMember(Name = "storeId")]
        public Guid StoreId { get; set; }

        [DataMember(Name = "productReference")]
        public string ProductReference { get; set; }

        [DataMember(Name = "customerReference")]
        public string CustomerReference { get; set; }

        [DataMember(Name = "rating")]
        public decimal Rating { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "body")]
        public string Body { get; set; }

        [DataMember(Name = "recommendProduct")]
        public bool? RecommendProduct { get; set; }
    }
}