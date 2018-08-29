using Flashcard.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Flashcard.Services.Interfaces
{
	public interface ICardService
	{
		Task<IEnumerable<CardViewModel>> ListCardsAsync();
		Task<CardViewModel> GetCardAsync(int id);

		Task<bool> CreateCardItemAsync(CardViewModel cartToCreate);
		Task<bool> UpdateCardItemAsync(int id, CardViewModel cardToUpdate);
		Task<bool> DeleteCardItemAsync(CardViewModel cardToDelete);
	}
}
