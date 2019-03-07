using JWT;
using JWT.Algorithms;
using JWT.Builder;
using JWT.Serializers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace GroupTaskApi.Security
{
    public class TokenUtil
    {
        public TokenUtil(string secret)
        {
            Secret = secret;
        }

        public TokenUtil(IConfiguration configuration)
            : this(configuration.GetSection("identity").GetValue<string>("secret")) { }

        public TokenUtil(IServiceProvider serviceProvider)
            : this(serviceProvider.GetRequiredService<IConfiguration>())
        {
        }

        public string Secret { get; }

        public string ComputeToken(IDictionary<string, object> claims)
        {
            var builder = new JwtBuilder()
                .WithAlgorithm(new HMACSHA256Algorithm())
                .WithSecret(Secret)
                .AddClaim("exp", DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds());
            foreach (var keyValuePair in claims)
            {
                builder.AddClaim(keyValuePair.Key, keyValuePair.Value);
            }

            return builder.Build();
        }

        public JObject Parse(string token)
        {
            IJsonSerializer serializer = new JsonNetSerializer();
            IDateTimeProvider provider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, provider);
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);

            var json = decoder.Decode(token, Secret, verify: true);
            return JObject.Parse(json);
        }
    }
}
