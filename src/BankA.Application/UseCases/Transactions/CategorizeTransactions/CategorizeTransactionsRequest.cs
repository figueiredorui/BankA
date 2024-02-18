using MediatR;

namespace BankA.Application.UseCases.Transactions.CategorizeTransactions
{
    public class CategorizeTransactionsRequest : IRequest<CategorizeTransactionsResponse>
    {
        public CategorizeTransactionsRequest(int accountId)
        {
            AccountId = accountId;
        }

        public int AccountId { get; set; }
    }
}
