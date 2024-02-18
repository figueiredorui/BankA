using BankA.Application.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BankA.Application.UseCases.Transactions.DeleteTransaction
{
    class DeleteTransactionRequestHandler : IRequestHandler<DeleteTransactionRequest, bool>
    {

        private readonly IAppDataDbContext _bankADbContext;
        public DeleteTransactionRequestHandler(IAppDataDbContext bankADbContext)
        {
            _bankADbContext = bankADbContext;
        }

        public async Task<bool> Handle(DeleteTransactionRequest request, CancellationToken cancellationToken)
        {
            var transaction = await _bankADbContext.GetTransactionById(request.TransactionId);
            if (transaction == null)
                throw new ApplicationException($"Transaction '{request.TransactionId}' not found");

            _bankADbContext.Transactions.Remove(transaction);
            await _bankADbContext.SaveChangesAsync();

            return true;
        }
    }
}
