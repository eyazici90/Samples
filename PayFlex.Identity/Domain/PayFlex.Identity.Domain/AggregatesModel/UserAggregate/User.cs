using Galaxy.Domain;
using Galaxy.Identity;
using PayFlex.Identity.Domain.Events;
using PayFlex.Identity.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayFlex.Identity.Domain.AggregatesModel.UserAggregate
{
    public sealed class User : FullyAuditIdentityUserEntity, IAggregateRoot
    {
        private List<UserAssignedToTenant> _userTenants;

        public IEnumerable<UserAssignedToTenant> UserTenants => _userTenants.AsEnumerable();

        private List<UserAssignedToRole> _userRoles;

        public IEnumerable<UserAssignedToRole> UserRoles => _userRoles.AsEnumerable();

        private List<UserAssignedToPermission> _userPermissions;

        public IEnumerable<UserAssignedToPermission> UserPermissions => _userPermissions.AsEnumerable();

        private User() 
        {
            _userRoles = new List<UserAssignedToRole>();
            _userPermissions = new List<UserAssignedToPermission>();
            _userTenants = new List<UserAssignedToTenant>();
        }

        private User(string userName, int tenantId) : this()
        {
            this.UserName = !string.IsNullOrWhiteSpace(userName) ? userName
                                                                 : throw new ArgumentNullException(nameof(userName));
            if (tenantId < 0)
                throw new IdentityDomainException($"Invalid Tenant Id : {tenantId}");

            TenantId = tenantId;
        }

        public static User Create(string userName, int tenantId)
        {
            return new User(userName, tenantId);
        }

        public void AssignRole(int roleId)
        {
            if (roleId < 0)
                throw new IdentityDomainException($"Invalid RoleId : {roleId}");
            var userRole = UserAssignedToRole.Create(this.Id, roleId);
            this._userRoles.Add(userRole);
            ApplyEvent(new PermissionAssignedToUserDomainEvent(this));
        }

        public void AssignPermission(int permissionId)
        {
            if (permissionId < 0)
                throw new IdentityDomainException($"Invalid PermissionId : {permissionId}");
            var userPermission = UserAssignedToPermission.Create(this.Id, permissionId);
            this._userPermissions.Add(userPermission);
            ApplyEvent(new PermissionAssignedToUserDomainEvent(this));
        }

        public void AssignTenant(int tenantId)
        {
            if (tenantId < 0)
                throw new IdentityDomainException($"Invalid TenantId : {tenantId}");
            var userTenant = UserAssignedToTenant.Create(this.Id, tenantId);
            this._userTenants.Add(userTenant);
            ApplyEvent(new TenantAssignedToUserDomainEvent(this));
        }

    }
}
