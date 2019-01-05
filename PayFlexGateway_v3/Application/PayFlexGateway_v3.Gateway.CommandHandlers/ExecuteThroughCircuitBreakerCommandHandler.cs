using MediatR;
using PayFlexGateway_v3.Gateway.Shared.Commands;
using Polly.CircuitBreaker;
using System.Threading;
using System.Threading.Tasks;

namespace PayFlexGateway_v3.Gateway.CommandHandlers
{
    public class ExecuteThroughCircuitBreakerCommandHandler : IRequestHandler<ExecuteThroughCircuitBreakerCommand, bool>
    {
        public async Task<bool> Handle(ExecuteThroughCircuitBreakerCommand request, CancellationToken cancellationToken)
        {
            var circuitBreaker = request.CircuitBreaker as CircuitBreakerPolicy;
            await circuitBreaker.ExecuteAsync(async () =>
            {
                await request.Execution();
            });
            return true;
        }
    }
}
