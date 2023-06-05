using ETicaretAPI.Application.Abstractions.Token;
using ETicaretAPI.Domain.Entites.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ETicaretAPI.Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;
        private readonly TimeSpan _defaultValidityTime;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;

            var validityTime = _configuration.GetValue<string>("Token:ValidityTime");
            if (validityTime != null)
                _defaultValidityTime = TimeSpan.Parse(validityTime);
        }

        public Application.DTOs.Token CreateAccessToken(AppUser appUser, TimeSpan validityTime)
        {
            //Token üretimi;
            Application.DTOs.Token token = new();

            //SecurityKey'in simetriğini alıyoruz.
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

            //Şifrelenmiş kimliği oluşturuyoruz.
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            //Oluşturulacak token ayarlarını veriyoruz
            token.Expiration = DateTime.UtcNow.Add(validityTime);
            JwtSecurityToken securityToken = new(
               audience: _configuration["Token:Audience"],
               issuer: _configuration["Token:Issuer"],
               expires: token.Expiration,
               notBefore: DateTime.UtcNow,
               signingCredentials: signingCredentials
               );

            //Token oluşturucu sınıfından bir önrek alacağız.
            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken = tokenHandler.WriteToken(securityToken);
            return token;

        }

        public Application.DTOs.Token CreateAccessToken(AppUser appUser)
        {
            return CreateAccessToken(appUser, _defaultValidityTime);
        }
    }
}
