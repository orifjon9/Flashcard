using Flashcard.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Flashcard.Models.Context;
using System.Threading.Tasks;

namespace Flashcard.Repositories.Persistence
{
	public class UserRepository : BaseRepository<User>, IUserRepository
	{
		public UserRepository(FlashcardContext context) : base(context)
		{
			InitialRecords();
		}

		public User Find(string email, string password)
		{
			return _dbContext.Users
					.Where(w => w.Email == email && w.Password == password)
					.FirstOrDefault();
		}

		private void InitialRecords()
		{
			// roles
			if (_dbContext.Roles.Count() == 0)
			{
				_dbContext.Roles.AddRangeAsync(new Role[] {
					new Role { Name = "Administrator" },
					new Role { Name = "User" }
				});
				_dbContext.SaveChanges();
			}

			if (_dbContext.Users.Count() == 0)
			{
				var adminRole = _dbContext.Roles.Where(w => w.Name == "Administrator").FirstOrDefault();
				var user = new User()
				{
					Email = "info@orifjon.net",
					FirstName = "Orifjon",
					LastName = "Narkulov",
					Birthday = new DateTime(1987, 1, 9),
					Password = "admin"
				};

				var userRole = new UserRole()
				{
					Role = adminRole,
					RoleId = adminRole.Id,
					User = user,
					UserId = user.Id
				};
				user.UserRoles.Add(userRole);
				_dbContext.Users.AddAsync(user);
				Commit();
			}
		}
	}
}
