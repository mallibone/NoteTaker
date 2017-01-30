using System;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;

namespace NoteTaker
{
	public class NoteTakerViewModel : ViewModelBase
	{
        readonly INavigationService navigationService;
        readonly NotesService notesService;

        public NoteTakerViewModel(INavigationService navigationService, NotesService notesService)
		{
            if (notesService == null)
                throw new ArgumentNullException(nameof(notesService));
            if (navigationService == null)
                throw new ArgumentNullException(nameof(navigationService));

            this.navigationService = navigationService;
            this.notesService = notesService;

            Notes = new ObservableCollection<Note>(this.notesService.GetNotes());
        }

        public ObservableCollection<Note> Notes { get; private set; }

		internal void NoteSelected(Note selectedItem)
		{
			if (selectedItem == null) throw new ArgumentNullException(nameof(selectedItem));
            navigationService.NavigateTo(Locator.ViewNames.EditNotePage, selectedItem.Clone());
		}

		internal void NewNote()
		{
			navigationService.NavigateTo(Locator.ViewNames.EditNotePage);
		}
	}

    
}
