using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Vendr.Contrib.ProductReviews.Enums
{
    [DataContract]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ReviewStatus
    {
        Pending,
        Approved,
        Declined
    }
}