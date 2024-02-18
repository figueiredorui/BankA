using BankA.Application.Interfaces;
using BankA.Domain;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BankA.Application.UseCases.Accounts.GetAccount
{
    class GetAccountRequestHandler : IRequestHandler<GetAccountRequest, GetAccountResponse>
    {
        private readonly IAppDataDbContext _bankADbContext;
        public GetAccountRequestHandler(IAppDataDbContext accountRepository)
        {
            _bankADbContext = accountRepository;
        }

        public async Task<GetAccountResponse> Handle(GetAccountRequest request, CancellationToken cancellationToken)
        {
            var account = await GetAccount(request);

            return new GetAccountResponse()
            {
                Id = account.Id,
                Description = account.Description,
                Closed = account.Closed,
                FileFormat = account.FileFormat.ToString(),
                FileFormatConfiguration = account.FileFormatConfiguration
            };
        }

        private async Task<Account> GetAccount(GetAccountRequest request)
        {
            var account = await _bankADbContext.GetAccountById(request.AccountId);
            if (account == null)
                throw new ApplicationException($"Account '{request.AccountId}' not found");
            return account;
        }
    }
}
