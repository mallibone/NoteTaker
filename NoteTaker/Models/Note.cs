using System;
namespace NoteTaker
{
	public class Note
	{
        public Note()
        {
            Created = DateTime.Now;
            LastEdited = DateTime.Now;
        }

		public string Title { get; set; }
		public string Content { get; set; }
		public DateTime Created { get; set; }
		public DateTime LastEdited { get; set; }
        public string LastEditedString => LastEdited.ToString("D");
	}
}
