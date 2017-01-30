using System;
using System.IO;
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
		IApp app;
		Platform platform;

		public Tests(Platform platform)
		{
			this.platform = platform;
		}

		[SetUp]
		public void BeforeEachTest()
		{
			app = AppInitializer.StartApp(platform);
		}

		[Test]
		public void AppStartsUp()
		{
			AppResult[] results = app.WaitForElement(c => c.Marked("Notes"));
			app.Screenshot("Note List");

			Assert.IsTrue(results.Any());
		}
	}
}
