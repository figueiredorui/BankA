using MediatR;

namespace BankA.Application.UseCases.Reports.GetCategorySummaryReport
{
    public class GetCategorySummaryReportRequest : IRequest<GetCategorySummaryReportResponse>
    {
        public GetCategorySummaryReportRequest(int accountId, int period)
        {
            AccountId = accountId;
            Period = period;
        }

        public int AccountId { get; set; }
        public int Period { get; set; }
    }
}
