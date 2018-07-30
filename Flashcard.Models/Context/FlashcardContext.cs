using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcard.Models.Context
{
	public class FlashcardContext : DbContext, IFlashcardContext
	{
		public FlashcardContext(DbContextOptions<FlashcardContext> options) : base(options)
		{
		}

		public DbSet<CardItem> CardItems { get; set; }
	}
}
