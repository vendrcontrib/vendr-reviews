using System;
using System.Runtime.Serialization;
using Vendr.ProductReviews.Enums;

namespace Vendr.ProductReviews.Models
{
    [DataContract]
    public class ProductReview
    {
        public ProductReview(Guid id)
        {
            Id = id;
            Icon = "icon-rate";
        }

        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "storeId")]
        public Guid StoreId { get; set; }

        [DataMember(Name = "icon")]
        public string Icon { get; set; }

        [DataMember(Name = "createDate")]
        public DateTime CreateDate { get; set; }

        [DataMember(Name = "status")]
        public ReviewStatus Status { get; set; }

        [DataMember(Name = "rating")]
        public decimal Rating { get; set; }

        [DataMember(Name = "productReference")]
        public string ProductReference { get; set; }

        [DataMember(Name = "customerReference")]
        public string CustomerReference { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }
    }
}