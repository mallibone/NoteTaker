using System;
namespace NoteTaker
{
	public class Note
	{
		public string Title { get; set; }
		public string Content { get; set; }
		public DateTime Created { get; set; }
		public DateTime LastEdited { get; set; }
	}
}
