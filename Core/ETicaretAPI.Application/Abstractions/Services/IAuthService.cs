using ETicaretAPI.Application.Abstractions.Services.Authentications;

namespace ETicaretAPI.Application.Abstractions.Services
{
    public interface IAuthService : IExternalAuthentication, IInternalAuthentication
    {

    }
}
