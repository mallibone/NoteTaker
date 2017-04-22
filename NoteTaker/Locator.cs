using System;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using NoteTaker.Services;
using NoteTaker.ViewModels;

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
            SimpleIoc.Default.Register<EditNoteViewModel>();
		}

		/// <summary>
		/// Gets the Main property.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
			"CA1822:MarkMembersAsStatic",
			Justification = "This non-static member is needed for data binding purposes.")]
		public NoteTakerViewModel NoteTakerViewModel => ServiceLocator.Current.GetInstance<NoteTakerViewModel>();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public EditNoteViewModel EditNoteViewModel => ServiceLocator.Current.GetInstance<EditNoteViewModel>();

		public class ViewNames
		{
			public static string NoteTakerPage = nameof(NoteTakerPage);
            public static string EditNotePage = nameof(Views.EditNoteView);
		}
	}
}
