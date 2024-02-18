using BankA.Application.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BankA.Application.UseCases.Accounts.DeleteAccount
{
    class DeleteAccountRequestHandler : IRequestHandler<DeleteAccountRequest, bool>
    {

        private readonly IAppDataDbContext _bankADbContext;
        public DeleteAccountRequestHandler(IAppDataDbContext accountRepository)
        {
            _bankADbContext = accountRepository;
        }

        public async Task<bool> Handle(DeleteAccountRequest request, CancellationToken cancellationToken)
        {
            var account = await _bankADbContext.GetAccountById(request.AccountId);
            if (account == null)
            {
                throw new ApplicationException($"Account '{request.AccountId}' not found");
            }

            _bankADbContext.Accounts.Remove(account);

            await _bankADbContext.SaveChangesAsync();

            return true;
        }
    }
}
