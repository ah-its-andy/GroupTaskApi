using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupTaskApi.ViewModel
{
    public class WechatOpenAccessTokenRequest
    {
        public string ServiceHost { get; set; }
        public string ServiceUrl { get; set; }
        public string AppId { get; set; }
        public string Secret { get; set; }
        public string Code { get; set; }
        public string GrantType { get; set; }

        public override string ToString()
        {
            return ServiceHost + "/" + ServiceUrl.Replace("{AppId}", AppId)
                       .Replace("{Secret}", Secret)
                       .Replace("{Code}", Code)
                       .Replace("{GrantType}", GrantType);
        }
    }
}
