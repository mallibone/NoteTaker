﻿using System;

namespace NoteTaker.Models
{
	public class Note
	{
	    public const int NewNoteId = 0;

	    public Note()
        {
            Created = DateTime.Now;
            LastEdited = DateTime.Now;
        }

        public int Id { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public DateTime Created { get; set; }
		public DateTime LastEdited { get; set; }
        public string LastEditedString => LastEdited.ToString("D");
    }
}
