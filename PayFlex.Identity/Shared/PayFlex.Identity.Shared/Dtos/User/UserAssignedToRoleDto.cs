using Galaxy.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlex.Identity.Shared.Dtos.User
{
    public class UserAssignedToRoleDto 
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
