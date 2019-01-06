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
    public class UserEntityTypeConfiguration: GalaxyBaseAuditEntityTypeConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);
            builder.ToTable("Users");

            builder.HasMany(e => e.UserRoles)
                .WithOne()
                .HasForeignKey(e => e.UserId);

            builder.HasMany(e => e.UserPermissions)
                .WithOne()
                .HasForeignKey(e => e.UserId);

            builder.HasMany(e => e.UserTenants)
                .WithOne()
                .HasForeignKey(e=>e.UserId);

            builder.HasOne<Tenant>()
                .WithMany()
                .HasForeignKey(e => e.TenantId);
        }
    }
}
