using GroupTaskApi.ViewModel;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GroupTaskApi.Service
{
    public class WechatOpenService
    {
        public async Task<WechatOpenAccessTokenResponse> FetchWechatOpenAccessTokenAsync(WechatOpenAccessTokenRequest request, string cacheId)
        {
            var url = request.ToString();
            var responseBody = await GetJsonAsync(url);
            var responseModel = responseBody.ToObject<WechatOpenAccessTokenResponse>();
          
            return responseModel;
        }

        private async Task<JObject> GetJsonAsync(string url)
        {
            var httpClient = new HttpClient();
            try
            {
                var response = await httpClient.GetStringAsync(url);
                var body = JObject.Parse(response);
                var bodyAsDic = body as IDictionary<string, JToken>;
                if (bodyAsDic.ContainsKey("errcode"))
                {
                    throw new HttpRequestException($"wechat remote : Wechat Open Remote Error: {response}");
                }

                return body;
            }
            finally
            {
                httpClient.Dispose();
            }
        }
    }
}
