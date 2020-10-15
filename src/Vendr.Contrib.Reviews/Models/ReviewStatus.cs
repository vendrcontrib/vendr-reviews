using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Vendr.Contrib.Reviews.Models
{
    [DataContract]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ReviewStatus
    {
        [EnumMember(Value = "pending")]
        Pending = 0,

        [EnumMember(Value = "approved")]
        Approved = 1,

        [EnumMember(Value = "declined")]
        Declined = 2
    }
}