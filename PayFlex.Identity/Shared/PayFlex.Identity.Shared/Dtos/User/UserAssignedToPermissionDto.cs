using Galaxy.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlex.Identity.Shared.Dtos.User
{
    public class UserAssignedToPermissionDto 
    {
        public int UserId { get; set; }

        public int PermissionId { get; set; }
    }
}
