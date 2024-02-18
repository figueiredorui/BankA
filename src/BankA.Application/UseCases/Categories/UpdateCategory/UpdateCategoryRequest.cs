using MediatR;

namespace BankA.Application.UseCases.Categories.UpdateCategory
{
    public class UpdateCategoryRequest : IRequest<bool>
    {
        public int CategoryId { get; set; }
        public int ParentId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryType { get; set; }
    }
}
