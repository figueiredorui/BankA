using BankA.Application.UseCases.Transactions.SearchTransactions;
using BankA.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankA.Infrastructure.Data.Configurations
{
    public class TransactionViewConfiguration : IEntityTypeConfiguration<TransactionView>
    {
        public void Configure(EntityTypeBuilder<TransactionView> builder)
        {
            builder.ToView("BankTransactionsView", "dbo");
            builder.HasNoKey();
            builder.Property(e => e.Balance).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.RunningBalance).HasColumnType("decimal(18, 2)");
        }
    }
}
