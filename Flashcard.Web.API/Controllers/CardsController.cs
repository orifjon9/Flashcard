using Flashcard.ViewModels;
using Flashcard.Models.Context;
using Flashcard.Repositories;
using Flashcard.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Flashcard.Web.API.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class CardsController : ControllerBase
	{
		private readonly ICardService _service;

		public CardsController(ICardService service)
		{
			this._service = service;
		}

		// GET api/cards
		[HttpGet]
		public ActionResult<IEnumerable<CardViewModel>> Get()
		{
			return Ok(_service.ListCardsAsync().Result);
		}

		// Get api/cards/{id}
		[HttpGet("{id}", Name = "GetCard")]
		[ProducesResponseType(typeof(CardViewModel), 200)]
		[ProducesResponseType(404)]
		public async Task<ActionResult<CardViewModel>> GetById(int id)
		{
			var card = await _service.GetCardAsync(id);
			if(card == null)
			{
				return NotFound();
			}
			return Ok(card);
		}

		[HttpPost]
		public async Task<ActionResult<CardViewModel>> Create([FromBody]CardViewModel cardToCreate)
		{
			await _service.CreateCardItemAsync(cardToCreate);

			return CreatedAtRoute("GetCard", new { id = cardToCreate.Id }, cardToCreate);
		}

		[HttpPut("{id}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public async Task<ActionResult> Update(int id, [FromBody] CardViewModel cartToUpdate)
		{
			var card = await _service.GetCardAsync(id);
			if (card == null)
			{
				return NotFound();
			}
			
			await _service.UpdateCardItemAsync(id, cartToUpdate);

			return NoContent();
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public async Task<ActionResult> Delete(int id)
		{
			var card = await _service.GetCardAsync(id);
			if (card == null)
			{
				return NotFound();
			}

			await _service.DeleteCardItemAsync(card);

			return NoContent();
		}
	}
}
