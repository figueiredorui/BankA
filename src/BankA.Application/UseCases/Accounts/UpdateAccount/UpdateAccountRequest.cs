using MediatR;

namespace BankA.Application.UseCases.Accounts.UpdateAccount
{
    public class UpdateAccountRequest : IRequest<bool>
    {
        public int AccountId { get; set; }
        public string Description { get; set; }
        public string FileFormat { get; set; }
        public bool Closed { get; set; }
    }
}
