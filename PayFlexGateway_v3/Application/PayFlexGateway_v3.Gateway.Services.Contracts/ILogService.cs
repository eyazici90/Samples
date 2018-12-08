using Galaxy.Application;
using PayFlexGateway_v3.Gateway.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayFlexGateway_v3.Gateway.Services.Contracts
{
    public interface ILogService : IApplicationService
    {
        Task<IList<object>> GetAllLogs();

        Task<object> GetLogById(string id);

        Task<bool> LogRequest(LogRequestCommand command);

        Task<bool> LogResponse(LogResponseCommand command);
    }
}
