using BankA.Application.UseCases.Transactions.Models;
using BankA.Domain;

namespace BankA.Application.UseCases.Transactions.GetTransaction
{
    public class GetTransactionResponse : TransactionModel
    {
        public GetTransactionResponse(Transaction transaction) : base(transaction)
        {
        }
    }
}
