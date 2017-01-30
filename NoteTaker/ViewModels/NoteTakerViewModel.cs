using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;

namespace NoteTaker
{
	public class NoteTakerViewModel : ViewModelBase
	{
        readonly INavigationService navigationService;

        public NoteTakerViewModel(INavigationService navigationService)
		{
            if (navigationService == null)
                throw new ArgumentNullException(nameof(navigationService));

            this.navigationService = navigationService;
        }

		internal void NoteSelected(Note selectedItem)
		{
			if (selectedItem == null) throw new ArgumentNullException(nameof(selectedItem));
            navigationService.NavigateTo(Locator.ViewNames.EditNotePage, selectedItem);
		}

		internal void NewNote()
		{
			navigationService.NavigateTo(Locator.ViewNames.EditNotePage);
		}
	}
}
