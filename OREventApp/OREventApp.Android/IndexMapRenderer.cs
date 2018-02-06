using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Util;
using Java.IO;
using OREventApp.Droid;
using OREventApp.Renderers;
using Shared.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using Bitmap = Android.Graphics.Bitmap;
using Image = Xamarin.Forms.Image;
using Point = Xamarin.Forms.Point;
using Rectangle = Xamarin.Forms.Rectangle;
using View = Android.Views.View;

[assembly: ExportRenderer(typeof(IndexMap), typeof(IndexMapRenderer))]
namespace OREventApp.Droid
{
    public class IndexMapRenderer : MapRenderer
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
  
    }

}