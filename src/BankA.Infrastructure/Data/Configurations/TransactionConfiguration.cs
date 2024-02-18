using BankA.Domain;
using DotNetLib.EfCore.EntityAudit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankA.Infrastructure.Data.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("BankTransaction");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.TransactionDate).HasColumnType("date");
            builder.Property(e => e.Description).HasMaxLength(500);
            builder.Property(e => e.TransactionType).HasMaxLength(50);
            builder.Property(e => e.CreditAmount).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.DebitAmount).HasColumnType("decimal(18, 2)");
            builder.HasOne(e => e.Category).WithMany().HasForeignKey("CategoryId");
            builder.HasOne(e => e.Merchant).WithMany().HasForeignKey("MerchantId");

            builder.Ignore(e => e.Balance);

            builder.AddAuditingProperties();
        }
    }
}
