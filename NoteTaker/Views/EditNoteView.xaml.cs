using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace NoteTaker
{
    public partial class EditNoteView : ContentPage
    {
        public EditNoteView()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "Cancel");
            Vm = App.Locator.EditNoteViewModel;
            Vm.Init();
            BindingContext = Vm;
        }

        public EditNoteView(Note note):this()
        {
            if (note == null) throw new ArgumentNullException(nameof(note));
            Vm.Init(note);
        }

        public EditNoteViewModel Vm { get; private set; }
    }
}
