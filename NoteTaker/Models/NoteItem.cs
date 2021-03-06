﻿using System;

namespace NoteTaker.Models
{
	public class NoteItem
	{
	    public const int NewNoteId = 0;

	    public NoteItem()
        {
            Created = DateTime.Now;
            LastEdited = DateTime.Now;
        }

		public string Id { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public DateTime Created { get; set; }
		public DateTime LastEdited { get; set; }
        public string LastEditedString => LastEdited.ToString("D");

	    public override bool Equals(object obj)
	    {
	        if (ReferenceEquals(null, obj)) return false;
	        if (ReferenceEquals(this, obj)) return true;
	        if (obj.GetType() != this.GetType()) return false;
	        return Equals((NoteItem) obj);
	    }

	    public override int GetHashCode()
	    {
	        unchecked
	        {
	            var hashCode = (Title != null ? Title.GetHashCode() : 0);
	            hashCode = (hashCode * 397) ^ (Content != null ? Content.GetHashCode() : 0);
	            hashCode = (hashCode * 397) ^ Created.GetHashCode();
	            hashCode = (hashCode * 397) ^ LastEdited.GetHashCode();
	            return hashCode;
	        }
	    }

	    protected bool Equals(NoteItem other)
	    {
	        return Created.Equals(other.Created) && Id.Equals(other.Id);
	    }
    }
}
