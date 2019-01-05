using Galaxy.Application;
using PayFlexGateway_v3.Gateway.Shared.Commands;
using PayFlexGateway_v3.Gateway.Shared.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayFlexGateway_v3.Gateway.Application.Contracts
{
    public interface IBlackListService : IApplicationService
    {
        Task<bool> AddToBlackList(AddIPToBlacklistCommand command);

        Task<bool> IsIpInBlackList(BlackListByIpQuery query);
    }
}
