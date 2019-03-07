using Newtonsoft.Json;

namespace GroupTaskApi
{
    public class OpenIdConnectRequest
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
