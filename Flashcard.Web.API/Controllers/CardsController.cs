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
		[HttpGet("{id}")]
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

		private void LoadDefaultItems()
		{
			if(_context.CardItems.Count() == 0)
			{
				_context.CardItems.AddRangeAsync(
				new Models.CardItem() { Name = "AA", Description = "AA AA" },
				new Models.CardItem() { Name = "BB", Description = "BB BB" });
				// save
				_context.SaveChangesAsync();
			}
		}
	}
}
