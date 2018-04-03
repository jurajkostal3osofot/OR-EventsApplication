using System;
using System.Linq;
using OREventApp.Helpers;
using OREventApp.Models;
using OREventApp.Pages;
using Shared.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OREventApp.ViewCells
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EventCell : ViewCell
	{
	    private EventShared _eventShared;
	    private long _currentUserId = 2;
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
	        _eventShared = _context.EventShared;
            Heading.Text = _context.Heading;
	        UpdateNumberOfAttendates();
	        MiniMap.WidthRequest = MiniMap.HeightRequest = RightSideofView.Height;
	        MiniMap.MinimumWidthRequest = MiniMap.MinimumHeightRequest = RightSideofView.Height;
            MiniMap.Source = _context.MiniMap;
	        ShowActionButton();
	    }

	    private void UpdateNumberOfAttendates()
	    {
	        NumberOfAttendates.Text = $"{_eventShared.NumberOfLoggedInUsers} attendates";
        }

	    private void ShowActionButton()
	    {
	        if (_eventShared.LoggedInUsers.Select(x => x.Id == _currentUserId).Any())
	        {
	            JoinButton.IsVisible = false;
	            LeaveButton.IsVisible = true;
	        }

	        if (_eventShared.UserId == _currentUserId)
	        {
	            JoinButton.IsVisible = false;
	            LeaveButton.IsVisible = false;
	        }
        }

	    private async void JoinButtonClicked(object sender, EventArgs e)
	    {
	        JoinButton.IsVisible = false;
	        await new EventHelper().JoinToEventAsync(_eventShared.Id, _currentUserId);
	        LeaveButton.IsVisible = true;
	        ++_eventShared.NumberOfLoggedInUsers;
            UpdateNumberOfAttendates();
            AddCurrentUserToEvent();
	    }

	    private async void DetailButtonClicked(object sender, EventArgs e)
	    {
	        EventShared newEvent = await new EventHelper().GetEventAsync(_eventShared.Id);
	        await Application.Current.MainPage.Navigation.PushModalAsync(new DetailEventPage(newEvent));
        }

	    private async void LeaveButtonClicked(object sender, EventArgs e)
	    {
            LeaveButton.IsVisible = false;
	        await new EventHelper().LeaveFromEventAsync(_eventShared.Id, _currentUserId);
	        JoinButton.IsVisible = true;
	        --_eventShared.NumberOfLoggedInUsers;
	        UpdateNumberOfAttendates();
            RemoveCurrentUserFromEvent();
        }

	    private void AddCurrentUserToEvent()
	    {
	        EventShared eventShared = EventsNearPage._events.FirstOrDefault(x => x.Id == _eventShared.Id);
	        if (eventShared != null)
	        {
	            ++eventShared.NumberOfLoggedInUsers;
                eventShared.LoggedInUsers.Add(new User
                {
                    Id = _currentUserId,
                    Email = "test2@test.com"
                });
	        }
        }

	    private void RemoveCurrentUserFromEvent()
	    {
	        EventShared eventShared = EventsNearPage._events.FirstOrDefault(x => x.Id == _eventShared.Id);
	        if (eventShared != null)
	        {
	            --eventShared.NumberOfLoggedInUsers;
	            User currentUser = eventShared.LoggedInUsers.FirstOrDefault(x => x.Id == _currentUserId);
	            if (currentUser != null)
	            {
	                eventShared.LoggedInUsers.Remove(currentUser);
                }
	        }
	    }
    }
}