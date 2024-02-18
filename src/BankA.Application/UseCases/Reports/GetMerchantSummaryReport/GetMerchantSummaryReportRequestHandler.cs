using BankA.Application.Interfaces;
using BankA.Application.Services;
using BankA.Application.UseCases.Reports._Models;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BankA.Application.UseCases.Reports.GetMerchantSummaryReport
{
    class GetMerchantSummaryReportRequestHandler : IRequestHandler<GetMerchantSummaryReportRequest, GetMerchantSummaryReportResponse>
    {

        private readonly IAppDataDbContext _bankADbContext;
        private readonly PeriodFilterService _periodFilterService;
        public GetMerchantSummaryReportRequestHandler(IAppDataDbContext bankADbContext, PeriodFilterService periodFilterService)
        {
            _bankADbContext = bankADbContext;
            _periodFilterService = periodFilterService;
        }

        public async Task<GetMerchantSummaryReportResponse> Handle(GetMerchantSummaryReportRequest request, CancellationToken cancellationToken)
        {
            var dateRange = _periodFilterService.GetDateRange(request.AccountId, request.Period);

            var query = _bankADbContext.Transactions
               .Where(q => q.Account.Id == request.AccountId)
               .Where(q => q.TransactionDate >= dateRange.StartDate)
               .Where(q => q.TransactionDate <= dateRange.EndDate)
               .GroupBy(q => new { MerchantId = q.Merchant.Id, MerchantName = q.Merchant.MerchantName, CategoryType = q.Category.CategoryType })
               .Select(x => new MerchantSummaryModel
               {
                   MerchantId = x.Key.MerchantId,
                   MerchantName = x.Key.MerchantName,
                   CategoryType = x.Key.CategoryType,
                   Amount = x.Sum(x => x.CreditAmount - x.DebitAmount)
               }).OrderBy(q => q.MerchantName).ToList();

            return new GetMerchantSummaryReportResponse(query);
        }
    }
}
