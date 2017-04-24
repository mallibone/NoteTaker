using System;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using NoteTaker.Models;
using NoteTaker.Services;

namespace NoteTaker.ViewModels
{

    public class EditNoteViewModel : ViewModelBase
    {
        NoteItem _currentNoteItem;
        readonly NotesService _notesService;
        readonly INavigationService _navigationService;
        private bool _isBusy;

        public EditNoteViewModel(NotesService notesService, INavigationService navigationService)
        {
            if (navigationService == null) throw new ArgumentNullException(nameof(navigationService));
            if (notesService == null) throw new ArgumentNullException(nameof(notesService));

            this._notesService = notesService;
            this._navigationService = navigationService;

            SaveNote = new RelayCommand(SaveChangesToNote);
            DeleteNote = new RelayCommand(DeleteNoteFromStorage);
        }

        internal void Init(NoteItem noteItem)
        {
            this._currentNoteItem = noteItem;
            RaisePropertyChanged(nameof(Title));
            RaisePropertyChanged(nameof(Content));
        }

        public string Title {
            get
            {
                return _currentNoteItem.Title;
            }
            set
            {
                if (value == null || value == _currentNoteItem.Title) return;
                _currentNoteItem.Title = value;
                RaisePropertyChanged(nameof(Title));
            }
        }

        public string Content
        {
            get
            {
                return _currentNoteItem.Content;
            }
            set
            {
                if (value == null || value == _currentNoteItem.Content) return;
                _currentNoteItem.Content = value;
                RaisePropertyChanged(nameof(Content));
            }
        }

        public RelayCommand DeleteNote { get; private set; }

        public RelayCommand SaveNote { get; private set; }

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

        async void SaveChangesToNote()
        {
            IsBusy = true;
            await _notesService.StoreNote(this._currentNoteItem);
            IsBusy = false;
            _navigationService.GoBack();
        }

        async void DeleteNoteFromStorage()
        {
            IsBusy = true;
            await _notesService.Delete(_currentNoteItem);
            IsBusy = false;
            _navigationService.GoBack();
        }
    }
}
