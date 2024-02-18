using BankA.Domain;
using DotNetLib.EfCore.EntityAudit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankA.Infrastructure.Data.Configurations
{
    public class MerchantConfiguration : IEntityTypeConfiguration<Merchant>
    {
        public void Configure(EntityTypeBuilder<Merchant> builder)
        {
            builder.ToTable("BankMerchant");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.MerchantName).HasMaxLength(50);

            builder.AddAuditingProperties();
        }
    }
}
