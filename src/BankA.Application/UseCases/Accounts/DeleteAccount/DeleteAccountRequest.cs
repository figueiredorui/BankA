using MediatR;

namespace BankA.Application.UseCases.Accounts.DeleteAccount
{
    public class DeleteAccountRequest : IRequest<bool>
    {
        public DeleteAccountRequest(int accountId)
        {
            AccountId = accountId;
        }

        public int AccountId { get; set; }
    }
}
