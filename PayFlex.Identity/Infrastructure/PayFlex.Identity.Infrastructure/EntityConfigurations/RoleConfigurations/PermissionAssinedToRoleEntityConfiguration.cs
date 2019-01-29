using Galaxy.EntityFrameworkCore.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PayFlex.Identity.Domain.AggregatesModel.RoleAggregate;
using PayFlex.Identity.Domain.AggregatesModel.TenantAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlex.Identity.Infrastructure.EntityConfigurations.RoleConfigurations
{ 
    public class PermissionAssinedToRoleEntityConfiguration : GalaxyBaseEntityTypeConfiguration<PermissionAssignedToRole>
    {
        public override void Configure(EntityTypeBuilder<PermissionAssignedToRole> builder)
        {
            base.Configure(builder, true);

            builder.ToTable("RolePermissions");

            builder.HasIndex(e => new { e.RoleId, e.TenantId });

            builder.HasOne<Tenant>()
              .WithMany()
              .HasForeignKey(e => e.TenantId);

        }
    }
}
