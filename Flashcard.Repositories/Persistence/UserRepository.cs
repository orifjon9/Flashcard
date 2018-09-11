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
		private const string Admin_ROLE = "Administrator";
		private const string USER_ROLE = "User";
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

		public async Task Register(User user)
		{
			if (user.UserRoles.Count == 0)
			{
				var role = GetRoleByName(USER_ROLE);
				user.UserRoles.Add(new UserRole
				{
					Role = role,
					RoleId = role.Id,
					User = user,
					UserId = user.Id
				});
			}
			await AddAsync(user);
		}

		private Role GetRoleByName(string roleName)
		{
			return _dbContext.Roles.Where(w => w.Name == roleName).FirstOrDefault();
		}

		private void InitialRecords()
		{
			// roles
			if (_dbContext.Roles.Count() == 0)
			{
				_dbContext.Roles.AddRangeAsync(new Role[] {
					new Role { Name = Admin_ROLE },
					new Role { Name = USER_ROLE }
				});
				_dbContext.SaveChanges();
			}

			if (_dbContext.Users.Count() == 0)
			{
				var adminRole = GetRoleByName(Admin_ROLE);
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
