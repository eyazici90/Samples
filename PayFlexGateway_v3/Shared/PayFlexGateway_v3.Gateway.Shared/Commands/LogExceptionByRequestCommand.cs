using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlexGateway_v3.Gateway.Shared.Commands
{
    public class LogExceptionByRequestCommand : IRequest<bool>
    {
        public Exception CreatedException { get; set; }
    }
}
