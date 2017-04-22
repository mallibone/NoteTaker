using System;
using System.Collections.ObjectModel;
using System.Linq;
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

        public NoteTakerViewModel(INavigationService navigationService, NotesService notesService)
		{
            if (notesService == null)
                throw new ArgumentNullException(nameof(notesService));
            if (navigationService == null)
                throw new ArgumentNullException(nameof(navigationService));

            _navigationService = navigationService;
            _notesService = notesService;
            Init();
        }

        public ObservableCollection<NoteViewItem> Notes { get; private set; }

        internal void Init()
        {
            var notes = _notesService.GetNotes();
            var deleteNote = new Action<NoteViewItem>(RemoveNote);
            Notes = new ObservableCollection<NoteViewItem>(
                notes.Select(n => new NoteViewItem(n){DeleteNote = deleteNote}).ToList()
                );
            RaisePropertyChanged(nameof(Notes));
        }

	    private void RemoveNote(NoteViewItem noteViewItem)
	    {
	        Notes.Remove(noteViewItem);
	        _notesService.Delete(noteViewItem.Note);
	    }

	    internal void NoteSelected(NoteViewItem selectedNote)
		{
            _navigationService.NavigateTo(Locator.ViewNames.EditNotePage, selectedNote.Note);
		}

		internal void NewNote()
		{
			_navigationService.NavigateTo(Locator.ViewNames.EditNotePage, Note.NewNoteId);
		}
	}
}
