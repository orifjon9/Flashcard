using Flashcard.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Flashcard.Services.Interfaces
{
	public interface ICardService
	{
		Task<IEnumerable<Card>> ListCards();
		Task<Card> GetCardAsync(int id);

		Task<bool> CreateCardItemAsync(Card phoneToCard);
		Task<bool> UpdateCardItem(int id, Card phoneToCard);
		Task<bool> DeleteCardItem(Card phoneToCard);
	}
}
