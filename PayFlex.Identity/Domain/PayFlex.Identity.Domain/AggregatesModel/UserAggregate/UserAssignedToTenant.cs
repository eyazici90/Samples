﻿using Galaxy.Auditing;
using Galaxy.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlex.Identity.Domain.AggregatesModel.UserAggregate
{ 
    public class UserAssignedToTenant : FullyAuditEntity
    {
        public int UserId { get; private set; } 

        private UserAssignedToTenant() 
        {
        }

        private UserAssignedToTenant(int userId, int tenantId) : this()
        {
            this.UserId = userId;
            this.TenantId = tenantId;
        }

        public static UserAssignedToTenant Create(int userId, int tenantId)
        {
            return new UserAssignedToTenant(userId, tenantId);
        }
    }
}