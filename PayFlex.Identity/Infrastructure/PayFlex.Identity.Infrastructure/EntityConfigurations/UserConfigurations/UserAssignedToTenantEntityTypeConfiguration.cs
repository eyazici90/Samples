using Galaxy.EntityFrameworkCore.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PayFlex.Identity.Domain.AggregatesModel.TenantAggregate;
using PayFlex.Identity.Domain.AggregatesModel.UserAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlex.Identity.Infrastructure.EntityConfigurations.UserConfigurations
{ 
    public class UserAssignedToTenantEntityTypeConfiguration : GalaxyBaseEntityTypeConfiguration<UserAssignedToTenant>
    {
        public override void Configure(EntityTypeBuilder<UserAssignedToTenant> builder)
        {
            base.Configure(builder, true);

            builder.ToTable("UserTenants");

            builder.HasIndex(e => new { e.UserId, e.TenantId});

            builder.HasOne<Tenant>()
             .WithMany()
             .HasForeignKey(e => e.TenantId);

        }
    }
}
