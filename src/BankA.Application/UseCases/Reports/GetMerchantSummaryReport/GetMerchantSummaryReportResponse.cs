using BankA.Application.UseCases.Reports._Models;
using System;
using System.Collections.Generic;

namespace BankA.Application.UseCases.Reports.GetMerchantSummaryReport
{
    public class GetMerchantSummaryReportResponse
    {
        public GetMerchantSummaryReportResponse(List<MerchantSummaryModel> data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public List<MerchantSummaryModel> Data { get; set; }
    }
}
