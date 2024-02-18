using BankA.Domain.Enums;

namespace BankA.Application.UseCases.Reports._Models
{
    public class MerchantSummaryModel
    {
        public int MerchantId { get; set; }
        public string MerchantName { get; set; }
        public CategoryTypeEnum CategoryType { get; set; }
        public decimal Amount { get; set; }

    }
}
