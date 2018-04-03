using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

namespace OREventApp.Droid
{
    [Activity(Label = "CustomUrlSchemeInterceptor", NoHistory = true , LaunchMode = LaunchMode.SingleTop)]
    [
        IntentFilter
        (
            actions: new[] { Intent.ActionView },
            Categories = new[]
            {
                Intent.CategoryDefault,
                Intent.CategoryBrowsable
            },
            DataSchemes = new[]
            {
                "fb159391811426126",
                "com.companyname.oreventapp"
            },
            DataPaths = new[]
            {
                "/",
                "/oauth2redirect"
            },
            AutoVerify = true
        )
    ]
    public class CustomUrlSchemeInterceptor : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Android.Net.Uri uri_android = Intent.Data;

#if DEBUG
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("ActivityCustomUrlSchemeInterceptor.OnCreate()");
            sb.Append("     uri_android = ").AppendLine(uri_android.ToString());
            System.Diagnostics.Debug.WriteLine(sb.ToString());
#endif

            // Convert iOS NSUrl to C#/netxf/BCL System.Uri - common API
            Uri uriNetfx = new Uri(uri_android.ToString());

            AuthenticationState.Authenticator.OnPageLoading(uriNetfx);
            Finish();
        }
    }
}