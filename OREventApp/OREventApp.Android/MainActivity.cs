using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using FFImageLoading.Forms.Droid;
using Plugin.Permissions;

namespace OREventApp.Droid
{
    [Activity(Label = "OREventApp", Icon = "@drawable/splashscreen_logo", Theme = "@style/MyTheme.Splash", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.SetTheme(Resource.Style.MainTheme);

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            global::Xamarin.Auth.Presenters.XamarinAndroid.AuthenticationConfiguration.Init(this, bundle);
            global::Xamarin.Auth.CustomTabsConfiguration.CustomTabsClosingMessage = null;

            /*
            global::Android.Graphics.Color color_xamarin_blue;
            color_xamarin_blue = new global::Android.Graphics.Color(0x34, 0x98, 0xdb);
            global::Xamarin.Auth.CustomTabsConfiguration.ToolbarColor = color_xamarin_blue;
            */

            global::Xamarin.Auth.CustomTabsConfiguration.
                    ActivityFlags =
                global::Android.Content.ActivityFlags.NoHistory
                |
                global::Android.Content.ActivityFlags.SingleTop
                |
                global::Android.Content.ActivityFlags.NewTask
                ;

            global::Xamarin.Auth.CustomTabsConfiguration.IsWarmUpUsed = true;
            global::Xamarin.Auth.CustomTabsConfiguration.IsPrefetchUsed = true;

            Xamarin.FormsMaps.Init(this, bundle);
            LoadApplication(new App());
            CachedImageRenderer.Init(enableFastRenderer: true);
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity = this;

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

