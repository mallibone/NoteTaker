using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace NoteTaker
{
	public class NotesService
	{
        private readonly List<Note> Notes;

		public NotesService()
		{
            Notes = new List<Note>
            {
                new Note {Id = 1, Title = "Plan TechDays Talk 2017", Content = "Present how Visual Studio Mobile Center will enable mobile solutions to adopt DevOps without any setup hassle.", Created = DateTime.Now.AddMonths(-1), LastEdited = DateTime.Now.AddMonths(-1)},
                new Note {Id = 2, Title = "Valentines Day Plans", Content = "Don't forget to ask your wife to be your valentine on the 14. February", Created = DateTime.Now.AddDays(-7), LastEdited = DateTime.Now.AddDays(-7)},
                new Note {Id = 3, Title = "Answers for VSMC Q&A", Content = "- It depends\n- 42", Created = DateTime.Now.AddDays(-1), LastEdited = DateTime.Now.AddDays(-1)},
                new Note {Id = 4, Title = "Mention Feedback at end of talk", Content = "Dont forget to mention there will be a cool give away for the feedback", Created = DateTime.Now.AddHours(-1), LastEdited = DateTime.Now.AddHours(-1)},
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
    }
}
