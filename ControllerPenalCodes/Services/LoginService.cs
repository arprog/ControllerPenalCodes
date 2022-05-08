using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using ControllerPenalCodes.Models.Entities;
using ControllerPenalCodes.Interfaces.RepositoryInterfaces;
using ControllerPenalCodes.Interfaces.ServiceInterfaces;
using ControllerPenalCodes.Models.ViewModels.LoginViewModels;
using ControllerPenalCodes.Models.ViewModels.UserViewModels;
using ControllerPenalCodes.Shared;
using Microsoft.IdentityModel.Tokens;

namespace ControllerPenalCodes.Services
{
	public class LoginService : ILoginService
	{
        private readonly IUserRepository _userRepository;

		public LoginService(IUserRepository userRepository)
		{
            _userRepository = userRepository;
		}

		public async Task<Response<TokenViewModel>> LoginAuthentication(Authentication authentication, CreateUserViewModel userViewModel)
		{
            var password = Security.GenerateHashPassword(userViewModel.Password);

            var user = await _userRepository.GetByLogin(userViewModel.Username, password);

            if (user == null)
                return Response<TokenViewModel>.ResponseService(false, "Invalid username or password.");

            var token = GenerateToken(authentication, user);

            return Response<TokenViewModel>.ResponseService(true, token);
		}

        private static TokenViewModel GenerateToken(Authentication authentication, User user)
        {
            DateTime creationDate = DateTime.Now;
            DateTime expirationDate = creationDate + TimeSpan.FromHours(2);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                NotBefore = creationDate.ToUniversalTime(),
                Expires = expirationDate.ToUniversalTime(),
                SigningCredentials = authentication.SigningCredentials
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var tokenViewModel = new TokenViewModel
            {
                Authenticated = true,
                Created = creationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                AccessToken = tokenHandler.WriteToken(token)
            };

            return tokenViewModel;
        }
    }
}
