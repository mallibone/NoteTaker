using System;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NoteTaker.Models;

namespace NoteTaker.ViewModels
{
    public class NoteViewItem : ViewModelBase
    {
        private readonly NoteItem _noteItem;
        private bool _isBusy;
        private string _title;
        private string _content;
        private DateTime _lastEdited;

        public NoteViewItem(NoteItem noteItem)
        {
            _noteItem = noteItem;
            Title = noteItem.Title;
            Content = noteItem.Content;
            Created = noteItem.Created;
            LastEdited = noteItem.LastEdited;

            DeleteNoteCommand = new RelayCommand(DeleteNoteCommandHandler);
        }

        private async void DeleteNoteCommandHandler()
        {
            IsBusy = true;

            await DeleteNote(this);

            IsBusy = false;
        }

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

        public string Title
        {
            get => _title;
            set
            {
                if (_title == value) return;
                _title = value;
                RaisePropertyChanged(nameof(Title));
            }
        }

        public string Content
        {
            get => _content;
            set
            {
                if (_content == value) return;
                _content = value;
                RaisePropertyChanged(nameof(Content));
            }
        }


        public DateTime Created { get; }

        public DateTime LastEdited
        {
            get => _lastEdited;
            set
            {
                if (Equals(_lastEdited, value)) return;
                _lastEdited = value;
                RaisePropertyChanged(nameof(LastEdited));
            }
        }

        public string LastEditedString => LastEdited.ToString("D");
        public ICommand DeleteNoteCommand { get; }
        public Func<NoteViewItem, Task> DeleteNote { get; set; }

        public NoteItem NoteItem => new NoteItem {Id = _noteItem.Id, Title = Title, Content = Content, Created = Created, LastEdited = LastEdited};
    }
}