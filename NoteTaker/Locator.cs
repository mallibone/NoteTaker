using System;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace NoteTaker
{
	internal class Locator
	{
		static Locator()
		{
			ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
			//Services
			SimpleIoc.Default.Register<NotesService>();

			//ViewModels
			SimpleIoc.Default.Register<NoteTakerViewModel>();
		}

		/// <summary>
		/// Gets the Main property.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
			"CA1822:MarkMembersAsStatic",
			Justification = "This non-static member is needed for data binding purposes.")]
		public NoteTakerViewModel NoteTakerViewModel => ServiceLocator.Current.GetInstance<NoteTakerViewModel>();

		public class ViewNames
		{
			public static string NoteTakerPage = nameof(NoteTakerPage);
		}
	}
}
