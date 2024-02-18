using MediatR;

namespace BankA.Application.UseCases.Transactions.GetTransaction
{
    public class GetTransactionRequest : IRequest<GetTransactionResponse>
    {
        public GetTransactionRequest(int accountId, int transactionId)
        {
            AccountId = accountId;
            TransactionId = transactionId;
        }

        public int TransactionId { get; set; }
        public int AccountId { get; }
    }
}
