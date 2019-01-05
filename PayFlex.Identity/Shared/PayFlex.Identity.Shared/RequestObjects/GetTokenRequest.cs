using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlex.Identity.Shared.RequestObjects
{
    public class GetTokenRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
