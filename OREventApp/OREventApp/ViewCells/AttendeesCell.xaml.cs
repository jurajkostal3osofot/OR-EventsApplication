using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OREventApp.ViewCells
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AttendeesCell : ViewCell
	{
	    private Models.AttendeesCell _context;
        public AttendeesCell ()
		{
			InitializeComponent ();
		}

	    protected override void OnBindingContextChanged()
	    {
	        _context = (Models.AttendeesCell)BindingContext;
	    }

	    protected override void OnAppearing()
	    {
	        Name.Text = _context.Name;
	        Description.Text = _context.Description;
	    }
    }
}