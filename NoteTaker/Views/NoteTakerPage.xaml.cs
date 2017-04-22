using System.Threading.Tasks;
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

		    var selectedNote = ((NoteViewItem) e.SelectedItem);
		    //Navigation.PushAsync(new EditNoteView(selectedNote.Id));
            Vm.NoteSelected(selectedNote);

			NoteListView.SelectedItem = null;
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Vm.Init();
        }
	}
}
