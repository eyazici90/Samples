using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlex.Identity.Shared.ResponseObjects
{
    public class GetTokenResponse
    {
        public string Token { get; set; }

        public DateTimeOffset ExpiredDate { get; set; }
    }
}
