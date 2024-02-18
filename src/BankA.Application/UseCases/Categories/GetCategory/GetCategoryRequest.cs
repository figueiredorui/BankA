using MediatR;

namespace BankA.Application.UseCases.Categories.GetCategory
{
    public class GetCategoryRequest : IRequest<GetCategoryResponse>
    {
        public GetCategoryRequest(int categoryId)
        {
            CategoryId = categoryId;
        }

        public int CategoryId { get; set; }
    }
}
