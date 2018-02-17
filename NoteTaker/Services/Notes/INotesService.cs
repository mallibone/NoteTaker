using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NoteTaker.Models;

namespace NoteTaker.Services.Notes
{
    public interface INotesService
    {
        Task<NoteItem> GetNote(string id);
        Task<bool> Delete(NoteItem noteItem);
        Task<IEnumerable<NoteItem>> GetNotes(bool refresh = false);
        Task<bool> StoreNote(NoteItem noteItem);

        event EventHandler<NoteEvent> NoteDeleted;
        event EventHandler<NoteEvent> NoteAdded;
        event EventHandler<NoteEvent> NoteEdited;
    }
}