using BankA.Application.Models;
using MediatR;

namespace BankA.Application.UseCases.Accounts.ListAccounts
{
    public class ListAccountsRequest : PagedListRequest, IRequest<ListAccountsResponse>
    {
        public string Search { get; set; }
    }
}
