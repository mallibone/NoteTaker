using NoteTaker.UITests.Pages;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

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

		[Test]
		public void AppStartsUp()
		{
            Assert.IsTrue(_mainPage.IsVisible());
			_app.Screenshot("Note Overview");
            _mainPage.AddNote();
            Assert.IsTrue(_editNotePage.IsVisible());
            _app.Screenshot("Note Detail");
		}

        [Test]
        public void AddAndRemoveNote()
        {
            _mainPage.IsVisible();
            _app.Screenshot("Note Overview");
            _mainPage.AddNote();
            _editNotePage.IsVisible();
			_app.Screenshot("Note Detail");
            _editNotePage.SetTitle("Test Note");
			_editNotePage.SetContent("Hello Visual Studio Mobile Center");
			_app.DismissKeyboard();
			_app.Screenshot("Note All set");
			_app.Tap("Store");
			_mainPage.IsVisible();
			_app.Screenshot("New Note in List");
            Assert.True(_mainPage.HasNote("Test Note"));
            _mainPage.SelectNote("Test Note");
            _editNotePage.IsVisible();
            _editNotePage.DeleteNote();
            _mainPage.IsVisible();
			Assert.False(_mainPage.HasNote("Test Note"));
        }
	}
}
