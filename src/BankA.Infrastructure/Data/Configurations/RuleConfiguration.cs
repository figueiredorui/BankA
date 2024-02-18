using BankA.Domain;
using DotNetLib.EfCore.EntityAudit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankA.Infrastructure.Data.Configurations
{
    public class RuleConfiguration : IEntityTypeConfiguration<Rule>
    {
        public void Configure(EntityTypeBuilder<Rule> builder)
        {
            builder.ToTable("BankRule");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Keywords).HasMaxLength(500);
            builder.HasOne(e => e.Category).WithMany();
            builder.HasOne(e => e.Merchant).WithMany();

            builder.AddAuditingProperties();
        }
    }
}
