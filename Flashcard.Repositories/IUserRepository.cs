using Flashcard.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Flashcard.Repositories
{
    public interface IUserRepository: IRepository<User>
    {
		User Find(string email, string password);
    }
}
