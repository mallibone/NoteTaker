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

            ToolbarItems.Add(new ToolbarItem("add", "add", () =>
            {
                Vm.NewNote();
            }));
		}

		public NoteTakerViewModel Vm => App.Locator.NoteTakerViewModel;

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Vm.Init();
        }

		private void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem == null) return;

		    var selectedNote = ((NoteViewItem) e.SelectedItem);
            Vm.NoteSelected(selectedNote);

			NoteListView.SelectedItem = null;
		}
	}
}
