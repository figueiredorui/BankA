using BankA.Application.Interfaces;
using BankA.Application.UseCases.Transactions.Models;
using BankA.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankA.Application.Services
{
    public class CategoriseTransactionsService
    {
        private readonly IAppDataDbContext _bankADbContext;
        public CategoriseTransactionsService(IAppDataDbContext bankADbContext)
        {
            _bankADbContext = bankADbContext;
        }

        public async Task<CategoriseResultModel> Categorise(int accountId)
        {
            var report = new CategoriseResultModel();
            var transactionLst = GetUncategorised(accountId);

            var rules = GetRules();

            foreach (var t in transactionLst)
            {
                var rule = rules.Where(q => q.KeywordMatch(t.Description, t.Balance)).FirstOrDefault();
                if (rule != null)
                {
                    var transaction = await GetTransactionById(t.Id);

                    transaction.ApplyRule(rule);

                    await _bankADbContext.SaveChangesAsync();

                    report.Categorised++;
                }
                else
                {
                    report.Uncategorised++;
                }
            }
            return report;
        }

        private List<Transaction> GetUncategorised(int accountId)
        {
            var query = _bankADbContext.Transactions
                            .Where(q => q.Account.Id == accountId)
                            .Where(q => q.Category == null || q.Merchant == null)
                            .AsNoTracking();

            return query.ToList();
        }

        private List<Rule> GetRules()
        {
            var result = _bankADbContext.Rules.Include(i => i.Category).Include(i => i.Merchant).AsNoTracking().ToList();
            return result;
        }

        private async Task<Transaction> GetTransactionById(int transactionId)
        {
            var result = await _bankADbContext.Transactions.Include(i => i.Account)
                                .Include(i => i.Merchant)
                                .Include(i => i.Category).ThenInclude(i => i.Parent)
                            .Where(q => q.Id == transactionId).FirstOrDefaultAsync();
            return result;
        }
    }
}
