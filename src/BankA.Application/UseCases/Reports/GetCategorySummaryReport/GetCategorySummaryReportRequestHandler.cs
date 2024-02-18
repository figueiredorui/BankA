using BankA.Application.Interfaces;
using BankA.Application.Services;
using BankA.Application.UseCases.Reports._Models;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BankA.Application.UseCases.Reports.GetCategorySummaryReport
{
    class GetCategorySummaryReportRequestHandler : IRequestHandler<GetCategorySummaryReportRequest, GetCategorySummaryReportResponse>
    {

        private readonly IAppDataDbContext _bankADbContext;
        private readonly PeriodFilterService _periodFilterService;
        public GetCategorySummaryReportRequestHandler(IAppDataDbContext bankADbContext, PeriodFilterService periodFilterService)
        {
            _bankADbContext = bankADbContext;
            _periodFilterService = periodFilterService;
        }

        public async Task<GetCategorySummaryReportResponse> Handle(GetCategorySummaryReportRequest request, CancellationToken cancellationToken)
        {
            var dateRange = _periodFilterService.GetDateRange(request.AccountId, request.Period);

            var query = _bankADbContext.Transactions
                .Where(q => q.Account.Id == request.AccountId)
                .Where(q => q.TransactionDate >= dateRange.StartDate)
                .Where(q => q.TransactionDate <= dateRange.EndDate)
                .GroupBy(q => new { CategoryId = q.Category.Id, CategoryName = q.Category.CategoryName, CategoryType = q.Category.CategoryType })
                .Select(x => new CategorySummaryModel
                {
                    CategoryId = x.Key.CategoryId,
                    CategoryName = x.Key.CategoryName,
                    CategoryType = x.Key.CategoryType,
                    Amount = x.Sum(x => x.CreditAmount - x.DebitAmount)
                }).OrderBy(q => q.CategoryName).ToList();

            return new GetCategorySummaryReportResponse(query);
        }
    }
}
