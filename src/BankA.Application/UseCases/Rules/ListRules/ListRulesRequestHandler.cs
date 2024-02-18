using BankA.Application.Interfaces;
using BankA.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BankA.Application.UseCases.Rules.ListRules
{
    class ListRulesRequestHandler : IRequestHandler<ListRulesRequest, ListRulesResponse>
    {
        private readonly IAppDataDbContext _bankADbContext;

        public ListRulesRequestHandler(IAppDataDbContext bankADbContext)
        {
            _bankADbContext = bankADbContext;
        }

        public async Task<ListRulesResponse> Handle(ListRulesRequest request, CancellationToken cancellationToken)
        {
            var query = _bankADbContext.Rules.Include(i => i.Category).ThenInclude(i => i.Parent).Include(i => i.Merchant).AsNoTracking();

            query = FilterQuery(query, request);

            var result = query.Select(s => new RuleModel()
            {
                RuleId = s.Id,
                Keywords = s.Keywords,
                CategoryId = s.Category.Id,
                CategoryFullName = s.Category.CategoryFullName,
                MerchantId = s.Merchant.Id,
                MerchantName = s.Merchant.MerchantName
            })
            .OrderBy(a => a.Keywords)
            .PagedList(request.PageIndex, request.PageSize);

            return await Task.FromResult(new ListRulesResponse(result));
        }

        private static IQueryable<Domain.Rule> FilterQuery(IQueryable<Domain.Rule> query, ListRulesRequest request)
        {
            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(q => q.Keywords.Contains(request.Search, System.StringComparison.InvariantCultureIgnoreCase)
                                || q.Category.CategoryName.Contains(request.Search, System.StringComparison.InvariantCultureIgnoreCase)
                                || q.Merchant.MerchantName.Contains(request.Search, System.StringComparison.InvariantCultureIgnoreCase));
            }

            return query;
        }
    }
}
