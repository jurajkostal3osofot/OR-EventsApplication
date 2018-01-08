using System.Collections.Generic;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using OREventApp.Droid;
using OREventApp.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using View = Android.Views.View;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace OREventApp.Droid
{
    public class CustomMapRenderer : MapRenderer,  GoogleMap.IOnMapClickListener
    {
        private CustomMap formsMap;

        public CustomMapRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
            
            }

            if (e.NewElement != null)
            {
                formsMap = (CustomMap)e.NewElement;
                Control.GetMapAsync(this);
            }
        }

        protected override void OnMapReady(GoogleMap map)
        {
            base.OnMapReady(map);

            NativeMap.SetOnMapClickListener(this);
        }

        protected override MarkerOptions CreateMarker(Pin pin)
        {
            var marker = new MarkerOptions();
            marker.SetPosition(new LatLng(pin.Position.Latitude, pin.Position.Longitude));
            marker.SetTitle(pin.Label);
            marker.SetSnippet(pin.Address);
            marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.ic_launcher));
            return marker;
        }



        public void OnMapClick(LatLng point)
        {
            var pin = new Pin()
            {
                Position = new Position(point.Latitude, point.Longitude),
                Label = "New event"
            };
            formsMap.Pins.Clear();
            formsMap.Pins.Add(pin);   
        }
    }

}