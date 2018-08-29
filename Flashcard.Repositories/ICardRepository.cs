using Flashcard.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Flashcard.Repositories
{
    public interface ICardRepository: IRepository<Card>
    {
		Task<Card> GetAsync(int id);

	}
}
