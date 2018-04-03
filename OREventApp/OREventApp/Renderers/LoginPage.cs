using System;
using Xamarin.Forms;

namespace OREventApp.Renderers
{
    public class LoginPage : ContentPage
    {
        
        public event EventHandler<LoginSuccessEventArgs> LoginSuccess;
        public virtual void OnLoginSuccess(string eventId)
        {
            LoginSuccess?.Invoke(this, new LoginSuccessEventArgs { Id = eventId });
        }
    }

    public class LoginSuccessEventArgs : EventArgs
    {
        public string Id { get; set; }
    }
}
