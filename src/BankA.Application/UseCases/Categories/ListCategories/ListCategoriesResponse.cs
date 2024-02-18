using BankA.Application.Models;
using BankA.Application.UseCases.Categories.Models;

namespace BankA.Application.UseCases.Categories.ListCategories
{
    public class ListCategoriesResponse : PagedListResponse<CategoryModel>
    {
        public ListCategoriesResponse(PagedListResponse<CategoryModel> pagedList) : base(pagedList.Data, pagedList.RecordCount, pagedList.PageIndex, pagedList.PageSize)
        {
        }
    }
}
