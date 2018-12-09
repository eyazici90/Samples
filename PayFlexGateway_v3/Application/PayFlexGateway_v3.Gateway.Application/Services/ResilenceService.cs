using PayFlexGateway_v3.Gateway.Application.Contracts;
using Polly;
using Polly.CircuitBreaker;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayFlexGateway_v3.Gateway.Application.Services
{
    public class ResilenceService : IResilenceService
    {
        private static CircuitBreakerPolicy _circuitBreaker =  Policy
             .Handle<Exception>() 
             .CircuitBreakerAsync(
                 exceptionsAllowedBeforeBreaking: 2,
                 durationOfBreak: TimeSpan.FromSeconds(10)
             );

        public Exception LastHandledExceptionByCircuitBreaker =>
        _circuitBreaker.LastException;

        public bool CheckIfBreakerStateOpened() =>
            _circuitBreaker.CircuitState == CircuitState.Open;
        
        public async Task ExecuteWithCircuitBreakerAsync(Func<Task> execution)
        {
            await _circuitBreaker.ExecuteAsync(async () =>
            {
                await execution();
            });
        }
    }
}
