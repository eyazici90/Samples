using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlex.Identity.Shared.Requests
{
   public class CreateUserRequest
    { 
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int? TenantId { get; set; }
    }
}
