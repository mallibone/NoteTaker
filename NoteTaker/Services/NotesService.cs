using System;
using System.Collections.Generic;
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
                new Note {Title = "Plan TechDays Talk 2017", Content = "Present how Visual Studio Mobile Center will enable mobile solutions to adopt DevOps without any setup hassle.", Created = DateTime.Now.AddMonths(-1), LastEdited = DateTime.Now.AddMonths(-1)},
                new Note {Title = "Valentines Day Plans", Content = "Don't forget to ask your wife to be your valentine", Created = DateTime.Now.AddDays(-7), LastEdited = DateTime.Now.AddDays(-7)},
                new Note {Title = "Answers for VSMC Q&A", Content = "- It depends\n- 42", Created = DateTime.Now.AddDays(-1), LastEdited = DateTime.Now.AddDays(-1)},
                new Note {Title = "Mention Feedback at end of talk", Content = "Dont forget to mention there will be a cool give away for the feedback", Created = DateTime.Now.AddHours(-1), LastEdited = DateTime.Now.AddHours(-1)},
            };
		}

        internal IEnumerable<Note> GetNotes()
        {
            return Notes.Clone();
        }
    }
}
