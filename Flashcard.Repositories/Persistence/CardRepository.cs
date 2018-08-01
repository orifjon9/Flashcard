using Flashcard.Models;
using Flashcard.Models.Context;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Flashcard.Repositories.Persistence
{
    public class CardRepository : BaseRepository<Card>, ICardRepository
	{
		public CardRepository(FlashcardContext context):base(context)
		{
			LoadDefaultItems();
		}

		private void LoadDefaultItems()
		{
			if (_dbContext.Cards.Count() == 0)
			{
				_dbContext.Cards.AddRangeAsync(
				new Card() { Name = "AA", Description = "AA AA" },
				new Card() { Name = "BB", Description = "BB BB" });
				// save
				_dbContext.SaveChangesAsync();
			}
		}
	}
}
