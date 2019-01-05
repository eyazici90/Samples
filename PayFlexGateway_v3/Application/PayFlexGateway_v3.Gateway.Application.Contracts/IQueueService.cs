using Galaxy.Application;
using PayFlexGateway_v3.Gateway.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayFlexGateway_v3.Gateway.Application.Contracts
{
    public interface IQueueService : IApplicationService
    {
        Task<bool> PublishCommandThroughEventBus(PublishToQueueThroughEventBusCommand command);
    }
}
