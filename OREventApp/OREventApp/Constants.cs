using System;
using System.Collections.Generic;
using System.Text;

namespace OREventApp
{
    class Constants
    {
        private static string MainUrl = "http://192.168.1.6:45457/";//"http://10.0.201.183:45457/";
        public static string EventsUrl = MainUrl+"api/events/{0}/{1}";
        public static string EventUrl = MainUrl + "api/event/get/{0}";
        public static string ImagesUrl = MainUrl + "Images/";
        public static string JoinToEventUrl = MainUrl + "api/event/join";
        public static string LeaveFromEventUrl = MainUrl + "api/event/leave";
    }
}
