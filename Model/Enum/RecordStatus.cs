using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace APIAssignment2.Models.Enums
{

    [JsonConverter(typeof(StringEnumConverter))]
    public enum RecordStatus
    {
        Active,
        Deleted,
        Archived,
        Pending        
    }
}