using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcard.Models
{
    public class User
    {
		public User()
		{
			UserRoles = new HashSet<Role>();
			UserTokens = new HashSet<UserToken>();
		}

		public int Id { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }

		public ICollection<Role> UserRoles { get; set; }
		public ICollection<UserToken> UserTokens { get; set; }
	}
}
