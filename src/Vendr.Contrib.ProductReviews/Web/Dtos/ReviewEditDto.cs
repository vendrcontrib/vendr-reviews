using System.Collections.Generic;
using System.Runtime.Serialization;
using Umbraco.Web.Models.ContentEditing;

namespace Vendr.Contrib.ProductReviews.Web.Dtos
{
    [DataContract(Name = "review", Namespace = "")]
    public class ReviewEditDto : ReviewDto
    {
        [DataMember(Name = "path")]
        public string[] Path => new string[] { "-1", StoreId.ToString(), Constants.Trees.ProductReviews.Id, Id.ToString() };

        [DataMember(Name = "notifications")]
        public List<Notification> Notifications { get; set; }

        public ReviewEditDto()
        {
            Notifications = new List<Notification>();
        }
    }
}