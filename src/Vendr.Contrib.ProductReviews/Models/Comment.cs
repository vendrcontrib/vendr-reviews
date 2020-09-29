using System;
using System.Runtime.Serialization;

namespace Vendr.Contrib.ProductReviews.Models
{
    [DataContract(Name = "comment", Namespace = "")]
    public class Comment
    {
        public Comment() { }

        public Comment(Guid id)
        {
            Id = id;
        }

        [DataMember(Name = "id")]
        public Guid Id { get; internal set; }

        [DataMember(Name = "storeId")]
        public Guid StoreId { get; set; }

        [DataMember(Name = "reviewId")]
        public Guid ReviewId { get; set; }

        [DataMember(Name = "createDate")]
        public DateTime CreateDate { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }
    }
}