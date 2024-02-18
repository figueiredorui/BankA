using BankA.Application.Interfaces;
using BankA.Domain.ValueObjects;
using System;
using System.Linq;

namespace BankA.Application.Services
{
    internal class PeriodFilterService
    {
        private readonly IAppDataDbContext _bankADbContext;
        public PeriodFilterService(IAppDataDbContext bankADbContext)
        {
            _bankADbContext = bankADbContext;
        }

        public DateRange GetDateRange(int accountId, int period)
        {
            var dateRange = CalculateDateRange(accountId, period);
            return dateRange;
        }

        public DateRange CalculateDateRange(int accountId, int period)
        {
            var lastTransactionDate = GetLastTransactionDate(accountId);

            if (period == 0) //this year
            {
                var startDate = new DateTime(DateTime.Now.Year, 1, 1);
                var endDate = lastTransactionDate;

                return new DateRange(startDate, endDate);
            }
            else if (period == 1) //last year
            {
                var startDate = new DateTime(DateTime.Now.Year - 1, 1, 1);
                var endDate = new DateTime(DateTime.Now.Year - 1, 12, 31);

                return new DateRange(startDate, endDate);
            }
            else if (period == 12) //last 12 month
            {
                var ago12mDate = lastTransactionDate.AddMonths(-12);
                var startDate = new DateTime(ago12mDate.Year, ago12mDate.Month, 1);
                var endDate = lastTransactionDate;

                return new DateRange(startDate, endDate);
            }
            else if (period == 24) //last 24 month
            {
                var ago24mDate = lastTransactionDate.AddMonths(-24);
                var startDate = new DateTime(ago24mDate.Year, ago24mDate.Month, 1);
                var endDate = lastTransactionDate;

                return new DateRange(startDate, endDate);
            }
            else //all
            {
                var firstTransactionDate = GetFirstTransactionDate(accountId);

                var startDate = firstTransactionDate;
                var endDate = lastTransactionDate;

                return new DateRange(startDate, endDate);
            }
        }

        public DateTime GetLastTransactionDate(int accountId)
        {
            var lastTransactionDate = _bankADbContext.Transactions.Where(q => q.Account.Id == accountId).Max(t => (DateTime?)t.TransactionDate);
            if (lastTransactionDate != null)
                return lastTransactionDate.Value;
            else
                return DateTime.Now.Date;
        }

        public DateTime GetFirstTransactionDate(int accountId)
        {
            var lastTransactionDate = _bankADbContext.Transactions.Where(q => q.Account.Id == accountId).Min(t => (DateTime?)t.TransactionDate);
            if (lastTransactionDate != null)
                return lastTransactionDate.Value;
            else
                return DateTime.Now.Date;
        }
    }
}
