using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Backend.Interfaces.Repositories;
using Microsoft.Owin.Security.OAuth;

namespace Backend.Config
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return base.ValidateClientAuthentication(context);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            using (var scope = System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver.BeginScope())
            {
                var _userService = scope.GetService(typeof(IUserRepository)) as IUserRepository;
                var user = _userService.Get(context.UserName, new Domain.UserFetchlnclusion { });
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                if (user != null && context.Password == user.Password)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
                    identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));

                    context.Validated(identity);
                }
                else
                {
                    context.SetError("invalid credentials", "provided username and password is incorrect");
                    return;
                }
            }

            
        }
    }
}