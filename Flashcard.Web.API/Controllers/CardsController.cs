using Flashcard.Models;
using Flashcard.Models.Context;
using Flashcard.Repositories;
using Flashcard.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flashcard.Web.API.Controllers
{
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
		public ActionResult<IEnumerable<Card>> Get()
		{
			return Ok(_service.ListCardsAsync().Result);
		}

		// Get api/cards/{id}
		[HttpGet("{id}", Name = "GetCard")]
		[ProducesResponseType(typeof(Card), 200)]
		[ProducesResponseType(404)]
		public async Task<ActionResult<Card>> GetById(int id)
		{
			var card = await _service.GetCardAsync(id);
			if(card == null)
			{
				return NotFound();
			}
			return Ok(card);
		}

		[HttpPost]
		public async Task<ActionResult<Card>> Create([FromBody]Card item)
		{
			await _service.CreateCardItemAsync(item);

			return CreatedAtRoute("GetCard", new { id = item.Id }, item);
		}

		[HttpPut("{id}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public async Task<ActionResult> Update(int id, [FromBody] Card item)
		{
			var card = await _service.GetCardAsync(id);
			if (card == null)
			{
				return NotFound();
			}
			
			await _service.UpdateCardItemAsync(id, card);

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
