using BankA.Domain.Enums;

namespace BankA.Application.UseCases.Reports._Models
{
    public class CategorySummaryModel
    {
        public int ParentId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public CategoryTypeEnum CategoryType { get; set; }
        public decimal Amount { get; set; }

    }

}
