using System;
using System.Collections.Generic;
using System.Text;

namespace OREventApp.Interfaces
{
    public interface INotification
    {
        void Notify(string message, int duration, string action, Action<object> callback);
        void ShowNumberEvents(string message, int duration, Action<object> callback);
    }
}
