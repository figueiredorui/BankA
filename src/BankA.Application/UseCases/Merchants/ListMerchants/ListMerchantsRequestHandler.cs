using BankA.Application.Interfaces;
using BankA.Application.Models;
using BankA.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BankA.Application.UseCases.Merchants.ListMerchants
{
    class ListMerchantsRequestHandler : IRequestHandler<ListMerchantsRequest, ListMerchantsResponse>
    {
        private readonly IAppDataDbContext _bankADbContext;

        public ListMerchantsRequestHandler(IAppDataDbContext bankADbContext)
        {
            _bankADbContext = bankADbContext;
        }

        public async Task<ListMerchantsResponse> Handle(ListMerchantsRequest request, CancellationToken cancellationToken)
        {
            var query = _bankADbContext.Merchants.AsNoTracking();

            query = FilterQuery(query, request);

            var result = query.Select(s => new MerchantModel()
            {
                MerchantId = s.Id,
                MerchantName = s.MerchantName
            })
            .OrderBy(a => a.MerchantName)
            .PagedList(request.PageIndex, request.PageSize);

            return await Task.FromResult(new ListMerchantsResponse(result));
        }

        private static IQueryable<Merchant> FilterQuery(IQueryable<Merchant> query, ListMerchantsRequest request)
        {
            if (!string.IsNullOrEmpty(request.Search))
                query = query.Where(q => q.MerchantName.Contains(request.Search, System.StringComparison.InvariantCultureIgnoreCase));

            return query;
        }
    }
}
