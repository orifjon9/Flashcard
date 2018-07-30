using Flashcard.Models;
using Flashcard.Models.Context;
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
		private readonly FlashcardContext _context;

		public CardsController(FlashcardContext context)
		{
			this._context = context;
			this.LoadDefaultItems();
		}

		// GET api/cards
		[HttpGet]
		public ActionResult Get()
		{
			return Ok(_context.CardItems.ToList());
		}

		// Get api/cards/{id}
		[HttpGet("{id}", Name = "GetCard")]
		[ProducesResponseType(typeof(CardItem), 200)]
		[ProducesResponseType(404)]
		public async Task<ActionResult<CardItem>> GetById(int id)
		{
			var card = await _context.CardItems.FindAsync(id);
			if(card == null)
			{
				return NotFound();
			}
			return Ok(card);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody]CardItem item)
		{
			await _context.CardItems.AddAsync(item);
			await _context.SaveChangesAsync();

			return CreatedAtRoute("GetCard", new { id = item.Id }, item);
		}

		[HttpPut("{id}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public async Task<ActionResult> Update(int id, [FromBody] CardItem item)
		{
			var card = await _context.CardItems.FindAsync(id);
			if (card == null)
			{
				return NotFound();
			}

			card.Name = item.Name;
			card.Description = item.Description;
			await _context.SaveChangesAsync();

			return NoContent();
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public async Task<ActionResult> Delete(int id)
		{
			var card = await _context.CardItems.FindAsync(id);
			if (card == null)
			{
				return NotFound();
			}

			_context.CardItems.Remove(card);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private void LoadDefaultItems()
		{
			if(_context.CardItems.Count() == 0)
			{
				_context.CardItems.AddRangeAsync(
				new CardItem() { Name = "AA", Description = "AA AA" },
				new CardItem() { Name = "BB", Description = "BB BB" });
				// save
				_context.SaveChangesAsync();
			}
		}
	}
}
