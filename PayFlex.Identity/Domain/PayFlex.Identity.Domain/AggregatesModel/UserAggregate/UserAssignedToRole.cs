
using Galaxy.Auditing;
using Galaxy.Identity.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlex.Identity.Domain.AggregatesModel.UserAggregate
{
    public class UserAssignedToRole :  IdentityUserRoleEntity , IFullyAudit
    { 

        public virtual int? CreatorUserId { get; private set; }
        public virtual DateTime? LastModificationTime { get; private set; }
        public virtual int? LastModifierUserId { get; private set; }
        public virtual DateTime? CreationTime { get; private set; }

        public virtual int? TenantId { get; private set; }

        public virtual bool IsDeleted { get; private set; }

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
     
        public void SyncTenantState(int? tenantId = null)
        {
            if (tenantId.HasValue)
                this.TenantId = tenantId;
        }
        public virtual void SyncAuditState(int? creatorUserId = default, DateTime? lastModificationTime = default, int? lastmodifierUserId = default, DateTime? creationTime = default)
        {
            this.CreatorUserId = creatorUserId;
            this.LastModificationTime = lastModificationTime;
            this.LastModifierUserId = lastmodifierUserId;
            this.CreationTime = creationTime;
        }
    }
}
