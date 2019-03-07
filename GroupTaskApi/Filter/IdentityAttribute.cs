using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;
using GroupTaskApi.Security;
using JWT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace GroupTaskApi.Filter
{
    public class IdentityAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var headers = context.HttpContext.Request.Headers;
            if (!headers.ContainsKey("token"))
            {
                context.Result = new ContentResult()
                {
                    ContentType = "text/plain",
                    Content = "Header 'token' is required.",
                    StatusCode = 400
                };
                return;
            }

            var token = headers["token"];
            try
            {
                context.HttpContext.Items["identity_claims"] = context.HttpContext.RequestServices
                    .GetRequiredService<TokenUtil>().Parse(token);
            }
            catch (TokenExpiredException)
            {
                context.Result = new StatusCodeResult(401);
                return;
            }
            catch (SignatureVerificationException)
            {
                context.Result = new StatusCodeResult(403);
                return;
            }

            await next.Invoke();
        }
    }
}
