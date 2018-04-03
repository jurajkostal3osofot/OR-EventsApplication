using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using OREventApp.Droid.Renderers;
using OREventApp.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;

[assembly: ExportRenderer(typeof(IndexMap), typeof(IndexMapRenderer))]
namespace OREventApp.Droid.Renderers
{
    public class IndexMapRenderer : MapRenderer, GoogleMap.IOnMarkerClickListener
    {
        private IndexMap _formsMap;

        public IndexMapRenderer(Context context) : base(context)
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
                _formsMap = (IndexMap)e.NewElement;
                Control.GetMapAsync(this);
            }
        }

        protected override void OnMapReady(GoogleMap map)
        {
            base.OnMapReady(map);
            NativeMap.SetOnMarkerClickListener(this);
        }

        protected override MarkerOptions CreateMarker(Pin pin)
        {
            var marker = new MarkerOptions();
            marker.SetPosition(new LatLng(pin.Position.Latitude, pin.Position.Longitude));
            marker.SetTitle(pin.Label);
            marker.SetSnippet(pin.Address);
            switch (pin.Id)
            {
                case 1:
                    marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.ic_soccer));
                    break;
                case 2:
                    marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.ic_bike));
                    break;
                case 3:
                    marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.ic_sky));
                    break;
                default:
                    marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.ic_launcher));
                    break;
            }
            
            return marker;
        }

        public bool OnMarkerClick(Marker marker)
        {
            _formsMap.OnMarkerClicked(marker.Id);
            return true;
        }
    }
}