using MediatR;

namespace BankA.Application.UseCases.Rules.CreateRule
{
    public class CreateRuleRequest : IRequest<CreateRuleResponse>
    {
        public string Keywords { get; set; }
        public int CategoryId { get; set; }
        public string MerchantName { get; set; }
    }
}
