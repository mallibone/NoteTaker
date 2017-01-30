using System;
using GalaSoft.MvvmLight;

namespace NoteTaker
{
	public class NoteTakerViewModel : ViewModelBase
	{
		public NoteTakerViewModel()
		{
		}

		internal void NoteSelected(Note selectedItem)
		{
			if (selectedItem == null) throw new ArgumentNullException(nameof(selectedItem));
			throw new NotImplementedException();
		}

		internal void NewNote()
		{
			throw new NotImplementedException();
		}
	}
}
