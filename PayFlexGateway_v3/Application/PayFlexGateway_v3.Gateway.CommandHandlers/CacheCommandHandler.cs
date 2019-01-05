﻿using Galaxy.Cache;
using MediatR;
using PayFlexGateway_v3.Gateway.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PayFlexGateway_v3.Gateway.CommandHandlers
{ 
    public class CacheCommandHandler : IRequestHandler<AddValueToCacheCommand, bool>
    {
        private readonly ICache _cache;
        public CacheCommandHandler(ICache cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task<bool> Handle(AddValueToCacheCommand request, CancellationToken cancellationToken)
        {
            await this._cache.SetAsync(request.CacheKey, request.CacheValue);
            return true;
        }
    }
}
