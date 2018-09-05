using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcard.Models.Context
{
	public class FlashcardContext : DbContext, IFlashcardContext
	{
		public FlashcardContext(DbContextOptions<FlashcardContext> options) : base(options)
		{
		}

		public DbSet<Card> Cards { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<UserRole> UserRoles { get; set; }
		public DbSet<UserToken> UserTokens { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// build user mapping
			modelBuilder.Entity<User>(entity =>
			{
				entity.Property(e => e.Email).HasMaxLength(128).IsRequired();
				entity.HasIndex(e => e.Email).IsUnique();
				entity.Property(e => e.Password).IsRequired();
			});

			// build role mapping
			modelBuilder.Entity<Role>(entity =>
			{
				entity.Property(e => e.Name).HasMaxLength(64).IsRequired();
				entity.HasIndex(e => e.Name).IsUnique();
			});

			// build userrole mapping
			modelBuilder.Entity<UserRole>(entity => 
			{
				entity.HasKey(e => new { e.UserId, e.RoleId });
				entity.HasIndex(e => new { e.UserId, e.RoleId }).IsUnique();

				entity.HasOne(d => d.Role).WithMany(p => p.UserRoles).HasForeignKey(k => k.RoleId);
				entity.HasOne(d => d.User).WithMany(p => p.UserRoles).HasForeignKey(k => k.UserId);
			});

			// build usertoken mapping
			modelBuilder.Entity<UserToken>(entity => 
			{
				entity.HasOne(d => d.User).WithMany(d => d.UserTokens).HasForeignKey(k => k.UserId);
				entity.Property(e => e.RefreshToken).HasMaxLength(450).IsRequired();
				entity.Property(e => e.AccessToken).HasMaxLength(450).IsRequired();
			});
		}
	}
}
