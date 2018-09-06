using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcard.Models.Context
{
    public interface IFlashcardContext
    {
		DbSet<Card> Cards { get; set; }
		DbSet<User> Users { get; set; }
		DbSet<Role> Roles { get; set; }
		DbSet<UserRole> UserRoles { get; set; }
		DbSet<UserToken> UserTokens { get; set; }
	}
}
