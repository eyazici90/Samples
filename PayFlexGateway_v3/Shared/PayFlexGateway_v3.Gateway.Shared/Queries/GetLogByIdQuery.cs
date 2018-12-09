using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlexGateway_v3.Gateway.Shared.Queries
{
    public class GetLogByIdQuery : IRequest<object>
    {
        public DateTime? CreationDate { get; private set; }
        public string Id { get; set; }
        public GetLogByIdQuery(string id)
        {
            Id = id;
            CreationDate = DateTime.Now;
        }
    }
}
