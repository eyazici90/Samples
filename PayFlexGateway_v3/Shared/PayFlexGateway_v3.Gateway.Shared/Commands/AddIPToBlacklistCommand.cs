using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlexGateway_v3.Gateway.Shared.Commands
{
    public class AddIPToBlacklistCommand : IRequest<bool>
    {
        public string ClientIp { get; set; }

        public int? BlacklistedDurationSeconds { get; set; }
    }
}
