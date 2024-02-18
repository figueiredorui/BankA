using MediatR;

namespace BankA.Application.UseCases.Accounts.CreateAccount
{
    public class CreateAccountRequest : IRequest<CreateAccountResponse>
    {
        public string Description { get; set; }
        public string FileFormat { get; set; }
    }
}
