using MediatR;
using PayFlexGateway_v3.Gateway.Commands;
using PayFlexGateway_v3.Gateway.Commands.QueryCommands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PayFlexGateway_v3.Gateway.Queries
{
    public class GetLogByIdQueryHandler : IRequestHandler<GetLogByIdQuery, object>
    {
        public async Task<object> Handle(GetLogByIdQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new { Body = $"{request.Id} log query result"});
        }
    }
}
