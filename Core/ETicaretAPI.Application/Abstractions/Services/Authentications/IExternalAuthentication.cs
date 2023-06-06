using ETicaretAPI.Application.DTOs;

namespace ETicaretAPI.Application.Abstractions.Services.Authentications
{
    public interface IExternalAuthentication
    {
        //Task<Response> - (Request) 

        Task<DTOs.Token> FacebookLoginAsync(string authToken, int accessTokenLifeTime);

        Task<DTOs.Token> GoogleLoginAsync(string idToken, int accessTokenLifeTime);

        //Task TwitterLoginAsync();

        //Task MicrosoftLoginAsync();

        //Task GithubLoginAsync();

        //Task DiscordLoginAsync();

        //Task InstagramLoginAsync();

        //Task LinkedinLoginAsync();
    }
}
