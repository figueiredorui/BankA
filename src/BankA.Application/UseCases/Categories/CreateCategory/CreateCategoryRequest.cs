using MediatR;

namespace BankA.Application.UseCases.Categories.CreateCategory
{
    public class CreateCategoryRequest : IRequest<CreateCategoryResponse>
    {
        public int? ParentId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryType { get; set; }
    }
}
