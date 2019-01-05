using Galaxy.DataContext;
using Galaxy.Identity;
using Galaxy.Session;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PayFlex.Identity.Domain.AggregatesModel.RoleAggregate;
using PayFlex.Identity.Domain.AggregatesModel.UserAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlex.Identity.Infrastructure
{ 
    public sealed class PayFlexIdentityContext : GalaxyIdentityDbContext<User, Role, int>
    {
        public const string DEFAULT_SCHEMA = "identity";

        public PayFlexIdentityContext(DbContextOptions options) : base(options)
        {
        }

        public PayFlexIdentityContext(DbContextOptions options, IAppSessionBase appSession) : base(options, appSession)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(DEFAULT_SCHEMA);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims", DEFAULT_SCHEMA);
            modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins", DEFAULT_SCHEMA);
            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("UserRoles", DEFAULT_SCHEMA);
            modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims", DEFAULT_SCHEMA);
            modelBuilder.Entity<IdentityUserToken<int>>().ToTable("UserTokens", DEFAULT_SCHEMA);
        }

    }
}
