using System;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace NoteTaker
{

    public class EditNoteViewModel : ViewModelBase
    {
        Note currentNote;
        readonly NotesService notesService;
        readonly INavigationService navigationService;

        public EditNoteViewModel(NotesService notesService, INavigationService navigationService)
        {
            if (navigationService == null) throw new ArgumentNullException(nameof(navigationService));
            if (notesService == null) throw new ArgumentNullException(nameof(notesService));

            this.notesService = notesService;
            this.navigationService = navigationService;

            SaveNote = new RelayCommand(SaveChangesToNote);
            DeleteNote = new RelayCommand(DeleteNoteFromStorage);
        }

        internal void Init()
        {
            var note = new Note();
            Init(note);
        }

        internal void Init(Note note)
        {
            if (note == null) throw new ArgumentNullException(nameof(note));
            this.currentNote = note;
            RaisePropertyChanged(nameof(Title));
            RaisePropertyChanged(nameof(Content));
        }

        public string Title {
            get
            {
                return currentNote.Title;
            }
            set
            {
                if (value == null || value == currentNote.Title) return;
                currentNote.Title = value;
                RaisePropertyChanged(nameof(Title));
            }
        }

        public string Content
        {
            get
            {
                return currentNote.Content;
            }
            set
            {
                if (value == null || value == currentNote.Content) return;
                currentNote.Content = value;
                RaisePropertyChanged(nameof(Content));
            }
        }

        public RelayCommand DeleteNote { get; private set; }

        public RelayCommand SaveNote { get; private set; }

        void SaveChangesToNote()
        {
            notesService.StoreNote(this.currentNote);
            this.navigationService.GoBack();
        }

        void DeleteNoteFromStorage()
        {
            throw new NotImplementedException();
        }
    }
}
