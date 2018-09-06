using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Flashcard.ViewModels
{
    public class RegisterViewModel
    {
		[Required]
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		[Required]
		public DateTime Birthday { get; set; }
	}
}
