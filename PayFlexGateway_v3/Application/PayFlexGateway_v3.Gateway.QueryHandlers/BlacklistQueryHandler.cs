using Galaxy.Cache;
using MediatR;
using PayFlexGateway_v3.Gateway.Shared.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PayFlexGateway_v3.Gateway.QueryHandlers
{
    public class BlacklistQueryHandler : IRequestHandler<BlackListByIpQuery, bool>
    {
        private readonly ICache _cache;
        public BlacklistQueryHandler(ICache cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task<bool> Handle(BlackListByIpQuery request, CancellationToken cancellationToken) =>
            await this._cache.GetAsync<string>(request.Ip) != null;
        
    } 
}
