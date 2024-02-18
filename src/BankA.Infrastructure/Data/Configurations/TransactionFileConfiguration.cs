using BankA.Domain;
using DotNetLib.EfCore.EntityAudit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankA.Infrastructure.Data.Configurations
{
    public class TransactionFileConfiguration : IEntityTypeConfiguration<TransactionFile>
    {
        public void Configure(EntityTypeBuilder<TransactionFile> builder)
        {
            builder.ToTable("BankFile");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.ContentType).HasMaxLength(500);
            builder.Property(e => e.FileContent);
            builder.Property(e => e.FileName).HasMaxLength(500);

            builder.HasMany(d => d.Transactions)
               .WithOne()
               .HasForeignKey(d => d.FileId);

            builder.AddAuditingProperties();
        }
    }
}
