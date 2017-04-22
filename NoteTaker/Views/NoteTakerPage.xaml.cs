using NoteTaker.Models;
using NoteTaker.ViewModels;
using Xamarin.Forms;

namespace NoteTaker.Views
{
	public partial class NoteTakerPage : ContentPage
	{
		public NoteTakerPage()
		{
			InitializeComponent();

            BindingContext = Vm;

            ToolbarItems.Add(new ToolbarItem("Add", "add", () =>
            {
                Vm.NewNote();
            }));
		}

		public NoteTakerViewModel Vm => App.Locator.NoteTakerViewModel;

		void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem == null) return;

		    var noteId = ((NoteViewItem) e.SelectedItem).Id;
			Vm.NoteSelected(noteId);

			NoteListView.SelectedItem = null;
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Vm.Init();
        }
	}
}
