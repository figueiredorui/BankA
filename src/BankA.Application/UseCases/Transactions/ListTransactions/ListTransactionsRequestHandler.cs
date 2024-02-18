using BankA.Application.Interfaces;
using BankA.Application.Models;
using BankA.Application.Services;
using BankA.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BankA.Application.UseCases.Transactions.SearchTransactions
{
    class ListTransactionsRequestHandler : IRequestHandler<ListTransactionsRequest, ListTransactionsResponse>
    {
        private readonly IAppDataDbContext _bankADbContext;
        private readonly PeriodFilterService _periodFilterService;
        public ListTransactionsRequestHandler(IAppDataDbContext bankADbContext, PeriodFilterService periodFilterService)
        {
            _bankADbContext = bankADbContext;
            _periodFilterService = periodFilterService;
        }


        public async Task<ListTransactionsResponse> Handle(ListTransactionsRequest request, CancellationToken cancellationToken)
        {
            if (true)
            {
                var result = InMemoryQuery(request);
                return await Task.FromResult(new ListTransactionsResponse(result));
            }
            else
            {
                var result = SqlServerQuery(request);
                return await Task.FromResult(new ListTransactionsResponse(result));
            }

        }

        private PagedListResponse<TransactionView> SqlServerQuery(ListTransactionsRequest request)
        {
            var query = _bankADbContext.TransactionView
                                        .Where(q => q.AccountId == request.AccountId).AsNoTracking();
            query = FilterQuery(query, request);

            var result = query
                        .OrderByDescending(s => s.TransactionDate)
                        .ThenByDescending(s => s.Id)
                        .PagedList(request.PageIndex, request.PageSize);

            return result;
        }

        private PagedListResponse<TransactionView> InMemoryQuery(ListTransactionsRequest request)
        {
            var accountTransactions = _bankADbContext.Transactions
                .Include(i => i.Account)
                .Include(i => i.Merchant)
                .Include(i => i.Category)
                .ThenInclude(i => i.Parent)
                .Where(q => q.Account.Id == request.AccountId)
                .ToList();

            decimal sum = 0;
            var query = accountTransactions
                .OrderBy(o => o.TransactionDate).ThenBy(o => o.Id)
                .Select((x, i) =>
                new TransactionView
                {
                    Id = x.Id,
                    AccountId = x.Account.Id,
                    Description = x.Description,
                    TransactionDate = x.TransactionDate,
                    CategoryId = x.Category?.Id,
                    CategoryName = x.Category?.CategoryFullName,
                    CategoryType = x.Category?.CategoryType.ToString(),
                    MerchantId = x.Merchant?.Id,
                    MerchantName = x.Merchant?.MerchantName,
                    Balance = x.CreditAmount - x.DebitAmount,
                    RunningBalance = (sum += (x.CreditAmount - x.DebitAmount))
                })
            .ToList();

            query = FilterQuery(query, request);

            query.Reverse();
            var result = query
                        .PagedList(request.PageIndex, request.PageSize);

            return result;
        }

        private IQueryable<TransactionView> FilterQuery(IQueryable<TransactionView> query, ListTransactionsRequest request)
        {
            if (!string.IsNullOrEmpty(request.Search))
                query = query.Where(q => q.Description.Contains(request.Search)
                                        || q.CategoryName.Contains(request.Search)
                                        || q.MerchantName.Contains(request.Search));

            if (request.Uncategorized == true)
            {
                query = query.Where(q => q.CategoryId == null || q.MerchantId == null);
            }

            if (request.CategoryId.HasValue == true)
            {
                query = query.Where(q => q.CategoryId == request.CategoryId.Value || q.CategoryParentId == request.CategoryId.Value);
            }

            if (request.MerchantId.HasValue == true)
            {
                query = query.Where(q => q.MerchantId == request.MerchantId.Value);
            }

            if (!string.IsNullOrEmpty(request.CategoryType))
                query = query.Where(q => q.CategoryType.ToLower() == request.CategoryType.ToLower());

            if (!string.IsNullOrEmpty(request.Category))
                query = query.Where(q => q.CategoryName.ToLower() == request.Category.ToLower());


            if (request.Period.HasValue)
            {
                var dateRange = _periodFilterService.GetDateRange(request.AccountId, request.Period.Value);
                query = query.Where(q => q.TransactionDate >= dateRange.StartDate);
                query = query.Where(q => q.TransactionDate <= dateRange.EndDate);
            }

            return query;
        }

        private List<TransactionView> FilterQuery(List<TransactionView> query, ListTransactionsRequest request)
        {

            if (!string.IsNullOrEmpty(request.Search))
                query = query.Where(q => q.Description.Contains(request.Search, System.StringComparison.InvariantCultureIgnoreCase)
                                        || q.CategoryName.Contains(request.Search, System.StringComparison.InvariantCultureIgnoreCase)
                                        || q.MerchantName.Contains(request.Search, System.StringComparison.InvariantCultureIgnoreCase)).ToList();

            if (request.Uncategorized == true)
            {
                query = query.Where(q => q.CategoryId == null || q.MerchantId == null).ToList();
            }

            if (request.CategoryId.HasValue == true)
            {
                query = query.Where(q => q.CategoryId == request.CategoryId.Value).ToList();
            }

            if (request.MerchantId.HasValue == true)
            {
                query = query.Where(q => q.MerchantId == request.MerchantId.Value).ToList();
            }

            if (!string.IsNullOrEmpty(request.CategoryType))
                query = query.Where(q => q.CategoryType.ToString().ToLower() == request.CategoryType.ToLower()).ToList();

            if (!string.IsNullOrEmpty(request.Category))
                query = query.Where(q => q.CategoryName.ToLower() == request.Category.ToLower()).ToList();

            if (!string.IsNullOrEmpty(request.Merchant))
                query = query.Where(q => q.MerchantName.ToLower() == request.Merchant.ToLower()).ToList();

            if (request.Period.HasValue)
            {
                var dateRange = _periodFilterService.GetDateRange(request.AccountId, request.Period.Value);
                query = query.Where(q => q.TransactionDate >= dateRange.StartDate).ToList();
                query = query.Where(q => q.TransactionDate <= dateRange.EndDate).ToList();
            }

            return query;
        }
    }
}
