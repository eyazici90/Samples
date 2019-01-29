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
    public class UserAssignedToRoleEntityTypeConfiguration : GalaxyBaseEntityTypeConfiguration<UserAssignedToRole>
    {
        public override void Configure(EntityTypeBuilder<UserAssignedToRole> builder)
        {

            base.Configure(builder,true);

            builder.ToTable("UserRoles");
             
            builder.HasIndex(e => new { e.UserId, e.RoleId });

            builder.Property(e => e.RoleId).HasColumnName(nameof(UserAssignedToRole.RoleId));
            builder.Property(e => e.UserId).HasColumnName(nameof(UserAssignedToRole.UserId));

            builder.HasOne<Tenant>()
             .WithMany()
             .HasForeignKey(e => e.TenantId);

        }
    }
}
