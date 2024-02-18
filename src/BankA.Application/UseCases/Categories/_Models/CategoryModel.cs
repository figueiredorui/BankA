namespace BankA.Application.UseCases.Categories.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryFullName { get; set; }
        public int? ParentId { get; set; }
        public string ParentCategoryName { get; set; }
        public string CategoryType { get; set; }

    }


}
