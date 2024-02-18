using BankA.Application.Interfaces;
using BankA.Application.Services;
using BankA.Application.UseCases.Reports._Models;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BankA.Application.UseCases.Reports.GetCashFlowReport
{
    class GetCashFlowReportRequestHandler : IRequestHandler<GetCashFlowReportRequest, GetCashFlowReportResponse>
    {
        private readonly IAppDataDbContext _bankADbContext;
        private readonly PeriodFilterService _periodFilterService;
        public GetCashFlowReportRequestHandler(IAppDataDbContext bankADbContext, PeriodFilterService periodFilterService)
        {
            _bankADbContext = bankADbContext;
            _periodFilterService = periodFilterService;
        }

        public async Task<GetCashFlowReportResponse> Handle(GetCashFlowReportRequest request, CancellationToken cancellationToken)
        {
            var dateRange = _periodFilterService.GetDateRange(request.AccountId, request.Period);

            var result = _bankADbContext.Transactions
                .Where(q => q.Account.Id == request.AccountId)
                .Where(q => q.TransactionDate >= dateRange.StartDate)
                .Where(q => q.TransactionDate <= dateRange.EndDate)
                .GroupBy(q => new { Year = q.TransactionDate.Year, Month = q.TransactionDate.Month })
                .Select(x => new CashFlowMonthlyModel
                {
                    Year = x.Key.Year,
                    Month = x.Key.Month,
                    CreditAmount = x.Sum(x => x.CreditAmount),
                    DebitAmount = x.Sum(x => x.DebitAmount)
                }).OrderBy(q => q.Year).ThenBy(q => q.Month).ToList();

            var startDate = dateRange.StartDate;
            while (startDate <= dateRange.EndDate)
            {
                if (!result.Any(q => q.Month == startDate.Month && q.Year == startDate.Year))
                    result.Add(new CashFlowMonthlyModel() { Month = startDate.Month, Year = startDate.Year });

                startDate = startDate.AddMonths(1);
            }

            result = result.OrderBy(o => o.Year).ThenBy(o => o.Month).ToList();

            return new GetCashFlowReportResponse(result);
        }
    }
}
