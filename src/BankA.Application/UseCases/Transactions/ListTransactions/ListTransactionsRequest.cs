using BankA.Application.Models;
using MediatR;

namespace BankA.Application.UseCases.Transactions.SearchTransactions
{
    public class ListTransactionsRequest : PagedListRequest, IRequest<ListTransactionsResponse>
    {
        public string Search { get; set; }
        public int AccountId { get; set; }
        public int? CategoryId { get; set; }
        public string Category { get; set; }
        public int? MerchantId { get; set; }
        public string Merchant { get; set; }
        public string CategoryType { get; set; }
        public int? Period { get; set; }
        public bool Uncategorized { get; set; }
    }
}
