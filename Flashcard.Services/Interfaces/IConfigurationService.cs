using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcard.Services.Interfaces
{
    public interface IConfigurationService
    {
		string SecurityKey { get; }
		string Issuer { get; }
	}
}
