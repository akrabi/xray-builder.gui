using Newtonsoft.Json;

namespace XRayBuilderGUI.Libraries.Serialization.Json.Util
{
    public static class JsonUtil
    {
        public static TObject Deserialize<TObject>(string value, bool strict = true)
            => JsonConvert.DeserializeObject<TObject>(value, new JsonSerializerSettings
            {
                MissingMemberHandling = strict ? MissingMemberHandling.Error : MissingMemberHandling.Ignore
            });

        public static string Serialize(object value)
            => JsonConvert.SerializeObject(value);
    }
}