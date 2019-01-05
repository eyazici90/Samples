using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlexGateway_v3.Gateway.Shared.Queries
{
    public class GetAllLogsQuery : IRequest<IList<object>>
    {
        public DateTime? CreationDate { get; private set; }
        public GetAllLogsQuery()
        {
            CreationDate = DateTime.Now;
        }
    }
}
