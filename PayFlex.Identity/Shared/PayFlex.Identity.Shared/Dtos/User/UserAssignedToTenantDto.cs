using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlex.Identity.Shared.Dtos.User
{
    public class UserAssignedToTenantDto
    {
        public int UserId { get; set; }
        public int TenantId { get; set; }
    }
}
