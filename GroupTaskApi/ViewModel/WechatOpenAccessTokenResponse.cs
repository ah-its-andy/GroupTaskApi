using Newtonsoft.Json;

namespace GroupTaskApi.ViewModel
{
    public class WechatOpenAccessTokenResponse
    {
        [JsonProperty("session_key")]
        public string SessionKey { get; set; }

        [JsonProperty("openid")]
        public string OpenId { get; set; }

        [JsonProperty("unionid")]
        public string UnionId { get; set; }
    }
}
