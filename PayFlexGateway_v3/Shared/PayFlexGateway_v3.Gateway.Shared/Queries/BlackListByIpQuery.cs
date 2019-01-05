﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlexGateway_v3.Gateway.Shared.Queries
{ 
    public class BlackListByIpQuery : IRequest<bool>
    {
        public DateTime? CreationDate { get; private set; }
        public string Ip { get; set; }
        public BlackListByIpQuery(string ip)
        {
            Ip = ip;
            CreationDate = DateTime.Now;
        }
    }
}