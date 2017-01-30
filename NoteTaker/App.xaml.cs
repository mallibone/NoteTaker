using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;

namespace NoteTaker
{
	public partial class App : Application
	{
		private static Locator _locator;
		internal static Locator Locator => _locator ?? (_locator = new Locator());

		public App()
		{
			InitializeComponent();

			//MobileCenter.Start(typeof(Analytics), typeof(Crashes));

			MainPage = InitializeNavigationAndInitialPage();
		}

		private NavigationPage InitializeNavigationAndInitialPage()
		{
			var navService = new NavigationService();
			navService.Configure(Locator.ViewNames.NoteTakerPage, typeof(NoteTakerPage));
            navService.Configure(Locator.ViewNames.EditNotePage, typeof(EditNoteView));
			SimpleIoc.Default.Register<INavigationService>(() => navService);

			var mainPage = new NavigationPage(new NoteTakerPage());
			navService.Initialize(mainPage);
			return mainPage;
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}

    
}
