using Flashcard.Models;
using Flashcard.Repositories;
using Flashcard.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Flashcard.Services
{
	public class CardService : ICardService
	{
		private readonly ICardRepository _cardRepository;
		public CardService(ICardRepository cardRepository)
		{
			_cardRepository = cardRepository;
		}

		public async Task<IEnumerable<Card>> ListCardsAsync()
		{
			return await Task.Run(() => _cardRepository.GetAll());
		}

		public async Task<Card> GetCardAsync(int id)
		{
			return await _cardRepository.GetAsync(id);
		}

		public async Task<bool> UpdateCardItemAsync(int id, Card phoneToCard)
		{
			var card = await _cardRepository.GetAsync(id);

			card.Name = phoneToCard.Name;
			card.Description = phoneToCard.Description;

			_cardRepository.Update(card);
			await _cardRepository.CommitAsync();
			return true;
		}

		public async Task<bool> CreateCardItemAsync(Card phoneToCard)
		{
			await _cardRepository.AddAsync(phoneToCard);
			await _cardRepository.CommitAsync();
			return true;
		}

		public async Task<bool> DeleteCardItemAsync(Card phoneToCard)
		{
			_cardRepository.Delete(phoneToCard);
			await _cardRepository.CommitAsync();
			return true;
		}
	}
}
