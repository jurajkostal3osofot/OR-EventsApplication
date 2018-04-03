using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace OREventApp.Helpers
{
    public static class PageHelpers
    {
        static PageHelpers()
        {
        }

        public static void PresentUILoginScreen
                            (
                                this Page page,
                                Xamarin.Auth.Authenticator authenticator
                            )
        {
            //if (Settings.IsFormsImplementationRenderers)
            if (false)
            {
                // Renderers Implementaion

                Xamarin.Auth.XamarinForms.AuthenticatorPage ap;
                ap = new Xamarin.Auth.XamarinForms.AuthenticatorPage()
                {
                    Authenticator = authenticator,
                };

                NavigationPage np = new NavigationPage(ap);

                //if (Settings.IsFormsNavigationPushModal)
                if (false)
                {
                    System.Diagnostics.Debug.WriteLine("Presenting");
                    System.Diagnostics.Debug.WriteLine("        PushModal");
                    System.Diagnostics.Debug.WriteLine("        Custom Renderers");

                    page.Navigation.PushModalAsync(np);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Presenting");
                    System.Diagnostics.Debug.WriteLine("        Push");
                    System.Diagnostics.Debug.WriteLine("        Custom Renderers");

                    page.Navigation.PushAsync(np);
                }
            }
            else
            {
                // Presenters Implementation

                //if (Settings.IsFormsNavigationPushModal)
                if (false)
                {
                    System.Diagnostics.Debug.WriteLine("Presenting");
                    System.Diagnostics.Debug.WriteLine("        PushModal");
                    System.Diagnostics.Debug.WriteLine("        Presenters");

                    Xamarin.Auth.Presenters.OAuthLoginPresenter presenter = null;
                    presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
                    presenter.Login(authenticator);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Presenting");
                    System.Diagnostics.Debug.WriteLine("        Push");
                    System.Diagnostics.Debug.WriteLine("        Presenters");

                    Xamarin.Auth.Presenters.OAuthLoginPresenter presenter = null;
                    presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
                    presenter.Login(authenticator);
                }
            }

            return;
        }
    }
}
