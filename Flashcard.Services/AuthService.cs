using Flashcard.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Flashcard.ViewModels;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Linq;
using Flashcard.Repositories;
using AutoMapper;
using Flashcard.Models;

namespace Flashcard.Services
{
	public class AuthService : IAuthService
	{
		private readonly IConfigurationService _configurationService;
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;
		public AuthService(IConfigurationService service, IUserRepository userRepository, IMapper mapper)
		{
			_configurationService = service;
			_userRepository = userRepository;
			_mapper = mapper;
		}

		public async Task<UserWithToken> AuthenticateAsync(LoginViewModel loginViewModel)
		{
			var user = _userRepository.Find(loginViewModel.Email, loginViewModel.Password);
			if(user != null)
			{
				return await GenerateUserWithToken(user);
			}

			return null;
		}

		public async Task<UserWithToken> SignUpAsync(RegisterViewModel registerViewModel)
		{
			var user = new User
			{
				FirstName = registerViewModel.FirstName,
				LastName = registerViewModel.LastName,
				Email = registerViewModel.Email,
				Password = registerViewModel.Password,
				Birthday = registerViewModel.Birthday
			};
			await _userRepository.Register(user);
			await _userRepository.CommitAsync();
			return await GenerateUserWithToken(user);
		}

		private async Task<UserWithToken> GenerateUserWithToken(User user)
		{
			var task = Task.Run(() =>
					new UserWithToken
					{
						User = _mapper.Map<User, UserViewModel>(user),
						Token = BuildToken(user)
					});

			return await task;
		}

		private string BuildToken(User user)
		{
			var claims = new List<Claim>()
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.Email),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				//new Claim(ClaimTypes.NameIdentifier, userId.ToString())
			};	

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configurationService.SecurityKey));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(_configurationService.Issuer, _configurationService.Issuer,
					claims: claims,
					expires: DateTime.Now.AddDays(1), 
					signingCredentials: creds);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
