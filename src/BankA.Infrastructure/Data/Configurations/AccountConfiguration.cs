using BankA.Domain;
using BankA.Domain.Enums;
using BankA.Domain.ValueObjects;
using DotNetLib.EfCore.EntityAudit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace BankA.Infrastructure.Data.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            var jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.General);
            jsonSerializerOptions.WriteIndented = true;

            builder.ToTable("BankAccount");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Description).HasMaxLength(50);
            builder.Property(e => e.FileFormat).HasMaxLength(10).HasConversion(new EnumToStringConverter<StatementFileFormatEnum>());
            builder.Property(e => e.FileFormatConfiguration).HasMaxLength(2000).HasConversion(
                             v => v != null ? JsonSerializer.Serialize(v, jsonSerializerOptions) : null,
                                v => !string.IsNullOrEmpty(v) ? JsonSerializer.Deserialize<FileFormatConfiguration>(v, (JsonSerializerOptions)null) : new FileFormatConfiguration());

            builder.AddAuditingProperties();
        }
    }
}
