using BankA.Domain;

namespace BankA.Application.UseCases.Transactions.Models
{
    public class TransactionCategoryModel
    {
        public TransactionCategoryModel(Category category)
        {
            if (category == null)
                return;

            Id = category.Id;
            CategoryFullName = category.CategoryFullName;
            CategoryType = category.CategoryType.ToString();

        }

        public int Id { get; set; }
        public string CategoryFullName { get; set; }

        public string CategoryType { get; set; }
    }


}
