using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Umbraco.Web.Models.ContentEditing;
using Vendr.Contrib.ProductReviews.Enums;

namespace Vendr.Contrib.ProductReviews.Models
{
    [DataContract(Name = "productReview", Namespace = "")]
    public class ProductReview
    {
        public ProductReview() { }

        public ProductReview(Guid id)
        {
            Id = id;
            Icon = "icon-rate";
            Notifications = new List<Notification>();
        }

        [DataMember(Name = "path")]
        public string[] Path => new string[] { "-1", StoreId.ToString(), Constants.Trees.Reviews.Id, Id.ToString() };

        [DataMember(Name = "notifications")]
        public List<Notification> Notifications { get; private set; }

        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "storeId")]
        public Guid StoreId { get; set; }

        [DataMember(Name = "icon")]
        public string Icon { get; set; }

        [DataMember(Name = "createDate")]
        public DateTime CreateDate { get; set; }

        [DataMember(Name = "updateDate")]
        public DateTime UpdateDate { get; set; }

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