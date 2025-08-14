using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BaseSource.Kernel.Utilities.Extensions;

public static class JsonExtentions
{
    public static string ToJson(this object obj)
        => JsonConvert.SerializeObject(obj, new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        });

    public static T To<T>(this string str)
        => JsonConvert.DeserializeObject<T>(str)!;
}
