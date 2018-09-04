using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcard.Models
{
    public class Role
    {
		public Role()
		{
			UserRoles = new HashSet<User>();
		}

		public int Id { get; set; }
		public string Name { get; set; }

		public ICollection<User> UserRoles { get; set; }
	}
}
