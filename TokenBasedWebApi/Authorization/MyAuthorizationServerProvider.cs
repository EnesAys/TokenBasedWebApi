using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LengerProje.DATA;
using LengerProje.Helpers.Enums;
using LengerProje.Helpers.Constants;
using System.Web;
using LengerProje.Repositories;
using LengerProje.Helpers;
using LengerProje.Models;
using System.Reflection;

namespace LengerProje.Authorization
{
    public class MyAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private UserRepository userRepo = new UserRepository();
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            Logger.SendLog("MyAuthorizationServerProvider/GrantResourceOwnerCredentials", context.UserName+" "+context.Password, null, LogTypes.Request);
          
            
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            Users user = userRepo.FindUser(context.UserName, context.Password);

            if (user != null)
            {
                try
                {
                    identity.AddClaim(new Claim("username", user.Username));
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.Username));
                   

                    if (user.Roles.RoleType == RoleTypes.SuperUser.ToString())
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Role, RoleTypes.SuperUser.ToString()));
                    }
                    else
                        identity.AddClaim(new Claim(ClaimTypes.Role, RoleTypes.User.ToString()));

                    context.Validated(identity);
                    Logger.SendLog("MyAuthorizationServerProvider/GrantResourceOwnerCredentials", identity.Name,null,null);
                }
                catch (Exception ex)
                {
                    Logger.SendLog("MyAuthorizationServerProvider/GrantResourceOwnerCredentials", ex, true, LogTypes.Response);
                    context.SetError("invalid_grant", Messages.IdentityFail);
                    return;
                }
              

            }
            else
            {
        
                Logger.SendLog( MethodBase.GetCurrentMethod().Name, Messages.UserNotFind,true,LogTypes.Response);
                context.SetError("invalid_grant", Messages.UserNotFind);
                return;
            }

        }
    }
}