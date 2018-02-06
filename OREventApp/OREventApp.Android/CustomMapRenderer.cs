using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Widget;
using Java.Lang;
using OREventApp.Droid;
using OREventApp.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Xaml;
using View = Android.Views.View;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]

namespace OREventApp.Droid
{
    public class CustomMapRenderer : MapRenderer, GoogleMap.IOnCameraIdleListener
    {
        private CustomMap formsMap;
        private double lat;
        private double lng;

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
                formsMap = (CustomMap) e.NewElement;
                Control.GetMapAsync(this);
            }
        }

        protected override void OnMapReady(GoogleMap map)
        {
            base.OnMapReady(map);
            NativeMap.SetOnCameraIdleListener(this);
            NativeMap.MoveCamera(CameraUpdateFactory.NewLatLng(NativeMap.CameraPosition.Target));
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

        public void OnCameraIdle()
        {
            System.Diagnostics.Debug.WriteLine(NativeMap.CameraPosition.Target.Latitude + " ## " +
                                               NativeMap.CameraPosition.Target.Longitude);
            formsMap.OnPositionChanged(new Position(NativeMap.CameraPosition.Target.Latitude,
                NativeMap.CameraPosition.Target.Longitude));
        }
    }
}