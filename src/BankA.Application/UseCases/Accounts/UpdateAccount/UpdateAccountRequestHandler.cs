using BankA.Application.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BankA.Application.UseCases.Accounts.UpdateAccount
{
    class UpdateAccountRequestHandler : IRequestHandler<UpdateAccountRequest, bool>
    {

        private readonly IAppDataDbContext _bankADbContext;
        public UpdateAccountRequestHandler(IAppDataDbContext accountRepository)
        {
            _bankADbContext = accountRepository;
        }

        public async Task<bool> Handle(UpdateAccountRequest request, CancellationToken cancellationToken)
        {
            var account = await _bankADbContext.GetAccountById(request.AccountId);
            if (account == null)
            {
                throw new ApplicationException($"Account '{request.AccountId}' not found");
            }

            var exists = _bankADbContext.Accounts.Where(q => q.Description.ToLower() == request.Description && q.Id != request.AccountId).Any();
            if (exists == true)
            {
                throw new ApplicationException($"Account Name '{request.Description}' already exists");
            }

            account.UpdateAccount(request.Description, request.FileFormat);
            if (request.Closed)
                account.Close();
            else
                account.Open();

            await _bankADbContext.SaveChangesAsync();

            return true;
        }
    }
}
