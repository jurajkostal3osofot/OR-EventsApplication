using System;
using Android.App;
using Android.Content;
using OREventApp.Droid.Renderers;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Application = Xamarin.Forms.Application;
using LoginPage = OREventApp.Renderers.LoginPage;

[assembly: ExportRenderer(typeof(LoginPage), typeof(LoginPageRenderer))]
namespace OREventApp.Droid.Renderers
{
#pragma warning disable CS0618 // Type or member is obsolete
    public class LoginPageRenderer : PageRenderer
    {
        private LoginPage _loginPage;
        public static OAuth2Authenticator _authGoogle;
        bool _done = false;

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {

            }

            if (e.NewElement != null)
            {
                _loginPage = (LoginPage)e.NewElement;
                Init();
            }

            
        }

        private void Init()
        {
            if (!_done)
            {

                // this is a ViewGroup - so should be able to load an AXML file and FindView<>
                var activity = this.Context as Activity;
                CustomTabsConfiguration.CustomTabsClosingMessage = null;
                _authGoogle = new OAuth2Authenticator(
                    clientId: Application.Current.Properties["GoogleClientId"].ToString(),
                    scope: Application.Current.Properties["GoogleScope"].ToString(),
                    authorizeUrl: new Uri(Application.Current.Properties["GoogleAuthorizeUrl"].ToString()),
                    redirectUrl: new Uri(Application.Current.Properties["GoogleRedirectUrl"].ToString()),
                    isUsingNativeUI: true,
                    clientSecret: "",
                    accessTokenUrl: new Uri("https://www.googleapis.com/oauth2/v4/token")
                    );

                /*var authFacebook = new OAuth2Authenticator(
                    clientId: Application.Current.Properties["FacebookClientId"].ToString(),
                    scope: Application.Current.Properties["FacebookScope"].ToString(),
                    authorizeUrl: new Uri(Application.Current.Properties["FacebookAuthorizeUrl"].ToString()),
                    redirectUrl: new Uri(Application.Current.Properties["FacebookRedirectUrl"].ToString()),
                    isUsingNativeUI: true);
                */
                _authGoogle.BrowsingCompleted += (sender, args) =>
                {
                    Console.WriteLine("");
                };
                
                _authGoogle.Completed += (sender, eventArgs) =>
                {
                    Application.Current.MainPage = new NavigationPage(new MainPage());

                    if (eventArgs.IsAuthenticated)
                    {
                        Application.Current.Properties["google_access_token"] = eventArgs.Account.Properties["access_token"].ToString();
                        Application.Current.Properties["google_refresh_token"] = eventArgs.Account.Properties["refresh_token"].ToString();
                        AccountStore.Create (activity).Save (eventArgs.Account, "Google");
                        //Application.Current.MainPage.Navigation.PushAsync(new IndexPage());
                        //Application.Current.MainPage.Navigation.PopModalAsync(true);
                    }
                    else
                    {
                        Application.Current.Properties["access_token"] = string.Empty;
                        Application.Current.Properties["google_refresh_token"] = string.Empty;
                    }
                    _loginPage.OnLoginSuccess(Application.Current.Properties["access_token"].ToString());
                };

                //auth.AllowCancel = false;
                var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
                presenter.Login(_authGoogle);


                Intent intentAuthGoogle = _authGoogle.AuthenticationUIPlatformSpecificNative(activity).SetFlags(ActivityFlags.SingleTop);
                
                activity?.StartActivity(intentAuthGoogle);
                //var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
                //presenter.Login(_authGoogle);
                //Application.Current.MainPage.Navigation.PopToRootAsync();
                _done = true;
            }
            else
            {
                Application.Current.MainPage = new NavigationPage(new MainPage());
            }
        }

        public static void OnPageLoading(Uri uri)
        {
            _authGoogle.OnPageLoading(uri);
        }
    }
#pragma warning restore CS0618 // Type or member is obsolete
}