using ETicaretAPI.Application.Abstractions.Token;
using ETicaretAPI.Application.DTOs;
using ETicaretAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETicaretAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private readonly UserManager<Domain.Entites.Identity.AppUser> _userManager;
        private readonly SignInManager<Domain.Entites.Identity.AppUser> _signInManager;
        private readonly ITokenHandler _tokenHandler;

        public LoginUserCommandHandler(UserManager<Domain.Entites.Identity.AppUser> userManager, SignInManager<Domain.Entites.Identity.AppUser> signInManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            var appUser = await _userManager.FindByNameAsync(request.UsernameOrEmail);
            if (appUser == null)
                appUser = await _userManager.FindByEmailAsync(request.UsernameOrEmail);

            if (appUser == null)
                throw new NotFoundUserException();

            // appUser request deki şifre ile eşleşiyor mu ?
            SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(appUser, request.Password, false);
            
            if (!signInResult.Succeeded) //Authentication başarılı değilse!           
                throw new AuthenticationErrorException();

            //Yetkiyi belirliyoruz
            Token token = _tokenHandler.CreateAccessToken(appUser);
            return new LoginUserSuccessCommandResponse()
            {
                Token = token,
            };

        }
    }
}
