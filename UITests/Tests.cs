using NoteTaker.UITests.Pages;
using NUnit.Framework;
using Xamarin.UITest;

namespace NoteTaker.UITests
{
	[TestFixture(Platform.Android)]
	[TestFixture(Platform.iOS)]
	public class Tests
	{
		private IApp _app;
	    private readonly Platform _platform;
        private MainPage _mainPage;
        private EditNotePage _editNotePage;

        public Tests(Platform platform)
		{
			_platform = platform;
		}

		[SetUp]
		public void BeforeEachTest()
		{
			_app = AppInitializer.StartApp(_platform);
            _mainPage = new MainPage(_app);
            _editNotePage = new EditNotePage(_app);
		}

        //[Test]
        //[Ignore]
        //public void TestCreator()
        //{
        //    _app.Repl();
        //}

		//[Test]
  //      [Ignore]
		//public void AppStartsUp()
		//{
  //          Assert.IsTrue(_mainPage.IsVisible());
		//	_app.Screenshot("Note Overview");
  //          _mainPage.AddNote();
  //          Assert.IsTrue(_editNotePage.IsVisible());
  //          _app.Screenshot("Note Detail");
		//}

        [Test]
        public void AddNote()
        {
            _mainPage.IsVisible();
            _app.Screenshot("Note Overview");
            _mainPage.AddNote();
            _editNotePage.IsVisible();
			_app.Screenshot("Note Detail");
            _editNotePage.SetTitle("Test Note");
			_editNotePage.SetContent("Hello Visual Studio App Center");
			_app.DismissKeyboard();
			_app.Screenshot("Note All set");
			_app.Tap("Store");
			_mainPage.IsVisible();
			_app.Screenshot("New Note in List");
            Assert.True(_mainPage.HasNote("Test Note"));
        }

	    [Test]
	    public void EditNote()
	    {
            _mainPage.IsVisible();
            _app.Screenshot("Note Overview");
            _mainPage.SelectNote("Plan Date");
            _editNotePage.IsVisible();
			_app.Screenshot("Note Detail");
            _editNotePage.SetTitle(" Night");
			_app.DismissKeyboard();
			_app.Screenshot("Note edited");
			_app.Tap("Store");
			_mainPage.IsVisible();
			_app.Screenshot("Edited Note in List");
            Assert.True(_mainPage.HasNote("Plan Date Night"));
	    }

	    [Test]
	    public void RemoveNote()
	    {
            _mainPage.SelectNote("Plan Date");
            _editNotePage.IsVisible();
            _editNotePage.DeleteNote();
			_app.Screenshot("Open Note");
            _mainPage.IsVisible();
			Assert.False(_mainPage.HasNote("Plan Date"));
			_app.Screenshot("Note removed");
	    }
	}
}
