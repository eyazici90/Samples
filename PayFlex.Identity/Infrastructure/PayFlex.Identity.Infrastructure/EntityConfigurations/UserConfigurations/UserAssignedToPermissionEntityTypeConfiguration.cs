﻿using Galaxy.EntityFrameworkCore.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PayFlex.Identity.Domain.AggregatesModel.UserAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlex.Identity.Infrastructure.EntityConfigurations.UserConfigurations
{ 
    public class UserAssignedToPermissionEntityTypeConfiguration: GalaxyBaseEntityTypeConfiguration<UserAssignedToPermission>
    {
        public override void Configure(EntityTypeBuilder<UserAssignedToPermission> builder)
        {
            
            base.Configure(builder);
 
            builder.ToTable("UserPermissions");

            builder.Property(e => e.PermissionId).HasColumnName("PermissionId");
            builder.Property(e => e.UserId).HasColumnName("UserId");

        }
    }
}