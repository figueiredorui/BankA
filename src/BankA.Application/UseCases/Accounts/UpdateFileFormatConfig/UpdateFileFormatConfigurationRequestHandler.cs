using BankA.Application.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BankA.Application.UseCases.Accounts.FileFormatConfig
{
    class UpdateFileFormatConfigurationRequestHandler : IRequestHandler<UpdateFileFormatConfigurationRequest, bool>
    {

        private readonly IAppDataDbContext _bankADbContext;
        public UpdateFileFormatConfigurationRequestHandler(IAppDataDbContext accountRepository)
        {
            _bankADbContext = accountRepository;
        }

        public async Task<bool> Handle(UpdateFileFormatConfigurationRequest request, CancellationToken cancellationToken)
        {
            var account = await _bankADbContext.GetAccountById(request.AccountId);
            if (account == null)
            {
                throw new ApplicationException($"Account '{request.AccountId}' not found");
            }

            account.UpdateFileFormatConfig(request.FileFormatConfiguration);

            await _bankADbContext.SaveChangesAsync();

            return true;
        }
    }
}
