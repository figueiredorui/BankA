using BankA.Application.Models;
using MediatR;

namespace BankA.Application.UseCases.Categories.ListCategories
{
    public class ListCategoriesRequest : PagedListRequest, IRequest<ListCategoriesResponse>
    {
        public string CategoryType { get; set; }
        public string Search { get; set; }
        public bool IsParent { get; set; }
    }
}
