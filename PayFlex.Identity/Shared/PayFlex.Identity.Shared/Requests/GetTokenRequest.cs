using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlex.Identity.Shared.Requests
{
    public class GetTokenRequest
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public int? TenantId { get; set; }
    }
}
