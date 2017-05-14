using System;
using System.Linq;
using Xamarin.UITest;

namespace NoteTaker.UITests.Pages
{
    public class MainPage
    {
        private IApp _app;

        public MainPage(IApp app)
        {
            _app = app;
        }

        public bool IsVisible()
        {
            try
            {
                return _app.WaitForElement(c => c.Marked("MainPage")).Any();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return false;
            }
        }

        public void AddNote()
        {
            _app.Tap("add");
        }

        internal bool HasNote(string noteTitle)
        {
            return HasElement(noteTitle);
        }

        internal void SelectNote(string noteName)
        {
            _app.Tap(noteName);
        }

        private bool HasElement(string element)
        {
            return _app.Query(element).Any();
        }
    }
}