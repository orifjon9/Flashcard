using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcard.Models
{
    public class User
    {
		public User()
		{
			UserRoles = new HashSet<UserRole>();
			UserTokens = new HashSet<UserToken>();
		}

		public int Id { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }

		public ICollection<UserRole> UserRoles { get; set; }
		public ICollection<UserToken> UserTokens { get; set; }
	}
}
