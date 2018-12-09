using Galaxy.Application;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayFlexGateway_v3.Gateway.Application.Contracts
{
    public interface IResilenceService : IApplicationService
    {
        Task ExecuteWithCircuitBreakerAsync(Func<Task> execution);

        bool CheckIfBreakerStateOpened();

        Exception LastHandledExceptionByCircuitBreaker { get; }
    }
}
