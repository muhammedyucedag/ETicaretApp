using ETicaretAPI.Application.DTOs;
using ETicaretAPI.Domain.Entites.Identity;

namespace ETicaretAPI.Application.Abstractions.Token
{
    public interface ITokenHandler
    {
        DTOs.Token CreateAccessToken(AppUser appUser,TimeSpan validityTime);
        DTOs.Token CreateAccessToken(AppUser appUser);
    }
}
