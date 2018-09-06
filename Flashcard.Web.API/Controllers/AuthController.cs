using Flashcard.Services.Interfaces;
using Flashcard.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flashcard.Web.API.Controllers
{
	[Route("api/auth")]
	[ApiController]
    public class AuthController : ControllerBase
    {
		private readonly IAuthService _service;
		public AuthController(IAuthService service)
		{
			_service = service;
		}

		[HttpPost("sign-in")]
		[AllowAnonymous]
		public async Task<ActionResult<UserWithToken>> SignIn([FromBody] LoginViewModel loginViewModel)
		{
			return await _service.AuthenticateAsync(loginViewModel);
		}

		[HttpPost("sign-up")]
		[AllowAnonymous]
		public async Task<ActionResult<UserWithToken>> SignUp([FromBody] RegisterViewModel registerViewModel)
		{
			return await _service.SignUpAsync(registerViewModel);
		}
    }
}
