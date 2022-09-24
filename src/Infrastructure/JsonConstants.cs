using Newtonsoft.Json;

namespace RedSpartan.BrimstoneCompanion.Infrastructure
{
    public static class JsonConstants
    {
        static JsonConstants()
        {
            Settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
            };
        }

        public static JsonSerializerSettings Settings { get; }
    }
}