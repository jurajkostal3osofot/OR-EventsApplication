using System;
using Shared.Models;
using Xamarin.Forms.Maps;

namespace OREventApp.Renderers
{
    public class CustomMap : Map
    {
        
        public CustomMap()
        {
            
        }
        public CustomMap(MapSpan mapSpan) : base(mapSpan)
        {
            
        }

        public Position ObtainCenterMapPosition { get; set; }
        public event EventHandler<PositionEventArgs> PositionChanged;

        public virtual void OnPositionChanged(Position position)
        {
            PositionChanged?.Invoke(this, new PositionEventArgs{ Position = position});
        }
    }

    public class PositionEventArgs : EventArgs
    {
        public Position Position { get; set; }
    }
}
