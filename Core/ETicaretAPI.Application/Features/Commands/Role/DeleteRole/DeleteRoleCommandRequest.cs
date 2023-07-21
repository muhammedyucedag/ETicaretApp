using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Role.DeleteRole
{
    public class DeleteRoleCommandRequest :IRequest<DeleteRoleCommandResponse>
    {
        public string Name { get; set; }
    }
}