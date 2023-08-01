using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.AuthorizationEndpoint.AssignRoleEndPoint
{
    public class AssignRoleEndPointCommandHandler : IRequestHandler<AssignRoleEndPointCommandRequest, AssignRoleEndPointCommandResponse>
    {
        public Task<AssignRoleEndPointCommandResponse> Handle(AssignRoleEndPointCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
