using System;
using System.Linq;
using Xamarin.UITest;

namespace NoteTaker.UITests
{
    public class EditNotePage
    {
        private IApp _app;

        public EditNotePage(IApp app)
        {
            _app = app;
        }

        public bool IsVisible()
        {
            try
            {
                return _app.WaitForElement(c => c.Marked("Edit")).Any();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return false;
            }
        }

        public void SetTitle(string title)
        {
            _app.EnterText("NoteTitle", title);
        }

        public void SetContent(string content)
        {
            _app.EnterText("NoteContent", content);
        }

        public void SaveNote()
        {
            _app.Tap("Store");
        }

        public void DeleteNote()
        {
            _app.Tap("Delete");
        }
    }
}