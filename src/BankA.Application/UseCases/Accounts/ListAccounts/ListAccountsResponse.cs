using BankA.Application.Models;

namespace BankA.Application.UseCases.Accounts.ListAccounts
{
    public class ListAccountsResponse : PagedListResponse<AccountModel>
    {

        public ListAccountsResponse(PagedListResponse<AccountModel> pagedList) : base(pagedList.Data, pagedList.RecordCount, pagedList.PageIndex, pagedList.PageSize)
        {
        }

    }
}
