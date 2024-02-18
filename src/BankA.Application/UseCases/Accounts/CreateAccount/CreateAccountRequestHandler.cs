using BankA.Application.Interfaces;
using BankA.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BankA.Application.UseCases.Accounts.CreateAccount
{
    class CreateAccountRequestHandler : IRequestHandler<CreateAccountRequest, CreateAccountResponse>
    {
        private readonly IAppDataDbContext _bankADbContext;
        public CreateAccountRequestHandler(IAppDataDbContext accountRepository)
        {
            _bankADbContext = accountRepository;
        }
        public async Task<CreateAccountResponse> Handle(CreateAccountRequest request, CancellationToken cancellationToken)
        {
            var account = new Account(request.Description, request.FileFormat);
            _bankADbContext.Accounts.Add(account);
            await _bankADbContext.SaveChangesAsync();

            return new CreateAccountResponse() { Id = account.Id };

        }
    }
}
