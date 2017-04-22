using System;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using NoteTaker.Models;
using NoteTaker.Services;
using NoteTaker.Utils;

namespace NoteTaker.ViewModels
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
            Init();
        }

        public ObservableCollection<NoteViewItem> Notes { get; private set; }

        internal void Init()
        {
            var notes = this.notesService.GetNotes();
            var deleteNote = new Action<NoteViewItem>((n) => Notes.Remove(n));
            Notes = new ObservableCollection<NoteViewItem>(
                notes.Select(n => new NoteViewItem(n){DeleteNote = deleteNote}).ToList()
                );
            RaisePropertyChanged(nameof(Notes));
        }

		internal void NoteSelected(int noteId)
		{
            navigationService.NavigateTo(Locator.ViewNames.EditNotePage, noteId);
		}

		internal void NewNote()
		{
			navigationService.NavigateTo(Locator.ViewNames.EditNotePage, Note.NewNoteId);
		}
	}
}
