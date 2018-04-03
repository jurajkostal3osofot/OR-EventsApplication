using System;
using System.Diagnostics;
using System.Linq;
using OREventApp.Pages;
using Xamarin.Forms;

namespace OREventApp
{
	public partial class App : Application
	{
        private object _accessToken;
	    private bool _loggedIn;
        public App ()
		{
			InitializeComponent();

		    MainPage = new NavigationPage(new MainPage());
            string googleClientId = "192091297496-1765l0j7t0kn6i4hlne5cn36aa0qak32.apps.googleusercontent.com";

		    _loggedIn = false;

		    if (App.Current.Properties.TryGetValue("access_token", out _accessToken))
		    {
		        if (_accessToken.ToString().Length > 0)
		        {
		            _loggedIn = true;
		        }
		    }

            //if (!_loggedIn)
		    if (false)
            {
		        // If we aren't logged in, then this may be the first time we're starting the app, in which case we want to
		        // jam some settings in for our auth that we can retrieve later.  
		        // But MAYBE, we are re-launching an app that was not logged in, in which case jamming these values in would 
		        // cause a crash.  So we wrap them up in an empty try-catch, which is not elegant but is effective.
		        try
		        {
                    /*
                    AddOrUpdateProperties("GoogleClientId", "192091297496-c380rar1uks0c1odcqr07jmd3t4mudbl.apps.googleusercontent.com");
                    AddOrUpdateProperties("GoogleScope", "https://www.googleapis.com/auth/userinfo.email");
                    AddOrUpdateProperties("GoogleAuthorizeUrl", "https://accounts.google.com/o/oauth2/auth");
                    AddOrUpdateProperties("GoogleRedirectUrl", "com.companyname.oreventapp:/oauth2redirect");
                    AddOrUpdateProperties("GoogleClientSecret", "85o82W9Llebd6p7QYXD7iCRV");
                    AddOrUpdateProperties("GoogleAccessTokenUrl", "https://www.googleapis.com/oauth2/v4/token");
                    AddOrUpdateProperties("FacebookClientId", "187455168704201");
                    AddOrUpdateProperties("FacebookAuthorizeUrl", "https://m.facebook.com/dialog/oauth");
                    AddOrUpdateProperties("FacebookRedirectUrl", "http://www.facebook.com/connect/login_success.html");
                    AddOrUpdateProperties("FacebookScope", "public_profile");
                    */

		            //Renderers.LoginPage loginPage = new Renderers.LoginPage();
		            //MainPage = new NavigationPage(new Renderers.LoginPage());
		            Current.MainPage.Navigation.PushModalAsync(new LoginPage());
                }
		        catch(Exception e)
		        {
                    Debug.WriteLine(e);
		        }

                // The root page of your application before login.
		        //MainPage = new NavigationPage(new Renderers.LoginPage());

            }
		    else
		    {
                // If we ARE logged in, then fire up the root page of your application after login.
                //MainPage = new NavigationPage(new MainPage());
            }
        }

        private void AddOrUpdateProperties(string key, string value)
	    {
	        if (Current.Properties.ContainsKey(key))
	        {
	            Current.Properties[key] = value;
            }
	        else
	        {
	            Current.Properties.Add(key,value);
            }
        }

        protected override void OnStart ()
		{
            // Handle when your app starts
        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
