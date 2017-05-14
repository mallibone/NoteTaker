using System.Collections.Generic;
using System.Threading.Tasks;
using NoteTaker.Models;

namespace NoteTaker.Services.Notes
{
    public interface INotesService
    {
        Task<bool> Delete(NoteItem noteItem);
        Task<IEnumerable<NoteItem>> GetNotes(bool refresh = false);
        Task<bool> StoreNote(NoteItem noteItem);
    }
}