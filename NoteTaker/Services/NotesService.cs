using System;
using System.Collections.Generic;
using System.Linq;
using NoteTaker.Models;
using NoteTaker.Utils;

namespace NoteTaker.Services
{
	public class NotesService
	{
        private readonly List<Note> Notes;

		public NotesService()
		{
            Notes = new List<Note>
            {
                new Note {Title = "Global Azure Bootcamp", Content = "Show how quickly remote storage, authentication and push can be added to an app.\n\n=>From Zero to Hero", Created = DateTime.Now.AddMonths(-1), LastEdited = DateTime.Now.AddMonths(-1)},
                new Note {Title = "Plan Date Night", Content = "Don't forget to ask your wife to be your valentine on the 14. February", Created = DateTime.Now.AddDays(-7), LastEdited = DateTime.Now.AddDays(-7)},
                new Note {Title = "Answers for Azure Mobile App Service Q&A", Content = "- It depends\n- 42", Created = DateTime.Now.AddDays(-1), LastEdited = DateTime.Now.AddDays(-1)},
                new Note {Title = "Spacey Wacey Stuff", Content = "Dr. Who things and what not.", Created = DateTime.Now.AddHours(-1), LastEdited = DateTime.Now.AddHours(-1)},
            };
		}

        internal IEnumerable<Note> GetNotes()
        {
            return Notes.Clone();
        }

        internal bool StoreNote(Note note)
        {
            if (note == null) throw new ArgumentNullException(nameof(note));


            Note storedNote = Notes.FirstOrDefault(n => n.Equals(note));


            if (storedNote != null)
            {
                storedNote.Title = note.Title;
                storedNote.Content = note.Content;
                storedNote.LastEdited = DateTime.Now;
            }
            else
            {
                storedNote = note;
                Notes.Add(storedNote);
            }

            return true;
        }

	    public bool Delete(Note note)
	    {
	        return Notes.Remove(note);
	    }
	}
}
