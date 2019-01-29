using Galaxy.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlex.Identity.Domain.AggregatesModel.RoleAggregate
{ 
    public class PermissionAssignedToRole : FullyAuditEntity
    {
        public int RoleId { get; private set; }

        public int PermissionId { get; private set; }

        private PermissionAssignedToRole()
        {
        }

        private PermissionAssignedToRole(int roleId, int permissionId) : this()
        {
            this.RoleId = roleId;
            this.PermissionId = permissionId;
        }

        public static PermissionAssignedToRole Create(int roleId, int permissionId)
        {
            return new PermissionAssignedToRole(roleId, permissionId);
        }

    }
}
