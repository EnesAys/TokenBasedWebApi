using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.OAuth;
using System.Web.Http;

[assembly: OwinStartup(typeof(LengerProje.Authorization.Startup))]

namespace LengerProje.Authorization
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            var myProvider = new MyAuthorizationServerProvider();
            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true, //http den gelen istekleride(güvensiz) kabul etmesi için
                TokenEndpointPath = new PathString("/api/token"),//token almak için gidilmesi gereken endpoint
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(3), //token süresi 3 gün
                Provider = myProvider
            };
            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());


            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
        }
    }
}
