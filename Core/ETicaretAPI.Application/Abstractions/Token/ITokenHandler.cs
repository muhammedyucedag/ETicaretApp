﻿using ETicaretAPI.Domain.Entites.Identity;

namespace ETicaretAPI.Application.Abstractions.Token
{
    public interface ITokenHandler
    {
        DTOs.Token CreateAccessToken(int second);
        string CreateRefreshToken();
    }
}
