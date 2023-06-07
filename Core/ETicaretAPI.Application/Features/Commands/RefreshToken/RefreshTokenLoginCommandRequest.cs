using MediatR;

namespace ETicaretAPI.Application.Features.Commands.RefreshToken
{
    public class RefreshTokenLoginCommandRequest : IRequest<RefreshTokenLoginCommandResponse>
    {
        public string RefreshToken { get; set; }
    }
}
