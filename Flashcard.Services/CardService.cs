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

		public async Task<IEnumerable<Card>> ListCards()
		{
			return await Task.Run(() => _cardRepository.GetAll());
		}

		public async Task<Card> GetCardAsync(int id)
		{
			return await _cardRepository.GetAsync(id);
		}

		public async Task<bool> UpdateCardItem(int id, Card phoneToCard)
		{
			_cardRepository.Update(phoneToCard);
			await _cardRepository.CommitAsync();
			return true;
		}

		public async Task<bool> CreateCardItemAsync(Card phoneToCard)
		{
			await _cardRepository.AddAsync(phoneToCard);
			await _cardRepository.CommitAsync();
			return true;
		}

		public async Task<bool> DeleteCardItem(Card phoneToCard)
		{
			_cardRepository.Delete(phoneToCard);
			await _cardRepository.CommitAsync();
			return true;
		}
	}
}
