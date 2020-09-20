using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BIEMM.Enums
{

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ModTypes
    {
        None,
        Patch,
        Mod
    }
}