using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcard.Models.Context
{
    public interface IFlashcardContext
    {
		DbSet<CardItem> CardItems { get; set; }
	}
}
