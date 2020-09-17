using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BIEMM
{

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ModTypes
    {
        None,
        Patch,
        Mod
    }
}