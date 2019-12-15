using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Models.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RoleEnum
    {
        Internal = 0,
        Contractor = 1,
        Homeowner = 2
    }
}