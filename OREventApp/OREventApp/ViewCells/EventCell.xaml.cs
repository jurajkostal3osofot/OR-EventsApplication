using OREventApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OREventApp.ViewCells
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EventCell : ViewCell
	{
	    private CellModel _context;
		public EventCell ()
		{
			InitializeComponent (); 
        }

	    protected override void OnBindingContextChanged()
	    {
	        _context = (CellModel) BindingContext;
	    }

	    protected override void OnAppearing()
	    {
	        Heading.Text = _context.Heading;
	        NumberOfAttendates.Text = _context.NumberOfAttendates;
	        MiniMap.WidthRequest = MiniMap.HeightRequest = RightSideofView.Height;
	        MiniMap.MinimumWidthRequest = MiniMap.MinimumHeightRequest = RightSideofView.Height;
            MiniMap.Source = _context.MiniMap;
	    }
	}
}