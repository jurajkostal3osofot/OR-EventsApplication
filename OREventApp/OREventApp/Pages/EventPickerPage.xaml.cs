using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OREventApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EventPickerPage : ContentPage
	{
	    public EventPickerPage()
	    {
	        InitializeComponent();
	        PickerEventsList.ItemsSource = new List<string>
	        {
	            "None",
	            "Football",
	            "Florball"
	        };

	    }
	    public ListView GetPickerEvents
	    {
	        get { return PickerEventsList; }
	    }
    }
}