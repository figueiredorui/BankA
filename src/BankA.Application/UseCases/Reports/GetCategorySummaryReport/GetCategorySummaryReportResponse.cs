using BankA.Application.UseCases.Reports._Models;
using System;
using System.Collections.Generic;

namespace BankA.Application.UseCases.Reports.GetCategorySummaryReport
{
    public class GetCategorySummaryReportResponse
    {
        public GetCategorySummaryReportResponse(List<CategorySummaryModel> data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public List<CategorySummaryModel> Data { get; set; }
    }

}
