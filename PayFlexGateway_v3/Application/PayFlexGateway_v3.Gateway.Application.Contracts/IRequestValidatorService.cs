using Galaxy.Application;
using PayFlexGateway_v3.Gateway.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayFlexGateway_v3.Gateway.Services.Contracts
{
    public interface IRequestValidatorService : IApplicationService
    {
        Task ValidateRequest(ValidateRequestCommand command);
    }
}
