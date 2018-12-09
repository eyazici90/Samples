using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlexGateway_v3.Gateway.Shared.Commands
{
    public class LogRequestCommand : IRequest<bool>
    {
        public string Headers { get; set; }
        public string Body { get; set; }
        public string Url { get; set; }
    }
}
