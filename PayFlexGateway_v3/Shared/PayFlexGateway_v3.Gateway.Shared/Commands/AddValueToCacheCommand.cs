using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlexGateway_v3.Gateway.Shared.Commands
{
    public class AddValueToCacheCommand : IRequest<bool>
    {

        public string CacheKey { get; set; }
        public string CacheValue { get; set; }
        public int? SlidingExpireTime { get; set; }
        public int? AbsoluteExpireTime { get; set; }

    }
}
