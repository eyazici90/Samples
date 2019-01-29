using PayFlex.Identity.Shared.Dtos.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlex.Identity.Shared.Requests
{
    public class ValidateCreadentialsRequest
    {
        public int UserId { get; set; }
        public string Password { get; set; }
    }
}
