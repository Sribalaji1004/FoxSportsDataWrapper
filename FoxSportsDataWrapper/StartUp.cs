using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using Microsoft.Owin.Cors;
using System.Web.Cors;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Infrastructure;

[assembly: OwinStartup(typeof(FoxSportsDataWrapper.Startup))]
namespace FoxSportsDataWrapper
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            configAuth(app);
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
           
            

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        private void configAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions auth = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                TokenEndpointPath = new PathString("/token"),
                Provider = new AuthorizationServerProvider(),
                //AuthenticationType = "token",
                //AccessTokenProvider = new AutorizationTokenProvider()


            };
            app.UseOAuthAuthorizationServer(auth);
            OAuthBearerAuthenticationOptions options = new OAuthBearerAuthenticationOptions()
            {
                AuthenticationMode = AuthenticationMode.Passive,

            };
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }

    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async System.Threading.Tasks.Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            //return base.ValidateClientAuthentication(context);
        }

        public override async System.Threading.Tasks.Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            FoxTickEntities repo = new FoxTickEntities();
            
                var checkusername = repo.Users.Where(a => a.Username == context.UserName).FirstOrDefault();

            if (checkusername != null) {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("sub", "FOXDataWrapper"));
                identity.AddClaim(new Claim("username", checkusername.Username));
                context.Validated(identity);
            }
            else {

                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }
        }


    }
    public class AutorizationTokenProvider : AuthenticationTokenProvider
    {
        public override void Create(AuthenticationTokenCreateContext context)
        {
            //base.Create(context);

        }
    }
}