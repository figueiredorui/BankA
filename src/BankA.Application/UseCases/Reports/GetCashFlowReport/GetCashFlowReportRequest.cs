using MediatR;

namespace BankA.Application.UseCases.Reports.GetCashFlowReport
{
    public class GetCashFlowReportRequest : IRequest<GetCashFlowReportResponse>
    {
        public GetCashFlowReportRequest(int accountId, int period)
        {
            AccountId = accountId;
            Period = period;
        }

        public int AccountId { get; set; }
        public int Period { get; set; }
    }
}
