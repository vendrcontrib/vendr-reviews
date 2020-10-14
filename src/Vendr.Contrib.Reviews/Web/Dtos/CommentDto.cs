using System;
using System.Runtime.Serialization;

namespace Vendr.Contrib.Reviews.Web.Dtos
{
    [DataContract(Name = "comment", Namespace = "")]
    public class CommentDto
    {
        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "storeId")]
        public Guid StoreId { get; set; }

        [DataMember(Name = "reviewId")]
        public Guid ReviewId { get; set; }

        [DataMember(Name = "body")]
        public string Body { get; set; }

        [DataMember(Name = "createDate")]
        public DateTime CreateDate { get; set; }
    }
}