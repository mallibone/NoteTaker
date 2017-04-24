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
        private readonly List<NoteItem> Notes = new List<NoteItem>();
	    private IMobileServiceSyncTable<NoteItem> _notesTable;

	    public NotesService()
		{
		    CurrentPlatform.Init();
		}

        internal async Task<IEnumerable<NoteItem>> GetNotes(bool refresh = false)
        {
            if(Notes.Any() && !refresh) return Notes.Clone();
            var store = new MobileServiceSQLiteStore(Constants.NotesDbName);
            store.DefineTable<NoteItem>();

            if (_notesTable == null)
            {
                await App.MobileService.SyncContext.InitializeAsync(store);

                _notesTable = App.MobileService.GetSyncTable<NoteItem>();

                await SyncWithRemote();
            }

            var notes = await _notesTable.ToListAsync();

		    Notes.Clear();
            Notes.AddRange(notes);

            return Notes.Clone();
        }

        internal async Task<bool> StoreNote(NoteItem noteItem)
        {
            if (noteItem == null) throw new ArgumentNullException(nameof(noteItem));

            NoteItem storedNoteItem = Notes.FirstOrDefault(n => n.Equals(noteItem));

            if (storedNoteItem != null)
            {
                storedNoteItem.Title = noteItem.Title;
                storedNoteItem.Content = noteItem.Content;
                storedNoteItem.LastEdited = DateTime.Now;
                await _notesTable.UpdateAsync(storedNoteItem);
            }
            else
            {
                storedNoteItem = noteItem;
                Notes.Add(storedNoteItem);
                await _notesTable.InsertAsync(storedNoteItem);
            }

            await SyncWithRemote();

            return true;
        }

	    public async Task<bool> Delete(NoteItem noteItem)
	    {
	        if (noteItem == null) throw new ArgumentNullException(nameof(noteItem));
            var itemDeletedFromCache = Notes.Remove(noteItem);

	        if (itemDeletedFromCache)
	        {
	            await _notesTable.DeleteAsync(noteItem);
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
	        catch (Exception e)
	        {
	            Debug.WriteLine(@"Sync error: {0}", e.Message);
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
