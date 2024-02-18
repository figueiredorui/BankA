using MediatR;

namespace BankA.Application.UseCases.Accounts.GetAccount
{
    public class GetAccountRequest : IRequest<GetAccountResponse>
    {
        public GetAccountRequest(int accountId)
        {
            AccountId = accountId;
        }

        public int AccountId { get; set; }
    }
}
