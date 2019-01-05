using Galaxy.EntityFrameworkCore.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PayFlex.Identity.Domain.AggregatesModel.RoleAggregate;
using PayFlex.Identity.Domain.AggregatesModel.UserAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlex.Identity.Infrastructure.EntityConfigurations.RoleConfigurations
{
    public class RoleEntityTypeConfiguration: GalaxyBaseAuditEntityTypeConfiguration<Role>
    {
        public override void Configure(EntityTypeBuilder<Role> builder)
        {
            base.Configure(builder);
            builder.ToTable("Roles");

            builder.HasMany<UserAssignedToRole>()
                .WithOne()
                .HasForeignKey(e => e.RoleId);

        }
    }
}
