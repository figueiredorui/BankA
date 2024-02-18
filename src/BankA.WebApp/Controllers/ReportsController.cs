using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankA.WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ReportsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{accountId}/cashflow")]
        public async Task<ActionResult<Application.UseCases.Reports.GetCashFlowReport.GetCashFlowReportResponse>> GetMonthlyCashFlow(int accountId, int? period)
        {
            var request = new Application.UseCases.Reports.GetCashFlowReport.GetCashFlowReportRequest(accountId, period.Value);
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("{accountId}/category")]
        public async Task<ActionResult<Application.UseCases.Reports.GetCategorySummaryReport.GetCategorySummaryReportResponse>> GetCategorySummary(int accountId, int? period)
        {
            var request = new Application.UseCases.Reports.GetCategorySummaryReport.GetCategorySummaryReportRequest(accountId, period.Value);
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("{accountId}/merchant")]
        public async Task<ActionResult<Application.UseCases.Reports.GetMerchantSummaryReport.GetMerchantSummaryReportResponse>> GetMerchantSummary(int accountId, int? period)
        {
            var request = new Application.UseCases.Reports.GetMerchantSummaryReport.GetMerchantSummaryReportRequest(accountId, period.Value);
            var result = await _mediator.Send(request);
            return Ok(result);
        }


    }
}