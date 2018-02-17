using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NoteTaker.Models;
using NoteTaker.Utils;

namespace NoteTaker.Services.Notes.Impl
{
    public class NotesServiceStub : INotesService
    {
        private readonly List<NoteItem> _notes;

        public event EventHandler<NoteEvent> NoteDeleted;
        public event EventHandler<NoteEvent> NoteEdited;
        public event EventHandler<NoteEvent> NoteAdded;

        public NotesServiceStub()
        {
            _notes = new List<NoteItem>
            {
                new NoteItem {Id = "1", Title = "Global Azure Bootcamp", Content = "Show how quickly remote storage, authentication and push can be added to an app.\n\n=>From Zero to Hero", Created = DateTime.Now.AddMonths(-1), LastEdited = DateTime.Now.AddMonths(-1)},
                new NoteItem {Id = "2", Title = "Plan Date Night", Content = "Don't forget to ask your wife to be your valentine on the 14. February", Created = DateTime.Now.AddDays(-7), LastEdited = DateTime.Now.AddDays(-7)},
                new NoteItem {Id = "3", Title = "Answers for Azure Mobile App Service Q&A", Content = "- It depends\n- 42", Created = DateTime.Now.AddDays(-1), LastEdited = DateTime.Now.AddDays(-1)},
                new NoteItem {Id = "4", Title = "Spacey Wacey Stuff", Content = "Dr. Who things and what not.", Created = DateTime.Now.AddHours(-1), LastEdited = DateTime.Now.AddHours(-1)},
            };
        }

        async Task<bool> INotesService.Delete(NoteItem noteItem)
        {
            if (noteItem == null) throw new ArgumentNullException(nameof(noteItem));

            if(!_notes.Any(n => n.Equals(noteItem))) return false;

            await Task.Delay(500);

            _notes.Remove(noteItem);

            NoteDeleted?.Invoke(this, new NoteEvent(noteItem.Clone()));

            return true;
        }

        async Task<IEnumerable<NoteItem>> INotesService.GetNotes(bool refresh)
        {
            await Task.Delay(500);
            return _notes.Clone();
        }

        public async Task<bool> StoreNote(NoteItem note)
        {
            if (note == null) throw new ArgumentNullException(nameof(note));

            await Task.Delay(500);

            NoteItem storedNote;

            if (!string.IsNullOrEmpty(note.Id))
            {
                storedNote = _notes.FirstOrDefault(n => n.Id == note.Id);
                if (storedNote == null) return false;

                storedNote.Title = note.Title;
                storedNote.Content = note.Content;
                storedNote.LastEdited = DateTime.Now;
                NoteEdited?.Invoke(this, new NoteEvent(storedNote.Clone()));
            }
            else
            {
                storedNote = note;
                storedNote.Id = (_notes.Last().Id + 1); // todo: replace with Snowflake ID...
                _notes.Add(storedNote);
                NoteAdded?.Invoke(this, new NoteEvent(storedNote.Clone()));
            }

            return true;
        }

        public async Task<NoteItem> GetNote(string id)
        {
            await Task.Delay(500);
            return _notes.FirstOrDefault(n => n.Id.Equals(id, StringComparison.Ordinal))?.Clone();
        }
    }
}
