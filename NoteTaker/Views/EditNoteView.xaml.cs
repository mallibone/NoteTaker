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
        }
    }
}
