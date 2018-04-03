using System;
using System.Collections.Generic;
using System.Text;

namespace OREventApp
{
    public class AuthenticationState
    {
        /// <summary>
        /// The authenticator.
        /// </summary>
        // TODO:
        // Oauth1Authenticator inherits from WebAuthenticator
        // Oauth2Authenticator inherits from WebRedirectAuthenticator
        public static Xamarin.Auth.WebAuthenticator Authenticator;
    }
}
