using Flashcard.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcard.Services
{
	public class ConfigurationService : IConfigurationService
	{

		public ConfigurationService(string key, string issuer)
		{
			this.SecurityKey = key;
			this.Issuer = issuer;
		}
		public string SecurityKey { get; private set; }
		public string Issuer { get; private set; }
	}
}
