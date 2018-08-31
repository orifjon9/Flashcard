using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Flashcard.ViewModels
{
    public class LoginViewModel
    {
		[Required]
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
