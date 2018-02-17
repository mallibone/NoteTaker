using System;
using NoteTaker.Models;
using NoteTaker.ViewModels;
using Xamarin.Forms;

namespace NoteTaker.Views
{
    public partial class EditNoteView : ContentPage
    {
        public EditNoteView(string noteItemId)
        {
            InitializeComponent();
            Vm.Init(noteItemId);
            BindingContext = Vm;
            NavigationPage.SetBackButtonTitle(this, "Cancel");
        }

        private EditNoteViewModel Vm => App.Locator.EditNoteViewModel;
    }
}
