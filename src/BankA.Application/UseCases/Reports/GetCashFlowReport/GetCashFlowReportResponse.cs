using BankA.Application.UseCases.Reports._Models;
using System;
using System.Collections.Generic;

namespace BankA.Application.UseCases.Reports.GetCashFlowReport
{
    public class GetCashFlowReportResponse
    {
        public GetCashFlowReportResponse(List<CashFlowMonthlyModel> data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public List<CashFlowMonthlyModel> Data { get; set; }
    }
}
