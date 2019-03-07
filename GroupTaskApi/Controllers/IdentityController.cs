using GroupTaskApi.Security;
using GroupTaskApi.Service;
using GroupTaskApi.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupTaskApi.Controllers
{
    [ApiController]
    public class IdentityController : Controller
    {
        private readonly TokenUtil _tokenUtil;
        private readonly IdentityService _identityService;
        private readonly WechatOpenService _wechatOpenService;

        public IdentityController(TokenUtil tokenUtil, IdentityService identityService, WechatOpenService wechatOpenService)
        {
            _tokenUtil = tokenUtil;
            _identityService = identityService;
            _wechatOpenService = wechatOpenService;
        }

        [HttpPost("connect/token")]
        public async Task<IActionResult> ConnectAsync([FromBody]OpenIdConnectRequest request)
        {
            var wechatUser =
                await _wechatOpenService.FetchWechatOpenAccessTokenAsync(new WechatOpenAccessTokenRequest()
                {
                    AppId = "wxf340990fe65e8bbd",
                    Secret = "73580975e26d6836b0cf8af35dd1e416",
                    Code = request.Code,
                    GrantType = "authorization_code",
                    ServiceHost = "https://api.weixin.qq.com",
                    ServiceUrl = "sns/jscode2session?appid={AppId}&secret={Secret}&js_code={Code}&grant_type={GrantType}"
                }, "");

            var result = await _identityService.SignInAsync(wechatUser.OpenId, request.Name);
            if (result.Succeeded)
            {
                return JwtResult(result.Claims);
            }
            return new JsonResult(result.Errors);
        }

        private IActionResult JwtResult(IDictionary<string, object> claims)
        {
            var result = new
            {
                tokenType = "Bearer",
                token = _tokenUtil.ComputeToken(claims)
            };
            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(result),
                ContentType = "application/json",
                StatusCode = 200
            };
        }
    }
}
