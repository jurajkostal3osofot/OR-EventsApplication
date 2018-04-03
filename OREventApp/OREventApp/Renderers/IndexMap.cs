using System;
using System.Collections.Generic;
using System.Text;
using Shared.Models;
using Xamarin.Forms.Maps;

namespace OREventApp.Renderers
{
    public class IndexMap : Map
    {
        public IndexMap()
        {
            
        }

        public IndexMap(MapSpan mapSpan) : base(mapSpan)
        {
            
        }

        public event EventHandler<MarkerClickEventArgs> MarkerClicked;
        public virtual void OnMarkerClicked(string eventId)
        {
            MarkerClicked?.Invoke(this, new MarkerClickEventArgs { Id = eventId});
        }
    }

    public class MarkerClickEventArgs : EventArgs
    {
        public string Id{ get; set; }
    }
}
