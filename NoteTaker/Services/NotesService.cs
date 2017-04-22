using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using NoteTaker.Models;
using NoteTaker.Utils;

namespace NoteTaker.Services
{
	public class NotesService
	{
        private readonly List<Note> Notes = new List<Note>();
	    private IMobileServiceSyncTable<Note> _notesTable;

	    public NotesService()
		{
		    CurrentPlatform.Init();
		}

        internal async Task<IEnumerable<Note>> GetNotes(bool refresh = false)
        {
            if(Notes.Any() && !refresh) return Notes.Clone();
            var store = new MobileServiceSQLiteStore(Constants.NotesDbName);
            store.DefineTable<Note>();

            if (_notesTable == null)
            {
                await App.MobileService.SyncContext.InitializeAsync(store);

                _notesTable = App.MobileService.GetSyncTable<Note>();

                await SyncWithRemote();
            }

            var notes = await _notesTable.ToListAsync();

		    Notes.Clear();
            Notes.AddRange(notes);

            return Notes.Clone();
        }

        internal async Task<bool> StoreNote(Note note)
        {
            if (note == null) throw new ArgumentNullException(nameof(note));

            Note storedNote = Notes.FirstOrDefault(n => n.Equals(note));

            if (storedNote != null)
            {
                storedNote.Title = note.Title;
                storedNote.Content = note.Content;
                storedNote.LastEdited = DateTime.Now;
                await _notesTable.UpdateAsync(storedNote);
            }
            else
            {
                storedNote = note;
                Notes.Add(storedNote);
                await _notesTable.InsertAsync(storedNote);
            }

            await SyncWithRemote();

            return true;
        }

	    public async Task<bool> Delete(Note note)
	    {
	        if (note == null) throw new ArgumentNullException(nameof(note));
            var itemDeletedFromCache = Notes.Remove(note);

	        if (itemDeletedFromCache)
	        {
	            await _notesTable.DeleteAsync(note);
                await SyncWithRemote();
            }

            return itemDeletedFromCache;
	    }

	    private async Task SyncWithRemote()
	    {
	        ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

	        try
	        {
	            await App.MobileService.SyncContext.PushAsync();

	            await _notesTable.PullAsync(
	                "allNoteItems",
	                _notesTable.CreateQuery());
	        }
	        catch (MobileServicePushFailedException exc)
	        {
	            if (exc.PushResult != null)
	            {
	                syncErrors = exc.PushResult.Errors;
	            }
	        }

	        // Simple error/conflict handling.
	        if (syncErrors != null)
	        {
	            foreach (var error in syncErrors)
	            {
	                if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
	                {
	                    //Update failed, reverting to server's copy.
	                    await error.CancelAndUpdateItemAsync(error.Result);
	                }
	                else
	                {
	                    // Discard local change.
	                    await error.CancelAndDiscardItemAsync();
	                }

	                Debug.WriteLine(@"Error executing sync operation. Item: {0} ({1}). Operation discarded.",
	                    error.TableName, error.Item["id"]);
	            }
	        }

        }
    }
}
