using Galaxy.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlex.Identity.Shared.Dtos.User
{
    public class UserDto 
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int? TenantId { get; set; }
    }
}
