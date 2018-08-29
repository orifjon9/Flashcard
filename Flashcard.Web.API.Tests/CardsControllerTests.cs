using Flashcard.Services;
using Flashcard.Services.Interfaces;
using Moq;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Flashcard.Web.API.Controllers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Flashcard.ViewModels;

namespace Flashcard.Web.UnitTest.Tests
{
	public class CardsControllerTests
	{
		private readonly Mock<ICardService> _mockCardService;
		private const int NotFoundCode = 404;
		private const int NoContentCode = 204;

		public CardsControllerTests()
		{
			_mockCardService = new Mock<ICardService>();
		}

		[Fact]
		public void Get_ReturnsAOkObjectResult_WithAListOfCards()
		{
			var lists = GetSingleCardItems();

			_mockCardService
					.Setup(x => x.ListCardsAsync())
					.Returns(Task.FromResult(lists.AsEnumerable()));
			var cardController = new CardsController(_mockCardService.Object);

			// Act
			var result = cardController.Get();

			// Assert
			var actionResult = Assert.IsType<OkObjectResult>(result.Result);
			var model = Assert.IsAssignableFrom<IEnumerable<CardViewModel>>(actionResult.Value);

			// Validation result's count
			Assert.Equal(model.Count(), lists.Count());
		}

		[Fact]
		public void Create_ReturnsAOkObjectResult_WithACardItem()
		{
			var card = GetSingleCardItem1();
			_mockCardService.Setup(x => x.CreateCardItemAsync(It.IsAny<CardViewModel>()))
				.Returns(Task.FromResult(true));

			var cardController = new CardsController(_mockCardService.Object);

			// Act
			var result = cardController.Create(card).Result;

			// Assert
			var actionResult = Assert.IsType<CreatedAtRouteResult>(result.Result);
			var model = Assert.IsAssignableFrom<CardViewModel>(actionResult.Value);

			// Validation Ids
			Assert.True(model.Id  == card.Id);
		}

		[Fact]
		public void GetById_ReturnsAOkObjectResult_WithACardItem()
		{
			var card = GetSingleCardItem2();
			_mockCardService.Setup(x => x.GetCardAsync(It.IsAny<int>()))
				.Returns(Task.FromResult(card));

			var cardsController = new CardsController(_mockCardService.Object);

			// Act
			var result = cardsController.GetById(card.Id).Result;

			// Assert
			var actionResult = Assert.IsType<OkObjectResult>(result.Result);
			var model = Assert.IsAssignableFrom<CardViewModel>(actionResult.Value);

			// Validate values
			Assert.Equal(model, card);
		}

		[Fact]
		public void GetById_ReturnsANotFoundResult_WithEmptyValue()
		{
			_mockCardService.Setup(x => x.GetCardAsync(It.IsAny<int>()))
				.Returns(Task.FromResult((CardViewModel)null));

			var controller = new CardsController(_mockCardService.Object);

			// Act
			var result = controller.GetById(1).Result;
			// Assert
			var actionResult = Assert.IsType<NotFoundResult>(result.Result);
			
			// Validation HttpCode
			Assert.Equal(actionResult.StatusCode, NotFoundCode);
		}

		[Fact]
		public void Update_ReturnsANoContent_WithAEmptyValue()
		{
			var card = GetSingleCardItem2();
			_mockCardService.Setup(x => x.GetCardAsync(It.IsAny<int>()))
				.Returns(Task.FromResult(card));

			_mockCardService.Setup(x => x.UpdateCardItemAsync(It.IsAny<int>(), It.IsAny<CardViewModel>()))
				.Returns(Task.FromResult(true));

			var controller = new CardsController(_mockCardService.Object);

			// Act
			var result = controller.Update(card.Id, card).Result;

			// Assert
			var actionResult = Assert.IsType<NoContentResult>(result);

			// Validate codes
			Assert.Equal(actionResult.StatusCode, NoContentCode);
		}

		[Fact]
		public void Update_ReturnsANotFound_WithAEmptyValue()
		{
			var card = GetSingleCardItem2();
			_mockCardService.Setup(x => x.GetCardAsync(It.IsAny<int>()))
				.Returns(Task.FromResult((CardViewModel)null));

			var controller = new CardsController(_mockCardService.Object);

			// Act
			var result = controller.Update(card.Id, card).Result;

			// Assert
			var actionResult = Assert.IsType<NotFoundResult>(result);

			// Validate codes
			Assert.Equal(actionResult.StatusCode, NotFoundCode);
		}

		[Fact]
		public void Delete_ReturnsANoContent_WithAEmptyValue()
		{
			var card = GetSingleCardItem1();
			_mockCardService.Setup(x => x.GetCardAsync(It.IsAny<int>()))
				.Returns(Task.FromResult(card));

			_mockCardService.Setup(x => x.DeleteCardItemAsync(It.IsAny<CardViewModel>()))
				.Returns(Task.FromResult(true));

			var controller = new CardsController(_mockCardService.Object);

			// Act
			var result = controller.Delete(card.Id).Result;

			// Assert
			var actionResult = Assert.IsType<NoContentResult>(result);

			// Validate codes
			Assert.Equal(actionResult.StatusCode, NoContentCode);
		}

		[Fact]
		public void Delete_ReturnsANotFound_WithAEmptyValue()
		{
			var card = GetSingleCardItem1();
			_mockCardService.Setup(x => x.GetCardAsync(It.IsAny<int>()))
				.Returns(Task.FromResult((CardViewModel)null));

			var controller = new CardsController(_mockCardService.Object);

			// Act
			var result = controller.Delete(card.Id).Result;

			// Assert
			var actionResult = Assert.IsType<NotFoundResult>(result);

			// Validate codes
			Assert.Equal(actionResult.StatusCode, NotFoundCode);
		}

		private IEnumerable<CardViewModel> GetSingleCardItems()
		{
			return (new List<CardViewModel>() {
					GetSingleCardItem1(),
					GetSingleCardItem2()})
				.AsEnumerable();
		}

		private CardViewModel GetSingleCardItem1()
		{
			return new CardViewModel() { Id = 1, Name = "Test Card", Description = "Description Card" };
		}

		private CardViewModel GetSingleCardItem2()
		{
			return new CardViewModel() { Id = 2, Name = "Test Card2", Description = "Description Card" };
		}

	}
}
