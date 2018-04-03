using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OREventApp.Helpers;
using Shared.Enums;
using Shared.Models;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OREventApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
	    protected Xamarin.Auth.WebAuthenticator _authenticator = null;
	    public AccountStore _store = null;
        public LoginPage ()
		{
			InitializeComponent ();
		    _store = AccountStore.Create();
		}

	    private void Button_OnClickedGoogle(object sender, EventArgs e)
	    {
            _authenticator  = new Xamarin.Auth.OAuth2Authenticator
                 (
                     clientId:
                         new Func<string>
                            (
                                () =>
                                {
                                    string retval_client_id = "oops something is wrong!";
                                    switch (Xamarin.Forms.Device.RuntimePlatform)
                                    {
                                        case "Android":
                                            retval_client_id = "192091297496-c380rar1uks0c1odcqr07jmd3t4mudbl.apps.googleusercontent.com";
                                            break;
                                        /*case "iOS":
                                            retval_client_id = "1093596514437-cajdhnien8cpenof8rrdlphdrboo56jh.apps.googleusercontent.com";
                                            break;*/
                                    }
                                    return retval_client_id;
                                }
                           ).Invoke(),
                    clientSecret: null,
                    authorizeUrl: new Uri("https://accounts.google.com/o/oauth2/auth"),
                    accessTokenUrl: new Uri("https://www.googleapis.com/oauth2/v4/token"),
                    redirectUrl:
                        new Func<Uri>
                            (
                                () =>
                                {
                                    string uri = null;
                                    switch (Xamarin.Forms.Device.RuntimePlatform)
                                    {
                                        case "Android":
                                            uri = "com.companyname.oreventapp:/oauth2redirect";
                                            break;
                                        /*case "iOS":
                                            uri =
                                                "com.xamarin.traditional.standard.samples.oauth.providers.ios:/oauth2redirect"
                                                //"com.googleusercontent.apps.1093596514437-cajdhnien8cpenof8rrdlphdrboo56jh:/oauth2redirect"
                                                ;
                                            break;*/
                                    }

                                    return new Uri(uri);
                                }
                             ).Invoke(),
                     scope:
                          "https://www.googleapis.com/auth/userinfo.profile https://www.googleapis.com/auth/userinfo.email https://www.googleapis.com/auth/plus.login",
                     getUsernameAsync: null,
                     isUsingNativeUI: true
                 )
                 {
                     AllowCancel = true,
                 };

            _authenticator.Completed += async (s, ea) =>
            {
                if (ea.IsAuthenticated)
                {
                    Account lastAccount = _store.FindAccountsForService("Google").FirstOrDefault();
                    if (lastAccount != null)
                    {
                        _store.Delete(lastAccount, "Google");
                    }
                    _store.Save(ea.Account, "Google");
                    RegistrationModel registrationModel = await GetGoogleAccountInfo(ea.Account);
                    Registration(registrationModel);
                }
                else
                {
                    await DisplayAlert("Authentication Results", "Unauthenticated user", "Cancel");
                }
            };

	        _authenticator.Error += (s, ea) =>
	        {
	            StringBuilder sb = new StringBuilder();
	            sb.Append("Error = ").AppendLine($"{ea.Message}");

	            DisplayAlert
	            (
	                "Authentication Error",
	                sb.ToString(),
	                "OK"
	            );
	        };
                
	        
            AuthenticationState.Authenticator = _authenticator;
            this.PresentUILoginScreen(_authenticator);
        }

        private void Button_OnClickedFacebook(object sender, EventArgs e)
        {
            _authenticator = new Xamarin.Auth.OAuth2Authenticator
                 (
                     clientId:
                         new Func<string>
                            (
                                () =>
                                {
                                    string retval_client_id = "oops something is wrong!";
                                    switch (Xamarin.Forms.Device.RuntimePlatform)
                                    {
                                        case "Android":
                                            retval_client_id = "159391811426126";
                                            break;
                                            /*case "iOS":
                                                retval_client_id = "1093596514437-cajdhnien8cpenof8rrdlphdrboo56jh.apps.googleusercontent.com";
                                                break;*/
                                    }
                                    return retval_client_id;
                                }
                           ).Invoke(),
                    authorizeUrl: new Uri("https://www.facebook.com/v2.9/dialog/oauth"),
                    redirectUrl:
                        new Func<Uri>
                            (
                                () =>
                                {
                                    string uri = null;
                                    switch (Xamarin.Forms.Device.RuntimePlatform)
                                    {
                                        case "Android":
                                            uri = "fb159391811426126://authorize";
                                            break;
                                            /*case "iOS":
                                                uri =
                                                    "com.xamarin.traditional.standard.samples.oauth.providers.ios:/oauth2redirect"
                                                    //"com.googleusercontent.apps.1093596514437-cajdhnien8cpenof8rrdlphdrboo56jh:/oauth2redirect"
                                                    ;
                                                break;*/
                                    }

                                    return new Uri(uri);
                                }
                             ).Invoke(),
                     scope: "email public_profile",
                     getUsernameAsync: null,
                     isUsingNativeUI: true
                 )
            {
                AllowCancel = true,
            };

            _authenticator.Completed += async (s, ea) =>
            {
                if (ea.IsAuthenticated)
                {
                    Account lastAccount = _store.FindAccountsForService("Facebook").FirstOrDefault();
                    if (lastAccount != null)
                    {
                        _store.Delete(lastAccount,"Facebook");
                    }
                    _store.Save(ea.Account,"Facebook");
                    RegistrationModel registrationModel = await GetFacebookAccountInfo(ea.Account);
                    Registration(registrationModel);
                }
                else
                {
                    await DisplayAlert("Authentication Results","Unauthenticated user","Cancel");
                }
            };

            _authenticator.Error += (s, ea) =>
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Error = ").AppendLine($"{ea.Message}");

                DisplayAlert
                (
                    "Authentication Error",
                    sb.ToString(),
                    "OK"
                );
            };

            AuthenticationState.Authenticator = _authenticator;
            this.PresentUILoginScreen(_authenticator);
        }

	    private async Task<RegistrationModel> GetGoogleAccountInfo(Account account)
	    {
	        RegistrationModel registrationModel = null;
            var request = new OAuth2Request("GET", new Uri("https://www.googleapis.com/oauth2/v2/userinfo"), null, account);
	        var response = await request.GetResponseAsync();
	        if (response != null)
	        {
	            string userJson = response.GetResponseText();
	            var user = JsonConvert.DeserializeObject<Dictionary<string,string>>(userJson);
	            registrationModel = new RegistrationModel
	            {
	                Email = user["email"],
	                FirstName = user["given_name"],
	                LastName = user["family_name"],
	                Type = LoginType.Google
	            };
            }

	        return registrationModel;
	    }

	    private async Task<RegistrationModel> GetFacebookAccountInfo(Account account)
	    {
	        RegistrationModel registrationModel = null;
            var request = new OAuth2Request("GET", new Uri("https://graph.facebook.com/v2.7/me?fields=email,first_name,last_name") , null,account);
	        var response = await request.GetResponseAsync();
	        if (response != null)
	        {
	            string userJson = response.GetResponseText();
	            var user = JsonConvert.DeserializeObject<Dictionary<string, string>>(userJson);
                registrationModel = new RegistrationModel
                {
                    Email = user["email"],
                    FirstName = user["first_name"],
                    LastName = user["last_name"],
                    Type = LoginType.Facebook
                };
            }

	        return registrationModel;
	    }

	    public void Registration(RegistrationModel registrationModel)
	    {

	    }
    }
}