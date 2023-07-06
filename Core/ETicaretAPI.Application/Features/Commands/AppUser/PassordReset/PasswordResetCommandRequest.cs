using MediatR;

namespace ETicaretAPI.Application.Features.Commands.AppUser.PassordReset
{
    public class PasswordResetCommandRequest : IRequest<PasswordResetCommandResponse>
    {
        public string Email { get; set; }
    }
}