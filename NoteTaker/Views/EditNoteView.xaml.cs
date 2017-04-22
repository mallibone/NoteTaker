using System;
using NoteTaker.Models;
using NoteTaker.ViewModels;
using Xamarin.Forms;

namespace NoteTaker.Views
{
    public partial class EditNoteView : ContentPage
    {
        public EditNoteView(int id)
        {
            InitializeComponent();
            BindingContext = Vm;
            NavigationPage.SetBackButtonTitle(this, "Cancel");
            Vm.Init(id);
        }

        private EditNoteViewModel Vm => App.Locator.EditNoteViewModel;
    }
}
