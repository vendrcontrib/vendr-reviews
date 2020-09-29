using System;
using System.Runtime.Serialization;

namespace Vendr.Contrib.ProductReviews.Models
{
    [DataContract(Name = "status", Namespace = "")]
    public class Status
    {
        [DataMember(Name = "alias")]
        public string Alias { get; set; }

        [DataMember(Name = "color")]
        public string Color { get; set; }

        [DataMember(Name = "icon")]
        public string Icon { get; set; }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "sortOrder")]
        public int SortOrder { get; set; }

        [DataMember(Name = "storeId")]
        public Guid StoreId { get; set; }
    }
}