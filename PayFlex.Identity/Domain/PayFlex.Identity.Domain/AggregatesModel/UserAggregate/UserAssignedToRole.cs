using Galaxy.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlex.Identity.Domain.AggregatesModel.UserAggregate
{
    public class UserAssignedToRole :  IdentityUserRoleEntity
    {
        private UserAssignedToRole() 
        {
        }

        private UserAssignedToRole(int userId, int roleId) : this()
        {
            this.UserId = userId;
            this.RoleId = roleId;
        }

        public static UserAssignedToRole Create(int userId, int roleId)
        {
            return new UserAssignedToRole(userId, roleId);
        }
    }
}
