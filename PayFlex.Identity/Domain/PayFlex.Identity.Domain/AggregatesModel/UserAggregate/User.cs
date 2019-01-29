using Galaxy.Domain; 
using Galaxy.Identity.Domain;
using PayFlex.Identity.Domain.Events;
using PayFlex.Identity.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayFlex.Identity.Domain.AggregatesModel.UserAggregate
{
    public sealed class User : AuditIdentityUserEntity, IAggregateRoot, ISoftDelete
    {
        private List<UserAssignedToTenant> _userTenants;

        public IEnumerable<UserAssignedToTenant> UserTenants => _userTenants.AsEnumerable();

        private List<UserAssignedToRole> _userRoles;

        public IEnumerable<UserAssignedToRole> UserRoles => _userRoles.AsEnumerable();

        private List<UserAssignedToPermission> _userPermissions;

        public IEnumerable<UserAssignedToPermission> UserPermissions => _userPermissions.AsEnumerable();

        public bool IsDeleted { get; private set; }

        private User() 
        {
            _userRoles = new List<UserAssignedToRole>();
            _userPermissions = new List<UserAssignedToPermission>();
            _userTenants = new List<UserAssignedToTenant>();
        }

        private User(string userName) : this()
        {
            this.UserName = !string.IsNullOrWhiteSpace(userName) ? userName
                                                                 : throw new ArgumentNullException(nameof(userName));
        }

        private User(string userName, string email) : this()
        {
            this.UserName = !string.IsNullOrWhiteSpace(userName) ? userName
                                                                 : throw new ArgumentNullException(nameof(userName));

            this.Email = !string.IsNullOrWhiteSpace(email) ? email
                                                                 : throw new ArgumentNullException(nameof(email));
            
        }

        public static User Create(string userName)
        {
            return new User(userName);
        }

        public static User Create(string userName, string email)
        {
            return new User(userName, email);
        }

        public void ChangeUserName(string userName)
        {
            this.UserName = !string.IsNullOrWhiteSpace(userName) ? userName
                                                                           : throw new ArgumentNullException(nameof(userName));
            ApplyEvent(new UserNameChangedDomainEvent(this));
        }

        public void ChangeEmail(string email)
        {
            this.Email = !string.IsNullOrWhiteSpace(email) ? email : Email;
            ApplyEvent(new UserEmailChangedDomainEvent(this));
        }

        public void ChangePhoneNumber(string phoneNumber)
        {
            this.PhoneNumber = !string.IsNullOrWhiteSpace(phoneNumber) ? phoneNumber : PhoneNumber; 
        }
         
        public void AssignRole(int roleId)
        {
            if (roleId < 0)
                throw new IdentityDomainException($"Invalid RoleId : {roleId}");
            var userRole = UserAssignedToRole.Create(this.Id, roleId);
            this._userRoles.Add(userRole);
            ApplyEvent(new RoleAssignedToUserDomainEvent(this));
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

        public void ChangePassword(string passWord)
        {
            if (string.IsNullOrEmpty(passWord))
                throw new IdentityDomainException($"Invalid Password : {passWord}");
            PasswordHash = passWord;
        }
    }
}
