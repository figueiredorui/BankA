using BankA.Application.Interfaces;
using BankA.Domain;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BankA.Application.UseCases.Transactions.GetTransaction
{
    class GetTransactionRequestHandler : IRequestHandler<GetTransactionRequest, GetTransactionResponse>
    {

        private readonly IAppDataDbContext _bankADbContext;
        public GetTransactionRequestHandler(IAppDataDbContext bankADbContext)
        {
            _bankADbContext = bankADbContext;
        }

        public async Task<GetTransactionResponse> Handle(GetTransactionRequest request, CancellationToken cancellationToken)
        {
            var transaction = await GetTransaction(request);

            return new GetTransactionResponse(transaction);
        }

        private async Task<Transaction> GetTransaction(GetTransactionRequest request)
        {
            var transaction = await _bankADbContext.GetTransactionById(request.TransactionId);
            if (transaction == null)
                throw new ApplicationException($"Transaction '{request.TransactionId}' not found");
            return transaction;
        }
    }
}
