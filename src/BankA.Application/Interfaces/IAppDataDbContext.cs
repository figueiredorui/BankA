using BankA.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BankA.Application.Interfaces
{
    public interface IAppDataDbContext
    {
        DbSet<Account> Accounts { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Rule> Rules { get; set; }
        DbSet<TransactionFile> Files { get; set; }
        DbSet<Transaction> Transactions { get; set; }
        DbSet<Merchant> Merchants { get; set; }
        DbSet<TransactionView> TransactionView { get; set; }

        Task<Account> GetAccountById(int accountId);
        Task<Category> GetCategoryById(int categoryId);
        Task<Merchant> GetMerchantById(int merchantId);
        Task<Transaction> GetTransactionById(int transactionId);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}