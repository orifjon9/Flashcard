using AutoMapper;
using Flashcard.Models;
using Flashcard.ViewModels;
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
		private readonly IMapper _mapper;
		public CardService(ICardRepository cardRepository, IMapper mapper)
		{
			_cardRepository = cardRepository;
			_mapper = mapper;
		}

		public async Task<IEnumerable<CardViewModel>> ListCardsAsync() 
				=> _mapper.Map<IEnumerable<Card>, IEnumerable<CardViewModel>>(await _cardRepository.GetAllAsync());
	

		public async Task<CardViewModel> GetCardAsync(int id) =>  _mapper.Map<Card, CardViewModel>(await _cardRepository.GetAsync(id));

		public async Task<bool> UpdateCardItemAsync(int id, CardViewModel cardToUpdate)
		{
			var card = _mapper.Map<CardViewModel, Card>(cardToUpdate);
			card.Id = id;

			_cardRepository.Update(card);
			await _cardRepository.CommitAsync();
			return true;
		}

		public async Task<bool> CreateCardItemAsync(CardViewModel cardToCreate)
		{
			var card = _mapper.Map<CardViewModel, Card>(cardToCreate);
			await _cardRepository.AddAsync(card);
			await _cardRepository.CommitAsync();
			cardToCreate.Id = card.Id;

			return true;
		}

		public async Task<bool> DeleteCardItemAsync(CardViewModel phoneToDelete)
		{
			var card = _mapper.Map<CardViewModel, Card>(phoneToDelete);
			_cardRepository.Delete(card);
			await _cardRepository.CommitAsync();
			return true;
		}
	}
}
