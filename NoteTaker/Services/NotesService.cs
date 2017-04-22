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
                new Note {Id = 1, Title = "Global Azure Bootcamp", Content = "Show how quickly remote storage, authentication and push can be added to an app.\n\n=>From Zero to Hero", Created = DateTime.Now.AddMonths(-1), LastEdited = DateTime.Now.AddMonths(-1)},
                new Note {Id = 2, Title = "Plan Date Night", Content = "Don't forget to ask your wife to be your valentine on the 14. February", Created = DateTime.Now.AddDays(-7), LastEdited = DateTime.Now.AddDays(-7)},
                new Note {Id = 3, Title = "Answers for Azure Mobile App Service Q&A", Content = "- It depends\n- 42", Created = DateTime.Now.AddDays(-1), LastEdited = DateTime.Now.AddDays(-1)},
                new Note {Id = 4, Title = "Spacey Wacey Stuff", Content = "Dr. Who things and what not.", Created = DateTime.Now.AddHours(-1), LastEdited = DateTime.Now.AddHours(-1)},
            };
		}

        internal IEnumerable<Note> GetNotes()
        {
            return Notes.Clone();
        }

        internal bool StoreNote(Note note)
        {
            if (note == null) throw new ArgumentNullException(nameof(note));

            Note storedNote;

            if (note.Id > 0)
            {
                storedNote = Notes.FirstOrDefault(n => n.Id == note.Id);
                if (storedNote == null) return false;

                storedNote.Title = note.Title;
                storedNote.Content = note.Content;
                storedNote.LastEdited = DateTime.Now;
            }
            else
            {
                storedNote = note;
                storedNote.Id = (Notes.Last().Id + 1); // todo: replace with Snowflake ID...
                Notes.Add(storedNote);
            }

            return true;
        }

	    public Note GetNote(int id)
	    {
            if(Notes.All(n => n.Id != id)) throw new ArgumentException();

	        return Notes.First(n => n.Id == id).Clone();
	    }
	}
}
