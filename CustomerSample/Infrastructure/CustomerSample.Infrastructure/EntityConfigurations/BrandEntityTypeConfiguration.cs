using CustomerSample.Customer.Domain.AggregatesModel.BrandAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CustomerSample.Infrastructure.EntityConfigurations
{
    public class BrandEntityTypeConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.ToTable("Brands");
            builder.HasKey(x => x.Id);

            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd();

            builder.Ignore(e => e.ObjectState);

            // QueryFilter with EF Core. Auto including where clause thx to microsoft team :)
            builder.HasQueryFilter(p => !p.IsDeleted);


            builder.Property(e => e.BrandName)
               .HasColumnType("nvarchar(40)")
               .IsRequired(true);

            builder.Property(e => e.EMail)
               .HasColumnType("nvarchar(30)")
               .IsRequired(true);

            builder.Property(e => e.Gsm)
               .IsRequired(false);


            builder.HasMany(e => e.Merchants)
                .WithOne()
                .HasForeignKey(e => e.BrandId);
                

        }
    }
}
