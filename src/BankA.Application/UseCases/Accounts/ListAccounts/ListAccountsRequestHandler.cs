using BankA.Application.Interfaces;
using BankA.Application.Models;
using BankA.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BankA.Application.UseCases.Accounts.ListAccounts
{
    class ListAccountsRequestHandler : IRequestHandler<ListAccountsRequest, ListAccountsResponse>
    {

        private readonly IAppDataDbContext _bankADbContext;
        public ListAccountsRequestHandler(IAppDataDbContext accountRepository)
        {
            _bankADbContext = accountRepository;
        }

        public async Task<ListAccountsResponse> Handle(ListAccountsRequest request, CancellationToken cancellationToken)
        {
            var query = _bankADbContext.Accounts.AsNoTracking();
            query = FilterQuery(query, request);

            var result = query.Select(s => new AccountModel()
            {
                Id = s.Id,
                Description = s.Description,
                Closed = s.Closed,
                FileFormat = s.FileFormat.ToString(),
                FileFormatConfiguration = s.FileFormatConfiguration
            })
            .OrderBy(a => a.Description)
            .PagedList(request.PageIndex, request.PageSize);

            return await Task.FromResult(new ListAccountsResponse(result));
        }

        private IQueryable<Account> FilterQuery(IQueryable<Account> query, ListAccountsRequest request)
        {
            if (!string.IsNullOrEmpty(request.Search))
                query = query.Where(q => q.Description.Contains(request.Search, System.StringComparison.InvariantCultureIgnoreCase));
            return query;
        }
    }
}
