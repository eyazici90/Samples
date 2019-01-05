using Galaxy.EntityFrameworkCore.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PayFlex.Identity.Domain.AggregatesModel.PermissionAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlex.Identity.Infrastructure.EntityConfigurations.PermissionConfigurations
{
    public class PermissionEntityTypeConfiguration : GalaxyBaseAuditEntityTypeConfiguration<Permission>
    {
        public override void Configure(EntityTypeBuilder<Permission> builder)
        {
            base.Configure(builder);
            builder.ToTable("Permissions");

        }
    }
}
