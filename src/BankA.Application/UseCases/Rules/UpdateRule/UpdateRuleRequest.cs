using MediatR;

namespace BankA.Application.UseCases.Rules.UpdateRule
{
    public class UpdateRuleRequest : IRequest<bool>
    {
        public int RuleId { get; set; }
        public string Keywords { get; set; }
        public int CategoryId { get; set; }
        public string MerchantName { get; set; }
    }
}
