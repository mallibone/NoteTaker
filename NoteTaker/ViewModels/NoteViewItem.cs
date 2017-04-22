using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using NoteTaker.Models;

namespace NoteTaker.ViewModels
{
    public class NoteViewItem
    {
        public NoteViewItem(Note note)
        {
            Id = note.Id;
            Title = note.Title;
            Content = note.Content;
            Created = note.Created;
            LastEdited = note.LastEdited;

            DeleteNoteCommand = new RelayCommand(() => DeleteNote(this));
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastEdited { get; set; }
        public string LastEditedString => LastEdited.ToString("D");
        public ICommand DeleteNoteCommand { get; set; }
        public Action<NoteViewItem> DeleteNote { get; set; }
    }
}