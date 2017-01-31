using System.Linq;
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

		public Tests(Platform platform)
		{
			_platform = platform;
		}

		[SetUp]
		public void BeforeEachTest()
		{
			_app = AppInitializer.StartApp(_platform);
		}

		[Test]
		public void AppStartsUp()
		{
			AppResult[] results = _app.WaitForElement(c => c.Marked("Notes"));
			_app.Screenshot("Note List");

			Assert.IsTrue(results.Any());
		}
	}
}
