using BankA.Domain;
using MediatR;

namespace BankA.Application.UseCases.Transactions.DeleteTransaction
{
    public class DeleteTransactionRequest : IRequest<bool>
    {
        public DeleteTransactionRequest(int accountId, int transactionId)
        {
            AccountId = accountId;
            TransactionId = transactionId;

        }

        public int TransactionId { get; set; }
        public int AccountId { get; set; }
    }
}
