using Flashcard.Web.API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Flashcard.Web.Integration.Tests.Fixtures
{
    public class TestContext
    {
		private TestServer _server;
		public HttpClient Client { get; private set; }

		public TestContext()
		{
			SetUpClient();
		}

		private void SetUpClient()
		{
			_server = new TestServer(new WebHostBuilder()
				.UseStartup<Startup>());

			Client = _server.CreateClient();
		}
	}
}
