using BankA.Application.Models;
using BankA.Domain;

namespace BankA.Application.UseCases.Transactions.SearchTransactions
{
    public class ListTransactionsResponse : PagedListResponse<TransactionView>
    {
        public ListTransactionsResponse(PagedListResponse<TransactionView> pagedList) : base(pagedList.Data, pagedList.RecordCount, pagedList.PageIndex, pagedList.PageSize)
        {
        }
    }

}
