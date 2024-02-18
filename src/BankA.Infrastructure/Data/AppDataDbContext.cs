using BankA.Application.Interfaces;
using BankA.Application.UseCases.Transactions.SearchTransactions;
using BankA.Domain;
using DotNetLib.EfCore.EntityAudit;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BankA.Infrastructure.Data
{
    public class AppDataDbContext : DbContext, IAppDataDbContext
    {
        private readonly IIdentityUserId _identityUser;
        public AppDataDbContext(DbContextOptions<AppDataDbContext> options, IIdentityUserId identityUser)
          : base(options)
        {
            _identityUser = identityUser;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Merchant> Merchants { get; set; }
        public virtual DbSet<Rule> Rules { get; set; }
        public virtual DbSet<TransactionFile> Files { get; set; }
        public virtual DbSet<TransactionView> TransactionView { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.EnsureEntityAudit(_identityUser.UserId);
            return await base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public async Task<Account> GetAccountById(int id)
        {
            var result = await Accounts.Where(q => q.Id == id)
                                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<Category> GetCategoryById(int categoryId)
        {
            var result = await Categories.Include(i => i.Parent)
                                .Where(x => x.Id == categoryId)
                                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<Merchant> GetMerchantById(int merchantId)
        {
            var result = await Merchants
                                .Where(x => x.Id == merchantId)
                                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<Transaction> GetTransactionById(int transactionId)
        {
            var result = await Transactions.Include(i => i.Account)
                                .Include(i => i.Merchant)
                                .Include(i => i.Category).ThenInclude(i => i.Parent)
                                .Where(q => q.Id == transactionId)
                                .FirstOrDefaultAsync();
            return result;
        }



    }

}
