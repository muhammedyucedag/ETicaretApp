using MediatR;

namespace ETicaretAPI.Application.Features.Commands.AuthorizationEndpoint.AssignRoleEndPoint
{
    public class AssignRoleEndPointCommandRequest : IRequest<AssignRoleEndPointCommandResponse>
    {
        public string[] Roles { get; set; }
        public string Code { get; set; }
    }
}