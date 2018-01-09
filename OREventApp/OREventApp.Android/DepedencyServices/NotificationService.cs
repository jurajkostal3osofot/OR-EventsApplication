using System;
using Android.App;
using Android.Support.Design.Widget;
using OREventApp.Droid.DepedencyServices;
using OREventApp.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(NotificationService))]
namespace OREventApp.Droid.DepedencyServices
{
    public class NotificationService : INotification
    {
        public void Notify(
            string message,
            int duration,
            string actionText,
            Action<object> action)
        {
            var view = ((Activity)Forms.Context).FindViewById(Android.Resource.Id.Content);
            var snack = Snackbar.Make(view, message, duration);
            if (actionText != null && action != null)
                snack.SetAction(actionText, action);
            snack.Show();
            snack.SetActionTextColor(Android.Graphics.Color.DarkRed);
        }

        public void ShowNumberEvents(string message, int duration, Action<object> action)
        {
            var view = ((Activity)Forms.Context).FindViewById(Android.Resource.Id.Content);
            var snack = Snackbar.Make(view, message, duration);
            snack.Show();
            snack.SetActionTextColor(Android.Graphics.Color.DarkRed);
            
        }
    }
}