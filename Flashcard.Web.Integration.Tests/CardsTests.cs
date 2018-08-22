using Flashcard.Web.Integration.Tests.Fixtures;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Flashcard.Web.Integration.Tests
{
    public class CardsTests
    {
		private readonly TestContext _context;

		public CardsTests()
		{
			_context = new TestContext();
		}

		[Fact]
		public async Task CardsReturnsOkResponse()
		{
			var response = await _context.Client.GetAsync("/api/cards");

			response.EnsureSuccessStatusCode();

			response.StatusCode.Should().Be(HttpStatusCode.OK);
		}
	}
}
