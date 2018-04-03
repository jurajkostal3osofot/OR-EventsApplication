using System;
using System.Collections.ObjectModel;
using System.Linq;
using OREventApp.Models;
using Shared.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OREventApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailEventPage : ContentPage
	{
	    private EventShared _eventShared;
	    private ObservableCollection<AttendeesCell> _listOfCollection;
	    private long _currentUserId = 2;
        public DetailEventPage (EventShared eventShared)
		{
			InitializeComponent ();
		    _eventShared = eventShared;
            _listOfCollection = new ObservableCollection<AttendeesCell>();
		    InitEvent();
		}

	    public void InitEvent()
	    {
	        Title.Text = _eventShared.Title;
	        Description.Text = _eventShared.Title;

	        UsersLoggedIn.ItemSelected += (sender, e) => { ((ListView)sender).SelectedItem = null; };
	        
            foreach (var user in _eventShared.LoggedInUsers)
	        {
	            _listOfCollection.Add(new AttendeesCell
	            {
                    Name = user.Email,
                    Description = user.Id == _eventShared.UserId ? "Owner" : ""
                });
            }

	        UsersLoggedIn.ItemsSource = _listOfCollection;
	        if (_eventShared.UserId != _currentUserId)
	        {
	            if(_eventShared.LoggedInUsers.Select(x => x.Id == _currentUserId).Any())
	            {
	                LeaveButton.IsVisible = true;
	                JoinButton.IsVisible = false;
	            }
            }
	        else
	        {
	            LeaveButton.IsVisible = false;
	            JoinButton.IsVisible = false;
            }
	    }

	    private async void JoinButtonClicked(object sender, EventArgs e)
	    {
	        JoinButton.IsVisible = false;
            await new Helpers.EventHelper().JoinToEventAsync(_eventShared.Id, _currentUserId);
            LeaveButton.IsVisible = true;
        }

	    private async void LeaveButtonClicked(object sender, EventArgs e)
	    {
	        LeaveButton.IsVisible = false;
            await new Helpers.EventHelper().LeaveFromEventAsync(_eventShared.Id, _currentUserId);
            JoinButton.IsVisible = true;
        }
	}
}