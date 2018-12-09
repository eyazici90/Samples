using MediatR;
using PayFlexGateway_v3.Gateway.Application.Contracts;
using PayFlexGateway_v3.Gateway.Shared.Commands;
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

        private readonly IMediator _mediatr;
        public ResilenceService(IMediator mediatr)
        {
            _mediatr = mediatr ?? throw new ArgumentNullException(nameof(mediatr));
        }

        public Exception LastHandledExceptionByCircuitBreaker =>
        _circuitBreaker.LastException;

        public bool CheckIfBreakerStateOpened() =>
            _circuitBreaker.CircuitState == CircuitState.Open;

        public async Task ExecuteWithCircuitBreakerAsync(ExecuteWithCircuitBreakerCommand command)
        {
            command.CircuitBreaker = _circuitBreaker;
            await _mediatr.Send(command);
        } 
       
    }
}
