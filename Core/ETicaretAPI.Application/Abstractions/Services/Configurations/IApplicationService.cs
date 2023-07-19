using ETicaretAPI.Application.DTOs.Configuration;

namespace ETicaretAPI.Application.Abstractions.Services.Configurations
{
    public interface IApplicationService
    {
        List<MenuDto> GetAuthorizeDefinitionEndPoints();
    }
}
