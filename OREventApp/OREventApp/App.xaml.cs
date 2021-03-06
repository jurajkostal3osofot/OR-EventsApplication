﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OREventApp.Pages;
using Xamarin.Forms;

namespace OREventApp
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

			//MainPage = new NavigationPage(new OREventApp.MainPage());
		    var tabs = new TabbedPage();
		    tabs.Children.Add(new IndexPage());
		    tabs.Children.Add(new AddEventPage());
		    tabs.Children.Add(new EventsNearPage());

		    MainPage = tabs;
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
