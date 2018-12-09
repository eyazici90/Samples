using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayFlexGateway_v3.Gateway.Shared.Commands
{
    public class ExecuteWithCircuitBreakerCommand : IRequest<bool>
    {
        public Func<Task> Execution{ get; set; }
        public object CircuitBreaker { get; set; }
    }
}
