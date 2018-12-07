using MediatR;
using PayFlexGateway_v3.Gateway.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PayFlexGateway_v3.Gateway.Queries
{
    public class GetAllLogsQueryHandler : IRequestHandler<GetAllLogsQuery, IList<object>>
    {
        public async Task<IList<object>> Handle(GetAllLogsQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new List<object>
            {
                new { Body = "Test"}
            });
        }
    }
}
