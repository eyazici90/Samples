using Galaxy.Domain;
using Galaxy.Identity;
using PayFlex.Identity.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlex.Identity.Domain.AggregatesModel.RoleAggregate
{
    public sealed class Role : FullyAuditIdentityRoleEntity, IAggregateRoot
    {
        private Role() : base()
        {
        }
        private Role(string roleName) : this()
        {
            this.Name = !string.IsNullOrWhiteSpace(roleName) ? roleName
                                                         : throw new ArgumentNullException(nameof(roleName));
        }
        public static Role Create(string roleName)
        {
            return new Role(roleName);
        }

        public Role ChangeName(string roleName)
        {

            if (string.IsNullOrWhiteSpace(roleName))
                throw new IdentityDomainException($"Invalid role name : {roleName}");

            this.Name = roleName;
            return this;
        }

        public void DeleteThisRole()
        {
            this.IsDeleted = true;
        }
    }
}
