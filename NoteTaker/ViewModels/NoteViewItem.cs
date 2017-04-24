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
        private NoteItem _noteItem;
        private bool _isBusy;

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

        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastEdited { get; set; }
        public string LastEditedString => LastEdited.ToString("D");
        public ICommand DeleteNoteCommand { get; set; }
        public Func<NoteViewItem, Task> DeleteNote { get; set; }
        public NoteItem NoteItem => new NoteItem {Id = _noteItem.Id, Title = Title, Content = Content, Created = Created, LastEdited = LastEdited};
    }
}