using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
using Microsoft.WindowsAzure.MobileServices;
using NoteTaker.Services;
using Xamarin.Forms;

namespace NoteTaker
{
	public partial class App : Application
	{
		private static Locator _locator;
		internal static Locator Locator => _locator ?? (_locator = new Locator());

	    public static readonly MobileServiceClient MobileService =
	        new MobileServiceClient(
	            "https://azurebootcampch.azurewebsites.net"
	        );

		public App()
		{
			InitializeComponent();

            MobileCenter.Start(typeof(Analytics), typeof(Crashes));

            MainPage = InitializeNavigationAndInitialPage();
		}


        private NavigationPage InitializeNavigationAndInitialPage()
		{
			var navService = new NavigationService();
			navService.Configure(Locator.ViewNames.NoteTakerPage, typeof(Views.NoteTakerPage));
            navService.Configure(Locator.ViewNames.EditNotePage, typeof(Views.EditNoteView));
			SimpleIoc.Default.Register<INavigationService>(() => navService);

			var mainPage = new NavigationPage(new Views.NoteTakerPage());
            mainPage.BarBackgroundColor = Color.FromRgb(239, 125, 23);
            mainPage.BarTextColor = Color.White;
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
