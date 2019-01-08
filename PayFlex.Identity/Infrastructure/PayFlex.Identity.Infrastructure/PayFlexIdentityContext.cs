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
        protected override string DEFAULT_SCHEMA { get; set; } = "identity";

        public PayFlexIdentityContext(DbContextOptions options) : base(options)
        {
        }

        public PayFlexIdentityContext(DbContextOptions options, IAppSessionContext appSession) : base(options, appSession)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(DEFAULT_SCHEMA);
            base.OnModelCreating(modelBuilder);
        }
    }
}
