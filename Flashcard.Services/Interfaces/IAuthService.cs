using Flashcard.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Flashcard.Services.Interfaces
{
    public interface IAuthService
    {
		Task<UserWithToken> AuthenticateAsync(LoginViewModel loginViewModel);
		Task<UserWithToken> SignUpAsync(RegisterViewModel registerViewModel);

    }
}
