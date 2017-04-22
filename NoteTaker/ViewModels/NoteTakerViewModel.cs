using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using NoteTaker.Models;
using NoteTaker.Services;

namespace NoteTaker.ViewModels
{
	public class NoteTakerViewModel : ViewModelBase
	{
        readonly INavigationService _navigationService;
        readonly NotesService _notesService;
	    private bool _isBusy;

	    public NoteTakerViewModel(INavigationService navigationService, NotesService notesService)
		{
            if (notesService == null)
                throw new ArgumentNullException(nameof(notesService));
            if (navigationService == null)
                throw new ArgumentNullException(nameof(navigationService));

            _navigationService = navigationService;
            _notesService = notesService;
        }

        public ObservableCollection<NoteViewItem> Notes { get; private set; }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (_isBusy == value) return;
                _isBusy = value;
                RaisePropertyChanged(nameof(IsBusy));
            }
        }

        internal async Task Init()
        {
            IsBusy = true;
            var notes = await _notesService.GetNotes();
            var deleteNote = new Func<NoteViewItem, Task>(RemoveNote);

            Notes = new ObservableCollection<NoteViewItem>(
                notes.Select(n => new NoteViewItem(n) { DeleteNote = deleteNote }).ToList()
                );
            RaisePropertyChanged(nameof(Notes));
            IsBusy = false;
        }

	    private async Task RemoveNote(NoteViewItem noteViewItem)
	    {
	        IsBusy = true;
	        Notes.Remove(noteViewItem);
	        await _notesService.Delete(noteViewItem.Note);
	        IsBusy = false;
	    }

	    internal void NoteSelected(NoteViewItem selectedNote)
		{
            _navigationService.NavigateTo(Locator.ViewNames.EditNotePage, selectedNote.Note);
		}

		internal void NewNote()
		{
			_navigationService.NavigateTo(Locator.ViewNames.EditNotePage, new Note());
		}
	}
}
