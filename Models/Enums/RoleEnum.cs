using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace Models.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RoleEnum
    {
        Admin = 0,
        Contractor = 1,
        Homeowner = 2
    }
}