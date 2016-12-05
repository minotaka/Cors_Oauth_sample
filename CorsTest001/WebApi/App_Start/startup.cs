using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Web.Http.Cors;

[assembly: OwinStartup(typeof(WebApi.Startup))]
namespace WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseOAuthBearerTokens(new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            });
        }
    }

    class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult(0);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            // 認証ロジックを書く
            // if (context.UserName == "admin" && context.Password == "p@ssw0rd")
            if (context.UserName == "admin" && context.Password == "admin")
            {
                // context.Options.AuthenticationTypeを使ってClaimsIdentityを作る
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                // 必要なClaimを追加しておく。
                identity.AddClaims(new[]
                {
                    new Claim(ClaimTypes.GivenName, context.UserName),
                    new Claim(ClaimTypes.Role, "User"),
                    new Claim(ClaimTypes.Role, "Admin")
                });
                context.Validated(identity);
            }
            else
            {
                context.Rejected();
            }
            return Task.FromResult(0);
        }
    }
}
