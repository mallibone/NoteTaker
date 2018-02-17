using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using NoteTaker.Models;
using NoteTaker.Services.Notes;

namespace NoteTaker.ViewModels
{
	public class NoteTakerViewModel : ViewModelBase
	{
        readonly INavigationService _navigationService;
        readonly INotesService _notesService;
	    private bool _isBusy;

	    public NoteTakerViewModel(INavigationService navigationService, INotesService notesService)
		{
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            _notesService = notesService ?? throw new ArgumentNullException(nameof(notesService));

		    RefreshCommand = new RelayCommand(async () => await RefreshNotes(), () => !_isBusy);

		    _notesService.NoteAdded += AddNote;
		    _notesService.NoteDeleted += DeleteNote;
		    _notesService.NoteEdited += UpdateNote;
		}

	    public ObservableCollection<NoteViewItem> Notes { get; private set; }
        public ICommand RefreshCommand { get; } 

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
            if (Notes != null && Notes.Any()) return;
            await LoadNotes();
        }

	    internal void NoteSelected(NoteViewItem selectedNote)
		{
            _navigationService.NavigateTo(Locator.ViewNames.EditNotePage, selectedNote.NoteItem.Id);
		}

		internal void NewNote()
		{
			_navigationService.NavigateTo(Locator.ViewNames.EditNotePage, new NoteItem());
		}

	    private async Task LoadNotes()
	    {
	        IsBusy = true;
	        var notes = await _notesService.GetNotes();

	        Notes = new ObservableCollection<NoteViewItem>(
	            notes.Select(n => new NoteViewItem(n) {DeleteNote = RemoveNote})
	                .OrderByDescending(n => n.NoteItem.LastEdited)
	                .ToList()
	        );
	        RaisePropertyChanged(nameof(Notes));
	        IsBusy = false;
	    }

	    private async Task RemoveNote(NoteViewItem noteViewItem)
	    {
	        IsBusy = true;
	        Notes.Remove(noteViewItem);
	        await _notesService.Delete(noteViewItem.NoteItem);
	        IsBusy = false;
	    }

	    private async Task RefreshNotes()
	    {
	        await LoadNotes();
	    }

	    private void UpdateNote(object sender, NoteEvent e)
	    {
	        var note = e.Note;
	        var noteItem = Notes.First(n => n.NoteItem.Id.Equals(note.Id));

	        noteItem.Title = note.Title;
	        noteItem.LastEdited = note.LastEdited;
	        noteItem.Content = note.Content;
	    }

	    private void DeleteNote(object sender, NoteEvent e)
	    {
	        var noteId = e.Note.Id;
	        var noteViewItem = Notes.First(n => n.NoteItem.Id.Equals(noteId));
	        Notes.Remove(noteViewItem);
	    }

	    private void AddNote(object sender, NoteEvent e)
	    {
            Notes.Add(new NoteViewItem(e.Note) {DeleteNote = RemoveNote});
	        Notes = new ObservableCollection<NoteViewItem>(Notes.OrderByDescending(n => n.NoteItem.LastEdited).ToList());
	    }
	}
}
