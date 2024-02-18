using MediatR;

namespace BankA.Application.UseCases.Categories.DeleteCategory
{
    public class DeleteCategoryRequest : IRequest<bool>
    {
        public DeleteCategoryRequest(int categoryId)
        {
            CategoryId = categoryId;
        }

        public int CategoryId { get; set; }
    }
}
