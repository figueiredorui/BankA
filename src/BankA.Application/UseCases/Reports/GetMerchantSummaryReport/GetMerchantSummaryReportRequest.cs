using MediatR;

namespace BankA.Application.UseCases.Reports.GetMerchantSummaryReport
{
    public class GetMerchantSummaryReportRequest : IRequest<GetMerchantSummaryReportResponse>
    {
        public GetMerchantSummaryReportRequest(int accountId, int period)
        {
            AccountId = accountId;
            Period = period;
        }

        public int AccountId { get; set; }
        public int Period { get; set; }
    }
}
