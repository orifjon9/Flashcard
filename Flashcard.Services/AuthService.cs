﻿using Flashcard.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Flashcard.ViewModels;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Flashcard.Services
{
	public class AuthService : IAuthService
	{
		private readonly IConfigurationService configurationService;
		public AuthService(IConfigurationService service)
		{
			configurationService = service;
		}

		public async Task<UserWithToken> AuthenticateAsync(LoginViewModel loginViewModel)
		{
			if("admin".Equals(loginViewModel.Email) && "admin".Equals(loginViewModel.Password))
			{
				var task = Task.Run(() => 
					new UserWithToken
					{
						User = new UserViewModel { Id = 1, Email = "orifjon9@gmail.com", UserName = "Orifjon" },
						Token = BuildToken()
					});

				return await task;
			}

			return null;
		}

		private string BuildToken()
		{
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configurationService.SecurityKey));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(configurationService.Issuer, configurationService.Issuer,
					expires: DateTime.Now.AddDays(1), 
					signingCredentials: creds);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
